using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using VaccineDose.App_Code;
using VaccineDose.Model;
using WebApi.OutputCache.V2;

namespace VaccineDose.Controllers
{
    public class ChildController : BaseController
    {
        #region C R U D
        public Response<IEnumerable<ChildDTO>> Get()
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbChilds = entities.Children.ToList();
                    List<ChildDTO> childDTOs = new List<ChildDTO>();
                    foreach (var child in dbChilds)
                    {
                        ChildDTO childDTO = Mapper.Map<ChildDTO>(child);
                        childDTO.CountryCode = child.User.CountryCode;
                        childDTO.MobileNumber = child.User.MobileNumber;
                        childDTOs.Add(childDTO);
                    }

                    return new Response<IEnumerable<ChildDTO>>(true, null, childDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ChildDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        public Response<ChildDTO> Get(int Id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbChild = entities.Children.Where(c => c.ID == Id).FirstOrDefault();
                    ChildDTO childDTO = Mapper.Map<ChildDTO>(dbChild);
                    childDTO.CountryCode = dbChild.User.CountryCode;
                    childDTO.MobileNumber = dbChild.User.MobileNumber;
                    return new Response<ChildDTO>(true, null, childDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<ChildDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        public Response<ChildDTO> Post(ChildDTO childDTO)
        {
            try
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                childDTO.Name = textInfo.ToTitleCase(childDTO.Name);
                childDTO.FatherName = textInfo.ToTitleCase(childDTO.FatherName);

                using (VDEntities entities = new VDEntities())
                {

                    Child childDB = Mapper.Map<Child>(childDTO);
                    // check for existing parent 
                    User user = entities.Users.Where(x => x.MobileNumber == childDTO.MobileNumber && x.UserType == "PARENT").FirstOrDefault();

                    if (user == null)
                    {
                        User userDB = new User();
                        userDB.MobileNumber = childDTO.MobileNumber;
                        userDB.Password = childDTO.Password;
                        userDB.CountryCode = childDTO.CountryCode;
                        userDB.UserType = "PARENT";
                        entities.Users.Add(userDB);
                        entities.SaveChanges();

                        childDB.UserID = userDB.ID;
                        entities.Children.Add(childDB);
                        entities.SaveChanges();
                    }
                    else
                    {
                        Child existingChild = entities.Children.FirstOrDefault(x => x.Name.Equals(childDTO.Name) && x.UserID == user.ID);
                        if (existingChild != null)
                            throw new Exception("Children with same name & number already exists. Parent should login and start change doctor process.");
                        childDB.UserID = user.ID;
                        entities.Children.Add(childDB);
                        entities.SaveChanges();
                    }
                    childDTO.ID = childDB.ID;

                    // get doctor schedule and apply it to child and save in Schedule table
                    Clinic clinic = entities.Clinics.Where(x => x.ID == childDTO.ClinicID).FirstOrDefault();
                    Doctor doctor = clinic.Doctor;
                    IEnumerable<DoctorSchedule> dss = doctor.DoctorSchedules;
                    foreach (DoctorSchedule ds in dss)
                    {
                        var dbDose = entities.Doses.Where(x => x.ID == ds.DoseID).FirstOrDefault();
                        if (childDTO.ChildVaccines.Any(x => x.ID == dbDose.Vaccine.ID))
                        {
                            Schedule cvd = new Schedule();
                            cvd.ChildId = childDTO.ID;
                            cvd.DoseId = ds.DoseID;
                            if (childDTO.IsEPIDone)
                            {
                                if (ds.Dose.Name.StartsWith("BCG")
                                    || ds.Dose.Name.StartsWith("HBV")
                                    || ds.Dose.Name.Equals("OPV # 1"))
                                {
                                    cvd.IsDone = true;
                                    cvd.Due2EPI = true;
                                    cvd.GivenDate = childDB.DOB;
                                }
                                else if (
                                  ds.Dose.Name.Equals("OPV/IPV+HBV+DPT+Hib # 1", StringComparison.OrdinalIgnoreCase)
                                  || ds.Dose.Name.Equals("Pneumococcal # 1", StringComparison.OrdinalIgnoreCase)
                                  || ds.Dose.Name.Equals("Rota Virus GE # 1", StringComparison.OrdinalIgnoreCase)
                                  )
                                {
                                    cvd.IsDone = true;
                                    cvd.Due2EPI = true;
                                    DateTime d = childDB.DOB;
                                    cvd.GivenDate = d.AddDays(42);
                                }
                                else if (
                                ds.Dose.Name.Equals("OPV/IPV+HBV+DPT+Hib # 2", StringComparison.OrdinalIgnoreCase)
                                || ds.Dose.Name.Equals("Pneumococcal # 2", StringComparison.OrdinalIgnoreCase)
                                || ds.Dose.Name.Equals("Rota Virus GE # 2", StringComparison.OrdinalIgnoreCase)
                                  )
                                {
                                    cvd.IsDone = true;
                                    cvd.Due2EPI = true;
                                    DateTime d = childDB.DOB;
                                    cvd.GivenDate = d.AddDays(70);
                                }
                                else if (
                                ds.Dose.Name.Equals("OPV/IPV+HBV+DPT+Hib # 3", StringComparison.OrdinalIgnoreCase)
                                || ds.Dose.Name.Equals("Pneumococcal # 3", StringComparison.OrdinalIgnoreCase)
                                )
                                {
                                    cvd.IsDone = true;
                                    cvd.Due2EPI = true;
                                    DateTime d = childDB.DOB;
                                    cvd.GivenDate = d.AddDays(98);
                                }
                                else if (
                               ds.Dose.Name.Equals("Measles # 1", StringComparison.OrdinalIgnoreCase)
                               )
                                {
                                    cvd.IsDone = true;
                                    cvd.Due2EPI = true;
                                    DateTime d = childDB.DOB;
                                    cvd.GivenDate = d.AddMonths(9);
                                }
                            }

                            cvd.Date = calculateDate(childDTO.DOB, ds.GapInDays);

                            entities.Schedules.Add(cvd);
                            entities.SaveChanges();
                        }
                    }
                    Child c = entities.Children.Include("Clinic").Where(x => x.ID == childDTO.ID).FirstOrDefault();
                    if (c.Email != "")
                        UserEmail.ParentEmail(c);

                    // generate SMS and save it to the db
                    UserSMS.ParentSMS(c);

                    return new Response<ChildDTO>(true, null, childDTO);
                }
                var cache = Configuration.CacheOutputConfiguration().GetCacheOutputProvider(Request);
                cache.RemoveStartsWith(Configuration.CacheOutputConfiguration().MakeBaseCachekey((DoctorController t) => t.GetAllChildsOfaDoctor(0, null)));
            }
            catch (Exception e)
            {
                return new Response<ChildDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }


        public Response<ChildDTO> Put([FromBody] ChildDTO childDTO)
        {
            try
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                childDTO.Name = textInfo.ToTitleCase(childDTO.Name);
                childDTO.FatherName = textInfo.ToTitleCase(childDTO.FatherName);

                using (VDEntities entities = new VDEntities())
                {
                    var dbChild = entities.Children.FirstOrDefault(c => c.ID == childDTO.ID);
                    if (dbChild == null) return new Response<ChildDTO>(false, "Child not found", null);
                    dbChild.Name = childDTO.Name;
                    dbChild.Email = childDTO.Email;
                    dbChild.FatherName = childDTO.FatherName;
                    dbChild.PreferredDayOfWeek = childDTO.PreferredDayOfWeek;
                    dbChild.Gender = childDTO.Gender;
                    dbChild.City = childDTO.City;
                    dbChild.PreferredDayOfReminder = childDTO.PreferredDayOfReminder;
                    dbChild.PreferredSchedule = childDTO.PreferredSchedule;
                    dbChild.IsEPIDone = childDTO.IsEPIDone;
                    dbChild.IsVerified = childDTO.IsVerified;

                    var dbUser = dbChild.User;
                    dbUser.MobileNumber = childDTO.MobileNumber;
                    entities.SaveChanges();
                    return new Response<ChildDTO>(true, null, childDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<ChildDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        public Response<string> Delete(int Id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbChild = entities.Children.Where(c => c.ID == Id).FirstOrDefault();
                    //entities.Schedules.RemoveRange(dbChild.Schedules);
                    //entities.FollowUps.RemoveRange(dbChild.FollowUps);
                    if(dbChild.User.Children.Count == 1)
                        entities.Users.Remove(dbChild.User);
                    entities.Children.Remove(dbChild);
                    entities.SaveChanges();
                    return new Response<string>(true, "Child is deleted successfully", null);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                    return new Response<string>(false, "Cannot delete child because it schedule exits. Delete the child schedule first.", null);
                else
                    return new Response<string>(false, GetMessageFromExceptionObject(ex), null);
            }
        }



        #endregion

        [HttpGet]
        [Route("~/api/child/{id}/GetChildAgainstMobile")]
        public Response<IEnumerable<ChildDTO>> GetChildAgainstMobile(string id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    User user = entities.Users.Where(x => x.MobileNumber == id).FirstOrDefault();
                    if (user != null)
                    {
                        var children = entities.Children.Where(c => c.UserID == user.ID).ToList();
                        IEnumerable<ChildDTO> childDTOs = Mapper.Map<IEnumerable<ChildDTO>>(children);
                        return new Response<IEnumerable<ChildDTO>>(true, null, childDTOs);
                    }
                    else
                    {
                        return new Response<IEnumerable<ChildDTO>>(false, "Childs not found", null);
                    }
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ChildDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        [HttpGet]
        [Route("~/api/child/{id}/GetCustomScheduleAgainsClinic")]
        public Response<DoctorScheduleDTO> GetCustomScheduleAgainsClinic(int id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var clinic = entities.Clinics.Where(c => c.ID == id).FirstOrDefault();
                    var doctorSchedule = clinic.Doctor.DoctorSchedules.FirstOrDefault();
                    if (doctorSchedule != null)
                    {
                        DoctorScheduleDTO doctorScheduleDTO = Mapper.Map<DoctorScheduleDTO>(doctorSchedule);
                        return new Response<DoctorScheduleDTO>(true, null, doctorScheduleDTO);
                    }
                    else
                    {
                        return new Response<DoctorScheduleDTO>(false, "Custom schedule is not added", null);
                    }

                }
            }
            catch (Exception e)
            {
                return new Response<DoctorScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        #region Schedule and Schedule PDF Methods

        [Route("~/api/child/{id}/schedule")]
        public Response<IEnumerable<ScheduleDTO>> GetChildSchedule(int id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var child = entities.Children.FirstOrDefault(c => c.ID == id);
                    if (child == null)
                        return new Response<IEnumerable<ScheduleDTO>>(false, "Child not found", null);
                    else
                    {
                        var dbSchedules = child.Schedules.OrderBy(x => x.Date).ToList();
                        for (int i = 0; i < dbSchedules.Count; i++)
                        {
                            var dbSchedule = dbSchedules.ElementAt(i);
                            dbSchedule.Dose = entities.Schedules.Include("Dose").Where<Schedule>(x => x.ID == dbSchedule.ID).FirstOrDefault().Dose;
                            dbSchedule.Brand = entities.Brands.Where<Brand>(x => x.ID == dbSchedule.BrandId).FirstOrDefault();
                        }

                        var schedulesDTO = Mapper.Map<List<ScheduleDTO>>(dbSchedules);
                        //foreach (var scheduleDTO in schedulesDTO)
                        //    scheduleDTO.Dose = Mapper.Map<DoseDTO>(entities.Schedules.Include("Dose").Where<Schedule>(x => x.ID == scheduleDTO.ID).FirstOrDefault<Schedule>().Dose);
                        return new Response<IEnumerable<ScheduleDTO>>(true, null, schedulesDTO);
                    }
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        [HttpGet]
        [Route("~/api/child/{id}/Download-Schedule-PDF")]
        public HttpResponseMessage DownloadSchedulePDF(int id)
        {
            Child dbScheduleChild;
            using (VDEntities entities = new VDEntities())
            {
                dbScheduleChild = entities.Children.Where(x => x.ID == id).FirstOrDefault();
            }
            var stream = CreateSchedulePdf(id);

            return new HttpResponseMessage
            {
                Content = new StreamContent(stream)
                {
                    Headers = {
                                ContentType = new MediaTypeHeaderValue("application/pdf"),
                                ContentDisposition = new ContentDispositionHeaderValue("attachment") {
                                    FileName =dbScheduleChild.Name.Replace(" ","")+"_Schedule_" +DateTime.UtcNow.AddHours(5).ToString("MMMM-dd-yyyy")+ ".pdf"
                                }
                            }
                },
                StatusCode = HttpStatusCode.OK
            };
        }

        private Stream CreateSchedulePdf(int childId)
        {
            //Access db data
            VDEntities entities = new VDEntities();
            var dbChild = entities.Children.Include("Clinic").Where(x => x.ID == childId).FirstOrDefault();
            var dbDoctor = dbChild.Clinic.Doctor;
            var child = entities.Children.FirstOrDefault(c => c.ID == childId);
            var dbSchedules = child.Schedules.OrderBy(x => x.Date).ToList();
            var scheduleDoses = from schedule in dbSchedules
                                group schedule.Dose by schedule.Date into scheduleDose
                                select new { Date = scheduleDose.Key, Doses = scheduleDose.ToList() };

            int count = 0;
            //
            using (var document = new Document(PageSize.A4, 50, 50, 25, 105))
            {
                var output = new MemoryStream();

                var writer = PdfWriter.GetInstance(document, output);
                writer.CloseStream = false;
                // calling PDFFooter class to Include in document
                writer.PageEvent = new PDFFooter(child);
                document.Open();
                GetPDFHeading(document, "Childhood Vaccination Record");

                //Table 1 for description above Schedule table
                PdfPTable upperTable = new PdfPTable(2);
                float[] upperTableWidths = new float[] { 250f, 250f };
                upperTable.HorizontalAlignment = 0;
                upperTable.TotalWidth = 500f;
                upperTable.LockedWidth = true;
                upperTable.SetWidths(upperTableWidths);

                upperTable.AddCell(CreateCell(dbDoctor.DisplayName, "bold", 1, "left", "description"));
                upperTable.AddCell(CreateCell(dbChild.Name, "bold", 1, "right", "description"));

                upperTable.AddCell(CreateCell(dbChild.Clinic.Name, "", 1, "left", "description"));
                if (dbChild.Gender == "Girl")
                {
                    upperTable.AddCell(CreateCell("D/O " + dbChild.FatherName, "", 1, "right", "description"));
                }
                else
                {
                    upperTable.AddCell(CreateCell("S/O " + dbChild.FatherName, "", 1, "right", "description"));
                }

                upperTable.AddCell(CreateCell(dbChild.Clinic.Address, "", 1, "left", "description"));
                upperTable.AddCell(CreateCell("+" + dbChild.User.CountryCode + "-" + dbChild.User.MobileNumber, "", 1, "right", "description"));

                upperTable.AddCell(CreateCell("Clinic Ph#: " + dbChild.Clinic.PhoneNumber, "", 1, "left", "description"));
                upperTable.AddCell(CreateCell(dbChild.DOB.ToString("MM/dd/yyyy"), "", 1, "right", "description"));

                if (dbDoctor.ShowPhone)
                {
                    upperTable.AddCell(CreateCell("Doctor Ph#: +" + dbDoctor.User.CountryCode + "-" + dbDoctor.PhoneNo, "", 1, "left", "description"));
                }
                else
                {
                    upperTable.AddCell(CreateCell("", "", 1, "left", "description"));

                }
                upperTable.AddCell(CreateCell("", "", 1, "right", "description"));
                document.Add(upperTable);

                document.Add(new Paragraph(""));
                document.Add(new Chunk("\n"));
                //Schedule Table
                float[] widths = new float[] { 25f, 145f, 70f, 70f, 45f, 45f, 45f };

                PdfPTable table = new PdfPTable(7);
                table.HorizontalAlignment = 0;
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(widths);

                table.AddCell(CreateCell("Age", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Vaccine", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Due Date", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Given Date", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Weight", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Height", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("OFC", "backgroudLightGray", 1, "center", "scheduleRecords"));
                //table.AddCell(CreateCell("Injected", "backgroudLightGray", 1, "center", "scheduleRecords"));

                var imgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/img");
                foreach (var schedule in scheduleDoses)
                {
                    int doseCount = 0;
                    Paragraph p = new Paragraph();
                    foreach (var dose in schedule.Doses)
                    {
                        count++;
                        doseCount++;
                        Font font = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                        // select only injected dose schedule
                        var dbSchedule = dose.Schedules.Where(x => x.DoseId == dose.ID).FirstOrDefault();

                        // table.AddCell(CreateCell(count.ToString(), "", 1, "center", "scheduleRecords"));
                        //table.AddCell(CreateCell(dose.Name, "", 1, "", "scheduleRecords"));


                        if (doseCount == 1)
                        {
                            PdfPCell ageCell = new PdfPCell(new Phrase(count.ToString(), font));
                            ageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            ageCell.Rowspan = schedule.Doses.Count();
                            table.AddCell(ageCell);
                            foreach (var d in schedule.Doses)
                            {
                                p.Add(d.Name + "\n");
                            }
                            PdfPCell dosenameCell = new PdfPCell(p);
                            dosenameCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            dosenameCell.Rowspan = schedule.Doses.Count();
                            table.AddCell(dosenameCell);

                            PdfPCell sameDueDateCell = new PdfPCell(new Phrase(schedule.Date.Date.ToString("dd/MM/yyyy"), font));
                            sameDueDateCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            sameDueDateCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            sameDueDateCell.PaddingBottom = schedule.Doses.Count();
                            sameDueDateCell.Rowspan = schedule.Doses.Count();
                            table.AddCell(sameDueDateCell);
                        }

                        PdfPCell dateCell = new PdfPCell(new Phrase(String.Format("{0:dd/MM/yyyy}", dbSchedule.GivenDate), font));
                        dateCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(dateCell);

                        PdfPCell weightCell = new PdfPCell(new Phrase(dbSchedule.Weight > 0 ? dbSchedule.Weight.ToString() : "", font));
                        weightCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(weightCell);

                        PdfPCell heightCell = new PdfPCell(new Phrase(dbSchedule.Height > 0 ? dbSchedule.Height.ToString() : "", font));
                        heightCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(heightCell);

                        PdfPCell circleCell = new PdfPCell(new Phrase(dbSchedule.Circle > 0 ? dbSchedule.Circle.ToString() : "", font));
                        circleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(circleCell);
                        //table.AddCell(CreateCell(String.Format("{0:dd/MM/yyyy}", dbSchedule.GivenDate), "", 1, "", "scheduleRecords"));
                        //table.AddCell(CreateCell(dbSchedule.Weight.ToString(), "", 1, "", "scheduleRecords"));
                        //table.AddCell(CreateCell(dbSchedule.Height.ToString(), "", 1, "", "scheduleRecords"));
                        //table.AddCell(CreateCell(dbSchedule.Circle.ToString(), "", 1, "", "scheduleRecords"));


                        ////  add a image
                        //var isDone = dbSchedule.Where(x => x.IsDone).FirstOrDefault();
                        //string injectionPath = "";
                        //if (dbSchedule.IsDone)
                        //{
                        //    injectionPath = "\\injectionFilled.png";
                        //}
                        //else
                        //{
                        //    injectionPath = "\\injectionEmpty.png";
                        //}
                        //Image img = Image.GetInstance(imgPath + injectionPath);
                        //img.ScaleAbsolute(2f, 2f);
                        //PdfPCell imageCell = new PdfPCell(img, true);
                        //imageCell.PaddingBottom = 2;
                        //imageCell.Colspan = 1; // either 1 if you need to insert one cell
                        ////imageCell.Border = 0;
                        //imageCell.FixedHeight = 15f;
                        //imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //table.AddCell(imageCell);
                    }


                    //  imageCell.setHorizontalAlignment(Element.ALIGN_CENTER);

                }

                document.Add(table);
                document.Close();

                output.Seek(0, SeekOrigin.Begin);

                return output;
            }
        }


        #endregion

        #region Invoice PDF Methods

        [HttpPost]
        [Route("api/child/Download-Invoice-PDF")]
        public HttpResponseMessage DownloadInvoicePDF(ChildDTO childDTO)
        {
            try
            {
                Stream stream;
                int amount = 0;
                int count = 1;
                int col = 3;
                int consultaionFee = 0;
                string childName = "";

                using (var document = new Document(PageSize.A4, 50, 50, 25, 25))
                {
                    var output = new MemoryStream();

                    var writer = PdfWriter.GetInstance(document, output);
                    writer.CloseStream = false;

                    document.Open();
                    //Page Heading
                    GetPDFHeading(document, "INVOICE");

                    //Access db data
                    VDEntities entities = new VDEntities();
                    var dbDoctor = entities.Doctors.Where(x => x.ID == childDTO.DoctorID).FirstOrDefault();
                    dbDoctor.InvoiceNumber = (dbDoctor.InvoiceNumber > 0) ? dbDoctor.InvoiceNumber + 1 : 1;
                    var dbChild = entities.Children.Include("Clinic").Where(x => x.ID == childDTO.ID).FirstOrDefault();
                    var dbSchedules = entities.Schedules.Include("Dose").Include("Brand").Where(x => x.ChildId == childDTO.ID).ToList();
                    childName = dbChild.Name;
                    //
                    //Table 1 for description above amounts table
                    PdfPTable upperTable = new PdfPTable(2);
                    float[] upperTableWidths = new float[] { 250f, 250f };
                    upperTable.HorizontalAlignment = 0;
                    upperTable.TotalWidth = 500f;
                    upperTable.LockedWidth = true;
                    // upperTable.DefaultCell.PaddingLeft = 4;
                    upperTable.SetWidths(upperTableWidths);

                    upperTable.AddCell(CreateCell("Dr " + dbDoctor.FirstName + dbDoctor.LastName, "bold", 1, "left", "description"));
                    //upperTable.AddCell(CreateCell("Invoice", "bold", 1, "right", "description"));
                    upperTable.AddCell(CreateCell("Invoice # " + dbDoctor.InvoiceNumber, "bold", 1, "right", "description"));
                    upperTable.AddCell(CreateCell("", "", 1, "left", "description"));
                    upperTable.AddCell(CreateCell("", "", 1, "right", "description"));

                    upperTable.AddCell(CreateCell(dbChild.Clinic.Name, "bold", 1, "left", "description"));

                    //upperTable.AddCell(CreateCell("Clinic Ph: " + dbChild.Clinic.PhoneNumber, "noColor", 1, "left", "description"));

                    upperTable.AddCell(CreateCell("Date: " + DateTime.UtcNow.AddHours(5), "bold", 1, "right", "description"));


                    if (childDTO.IsConsultationFee)
                    {
                        consultaionFee = (int)dbChild.Clinic.ConsultationFee;
                    }
                    //  upperTable.AddCell(CreateCell("Consultation Fee: " + consultaionFee, "noColor", 1, "left", "description"));
                    //upperTable.AddCell(CreateCell("", "", 1, "left", "description"));
                    //upperTable.AddCell(CreateCell("", "", 1, "right", "description"));

                    upperTable.AddCell(CreateCell("", "", 1, "left", "description"));
                    upperTable.AddCell(CreateCell("Bill To: " + dbChild.Name, "noColor", 1, "right", "description"));
                    //upperTable.AddCell(CreateCell("", "", 1, "left", "description"));
                    //upperTable.AddCell(CreateCell("Father: " + dbChild.FatherName, "", 1, "right", "description"));
                    //upperTable.AddCell(CreateCell("", "", 1, "left", "description"));
                    //upperTable.AddCell(CreateCell("Child: " + dbChild.Name, "", 1, "right", "description"));
                    upperTable.AddCell(CreateCell("P: " + dbDoctor.PhoneNo, "bold", 1, "left", "description"));
                    upperTable.AddCell(CreateCell("", "", 1, "right", "description"));
                    upperTable.AddCell(CreateCell("M: " + dbDoctor.User.MobileNumber, "bold", 1, "left", "description"));
                    upperTable.AddCell(CreateCell("", "", 1, "right", "description"));

                    document.Add(upperTable);
                    document.Add(new Paragraph(""));
                    document.Add(new Chunk("\n"));

                    //2nd Table
                    float[] widths = new float[] { 30f, 200f, 100f };
                    if (childDTO.IsBrand)
                    {
                        col = 4;
                        widths = new float[] { 30f, 200f, 150f, 100f };
                    }

                    PdfPTable table = new PdfPTable(col);
                    // table.WidthPercentage = 100;

                    table.HorizontalAlignment = 0;
                    table.TotalWidth = 500f;
                    table.LockedWidth = true;
                    table.SetWidths(widths);

                    table.AddCell(CreateCell("#", "backgroudLightGray", 1, "center", "invoiceRecords"));
                    table.AddCell(CreateCell("Item", "backgroudLightGray", 1, "center", "invoiceRecords"));
                    if (childDTO.IsBrand)
                    {
                        table.AddCell(CreateCell("Brand", "backgroudLightGray", 1, "center", "invoiceRecords"));
                    }
                    table.AddCell(CreateCell("Amount", "backgroudLightGray", 1, "center", "invoiceRecords"));
                    //Rows
                    table.AddCell(CreateCell(count.ToString(), "", 1, "center", "invoiceRecords"));
                    //col = (col > 3) ? col - 3 : col-2;
                    if (col - 2 < 2)
                    {
                        table.AddCell(CreateCell("Consultation Fee", "", col - 2, "left", "invoiceRecords"));
                    }
                    else
                    {
                        table.AddCell(CreateCell("Consultation Fee", "", 1, "left", "invoiceRecords"));
                        table.AddCell(CreateCell("------------------", "", 1, "center", "invoiceRecords"));

                    }
                    table.AddCell(CreateCell(consultaionFee.ToString(), "", 1, "right", "invoiceRecords"));
                    if (dbSchedules.Count != 0)
                    {

                        foreach (var schedule in dbSchedules)
                        {
                            //date is static due to date conversion issue
                            //  && schedule.Date.Date == DateTime.Now.Date
                            //when we add bulk injection we don't add brandId in schedule
                            if (schedule.IsDone && schedule.BrandId > 0)
                            {
                                count++;
                                table.AddCell(CreateCell(count.ToString(), "", 1, "center", "invoiceRecords"));
                                table.AddCell(CreateCell(schedule.Dose.Vaccine.Name, "", 1, "center", "invoiceRecords"));
                                if (childDTO.IsBrand)
                                {
                                    table.AddCell(CreateCell(schedule.Brand.Name, "", 1, "center", "invoiceRecords"));
                                }
                                var brandAmount = entities.BrandAmounts.Where(x => x.BrandID == schedule.BrandId && x.DoctorID == childDTO.DoctorID).FirstOrDefault();
                                amount = amount + Convert.ToInt32(brandAmount.Amount);
                                table.AddCell(CreateCell(brandAmount.Amount.ToString(), "", 1, "right", "invoiceRecords"));
                            }

                        }

                    }

                    //table.AddCell(CreateCell("Total(PKR)", "", col - 1, "right", "invoiceRecords"));

                    //add consultancy fee
                    if (childDTO.IsConsultationFee)
                    {
                        amount = amount + (int)dbChild.Clinic.ConsultationFee;
                    }
                    //table.AddCell(CreateCell(amount.ToString(), "", 1, "right", "invoiceRecords"));

                    entities.SaveChanges();
                    document.Add(table);

                    document.Add(new Paragraph(""));
                    document.Add(new Chunk("\n"));
                    //Table 3 for description above amounts table
                    PdfPTable bottomTable = new PdfPTable(2);
                    float[] bottomTableWidths = new float[] { 200f, 200f };
                    bottomTable.HorizontalAlignment = 0;
                    bottomTable.TotalWidth = 400f;
                    bottomTable.LockedWidth = true;
                    bottomTable.SetWidths(bottomTableWidths);

                    bottomTable.AddCell(CreateCell("Thank you for your vaccination", "bold", 1, "left", "description"));
                    bottomTable.AddCell(CreateCell("Total Amount: " + amount.ToString() + "/-", "bold", 1, "right", "description"));

                    var imgcellLeft = CreateCell("", "", 1, "left", "description");
                    imgcellLeft.PaddingTop = 5;
                    bottomTable.AddCell(imgcellLeft);

                    var imgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/UserImages");
                    var signatureImage = dbDoctor.SignatureImage;
                    if (signatureImage == null)
                    {
                        signatureImage = "avatar.png";
                    }
                    Image img = Image.GetInstance(imgPath + "\\" + signatureImage);

                    img.ScaleAbsolute(2f, 2f);
                    PdfPCell imageCell = new PdfPCell(img, true);
                    imageCell.PaddingTop = 5;
                    imageCell.Colspan = 1; // either 1 if you need to insert one cell
                    imageCell.Border = 0;
                    imageCell.FixedHeight = 40f;
                    imageCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    bottomTable.AddCell(imageCell);

                    document.Add(bottomTable);
                    document.Close();
                    output.Seek(0, SeekOrigin.Begin);
                    stream = output;

                }
                return new HttpResponseMessage
                {
                    Content = new StreamContent(stream)
                    {
                        Headers =
                            {
                                ContentType = new MediaTypeHeaderValue("application/pdf"),
                                ContentDisposition = new ContentDispositionHeaderValue("attachment")
                                {
                                    FileName = childName.Replace(" ","") +"_FollowUp"+"_"+DateTime.UtcNow.AddHours(5).Date.ToString("MMMM-dd-yyyy")+".pdf"
                                }
                            }
                    },
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }

        }

        private static void GetPDFHeading(Document document, String headingText)
        {
            Font ColFont = FontFactory.GetFont(FontFactory.HELVETICA, 26, Font.BOLD);
            Chunk chunkCols = new Chunk(headingText, ColFont);
            Paragraph chunkParagraph = new Paragraph();
            chunkParagraph.Alignment = Element.ALIGN_CENTER;
            chunkParagraph.Add(chunkCols);
            document.Add(chunkParagraph);
            document.Add(new Paragraph(""));
            document.Add(new Chunk("\n"));
        }

        protected PdfPCell CreateCell(string value, string color, int colpan, string alignment, string table)
        {

            Font font = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            if (color == "bold")
            {
                font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            }
            if (table != "description")
            {
                font.Size = 7;
            }
            PdfPCell cell = new PdfPCell(new Phrase(value, font));
            if (color == "backgroudLightGray")
            {
                cell.BackgroundColor = GrayColor.LIGHT_GRAY;
            }
            if (alignment == "right")
            {
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            }
            if (alignment == "left")
            {
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
            }
            if (alignment == "center")
            {
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
            }
            cell.Colspan = colpan;
            if (table == "description")
            {
                cell.Border = 0;
                cell.Padding = 2f;
            }
            return cell;

        }

        #endregion

        #region FollowUp and FollowUp PDF Methods

        [HttpPost]
        [Route("~/api/child/followup")]
        public Response<List<FollowUpDTO>> GetFollowUp(FollowUpDTO followUpDto)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    //when followup call from parent side
                    if (followUpDto.DoctorID < 1)
                    {
                        var dbChild = entities.Children.Include("Clinic").FirstOrDefault();
                        followUpDto.DoctorID = dbChild.Clinic.DoctorID;
                    }
                    // when followup call from doctor side
                    var dbFollowUps = entities.FollowUps
                        .Where(f => f.DoctorID == followUpDto.DoctorID && f.ChildID == followUpDto.ChildID)
                        .OrderByDescending(x => x.CurrentVisitDate).ToList();
                    List<FollowUpDTO> followUpDTOs = Mapper.Map<List<FollowUpDTO>>(dbFollowUps);
                    return new Response<List<FollowUpDTO>>(true, null, followUpDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<List<FollowUpDTO>>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        [HttpPost]
        [Route("~/api/child/Download-FollowUp-PDF")]
        public HttpResponseMessage DownloadFollowUpPDF(FollowUpDTO followUpDto)
        {
            VDEntities entities = new VDEntities();
            Child child = entities.Children.Where(x => x.ID == followUpDto.ChildID).FirstOrDefault();
            var stream = CreateFollowUpPdf(child);

            return new HttpResponseMessage
            {
                Content = new StreamContent(stream)
                {
                    Headers = {
                                ContentType = new MediaTypeHeaderValue("application/pdf"),
                                ContentDisposition = new ContentDispositionHeaderValue("attachment")
                                {
                                    FileName = "myfile.pdf"
                                }
                            }
                },
                StatusCode = HttpStatusCode.OK
            };
        }

        private Stream CreateFollowUpPdf(Child child)
        {
            using (var document = new Document(PageSize.A4, 50, 50, 25, 25))
            {
                var output = new MemoryStream();
                var writer = PdfWriter.GetInstance(document, output);
                writer.CloseStream = false;

                document.Open();

                GetPDFHeading(document, "Medical Visit History");


                //Table 1 for description above history table
                PdfPTable upperTable = new PdfPTable(2);
                float[] upperTableWidths = new float[] { 250f, 250f };
                upperTable.HorizontalAlignment = 0;
                upperTable.TotalWidth = 500f;
                upperTable.LockedWidth = true;
                upperTable.SetWidths(upperTableWidths);

                upperTable.AddCell(CreateCell("Dr " + child.Clinic.Doctor.FirstName + " " + child.Clinic.Doctor.LastName, "bold", 1, "left", "description"));
                upperTable.AddCell(CreateCell(child.Name, "bold", 1, "right", "description"));
                upperTable.AddCell(CreateCell("", "", 1, "left", "description"));
                upperTable.AddCell(CreateCell("", "", 1, "right", "description"));
                upperTable.AddCell(CreateCell(child.Clinic.Name, "bold", 1, "left", "description"));
                upperTable.AddCell(CreateCell("Date of Birth: " + child.DOB.ToString("dd-MM-yyyy"), "bold", 1, "right", "description"));

                upperTable.AddCell(CreateCell("", "", 1, "left", "description"));
                upperTable.AddCell(CreateCell("", "", 1, "right", "description"));

                upperTable.AddCell(CreateCell("P: " + child.Clinic.Doctor.PhoneNo, "bold", 1, "left", "description"));
                upperTable.AddCell(CreateCell("", "", 1, "right", "description"));
                upperTable.AddCell(CreateCell("M: " + child.Clinic.Doctor.User.MobileNumber, "bold", 1, "left", "description"));
                upperTable.AddCell(CreateCell("", "", 1, "right", "description"));

                document.Add(upperTable);

                PdfPTable table = new PdfPTable(3);
                table.TotalWidth = 500f;
                //fix the absolute width of the table
                table.LockedWidth = true;

                //relative col widths in proportions - 1/3 and 2/3
                float[] widths = new float[] { 30f, 200f, 270f };
                table.SetWidths(widths);
                table.HorizontalAlignment = 0;
                //leave a gap before and after the table
                table.SpacingBefore = 20f;
                table.SpacingAfter = 30f;

                table.AddCell(GetHeaderCell("#"));
                table.AddCell(GetHeaderCell("Date"));
                table.AddCell(GetHeaderCell("Diagnosis"));
                var followUps = child.FollowUps.ToList();
                foreach (var item in followUps)
                {
                    table.AddCell(new PdfPCell(new Phrase((followUps.IndexOf(item) + 1) + "")));
                    table.AddCell(new PdfPCell(new Phrase(item.CurrentVisitDate.Value.ToString("dd-MM-yyyy"))));
                    //table.AddCell(new PdfPCell(new Phrase(item.NextVisitDate.Value.ToString("dd-MM-yyyy"))));
                    table.AddCell(new PdfPCell(new Phrase(item.Disease.ToString())));
                }

                document.Add(table);

                document.Close();

                output.Seek(0, SeekOrigin.Begin);

                return output;
            }
        }

        private PdfPCell GetHeaderCell(string v)
        {
            Font font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            PdfPCell cell = new PdfPCell(new Phrase(v, font));
            return cell;
        }

        #endregion

        [HttpPost]
        [Route("api/child/change-doctor")]
        public Response<ChildDTO> ChangeDoctor(ChildDTO childDTO)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbChild = entities.Children.Where(c => c.ID == childDTO.ID).FirstOrDefault();
                    //give notification to old doctor
                    UserEmail.DoctorEmail(Mapper.Map<ChildDTO>(dbChild), "old");
                    //TODO: give notification on sms, to both doctors

                    var dbClinic = entities.Clinics.Where(x => x.ID == childDTO.ClinicID).FirstOrDefault();
                    childDTO.Name = dbChild.Name;
                    childDTO.Clinic = Mapper.Map<ClinicDTO>(dbClinic);
                    //give notification to new selected doctor
                    UserEmail.DoctorEmail(childDTO, "new");

                    dbChild.ClinicID = childDTO.ClinicID;
                    entities.SaveChanges();
                    return new Response<ChildDTO>(true, null, childDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<ChildDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        [HttpGet]
        [Route("api/child/{keyword}/search")]
        public Response<IEnumerable<ChildDTO>> SearchChildren(string keyword)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {

                    List<Child> dbChildrenResults = new List<Child>();
                    List<ChildDTO> childDTOs = new List<ChildDTO>();

                    dbChildrenResults = entities.Children.Where(c => c.Name.ToLower().Contains(keyword.ToLower()) ||
                                        c.FatherName.ToLower().Contains(keyword.ToLower())).ToList();
                    childDTOs.AddRange(Mapper.Map<List<ChildDTO>>(dbChildrenResults));

                    foreach (var item in childDTOs)
                    {
                        item.MobileNumber = dbChildrenResults.Where(x => x.ID == item.ID).FirstOrDefault().User.MobileNumber;
                    }

                    return new Response<IEnumerable<ChildDTO>>(true, null, childDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ChildDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }


        [HttpPost]
        [Route("api/child/validate-nameAndNumber")]
        public HttpResponseMessage checkNameAndNumber([FromBody]ChildDTO obj)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbChild = entities.Children.Where(x => x.Name == obj.Name && x.User.MobileNumber == obj.MobileNumber && x.User.CountryCode == obj.CountryCode).FirstOrDefault();
                    if (dbChild == null)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ChildDTO>(dbChild));
                        return response;

                    }
                    else
                    {
                        int HTTPResponse = 400;
                        var response = Request.CreateResponse((HttpStatusCode)HTTPResponse);
                        response.ReasonPhrase = "Child with this name and mobile number already exists!";
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpPost]
        [Route("api/child/simpledoctor-child")]
        public Response<ChildDTO> AddSimpleDoctorPatient(ChildDTO childDTO)
        {
            try
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                childDTO.Name = textInfo.ToTitleCase(childDTO.Name);
                childDTO.FatherName = textInfo.ToTitleCase(childDTO.FatherName);

                using (VDEntities entities = new VDEntities())
                {

                    Child childDB = Mapper.Map<Child>(childDTO);
                    // check for existing parent 
                    User user = entities.Users.Where(x => x.MobileNumber == childDTO.MobileNumber && x.UserType == "PARENT").FirstOrDefault();

                    if (user == null)
                    {
                        User userDB = new User();
                        userDB.MobileNumber = childDTO.MobileNumber;
                        userDB.Password = childDTO.Password;
                        userDB.CountryCode = childDTO.CountryCode;
                        userDB.UserType = "PARENT";
                        entities.Users.Add(userDB);
                        entities.SaveChanges();

                        childDB.UserID = userDB.ID;
                        entities.Children.Add(childDB);
                        entities.SaveChanges();
                    }
                    else
                    {
                        Child existingChild = entities.Children.FirstOrDefault(x => x.Name.Equals(childDTO.Name) && x.UserID == user.ID);
                        if (existingChild != null)
                            throw new Exception("Children with same name & number already exists. Parent should login and start change doctor process.");
                        childDB.UserID = user.ID;
                        entities.Children.Add(childDB);
                        entities.SaveChanges();
                    }
                    childDTO.ID = childDB.ID;

                    Child c = entities.Children.Include("Clinic").Where(x => x.ID == childDTO.ID).FirstOrDefault();
                    if (c.Email != "")
                        UserEmail.ParentEmail(c);

                    // generate SMS and save it to the db
                    UserSMS.ParentSMS(c);

                    return new Response<ChildDTO>(true, null, childDTO);
                }
                var cache = Configuration.CacheOutputConfiguration().GetCacheOutputProvider(Request);
                cache.RemoveStartsWith(Configuration.CacheOutputConfiguration().MakeBaseCachekey((DoctorController t) => t.GetAllChildsOfaDoctor(0, null)));
            }
            catch (Exception e)
            {
                return new Response<ChildDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

    }

    public class PDFFooter : PdfPageEventHelper
    {
        Child child = new Child();
        public PDFFooter(Child postedChild)
        {
            child = postedChild;
        }
        // write on top of document
        //public override void OnOpenDocument(PdfWriter writer, Document document)
        //{
        //    base.OnOpenDocument(writer, document);
        //    PdfPTable tabFot = new PdfPTable(new float[] { 1F });
        //    tabFot.SpacingAfter = 10F;
        //    PdfPCell cell;
        //    tabFot.TotalWidth = 300F;
        //    cell = new PdfPCell(new Phrase("Header"));
        //    tabFot.AddCell(cell);
        //    tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent);
        //}

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            string footer = @"This schedule is automatically generated for " + child.Clinic.Name + @" by Vaccs.io Visit http://www.vaccs.io/ for more details
             ____________________________________________________________________________________________________________________________________________
             Disclaimer: This schedule provides recommended dates for immunizations for your child based on date of birth. Your pediatrician
             may update due dates or add/remove vaccines from this schedule.Vaccs.io or its management or staff holds no responsibility on any loss or damage due to any vaccine given to child at any given timeOfSending.";
            footer = footer.Replace(Environment.NewLine, String.Empty).Replace("  ", String.Empty);
            Font georgia = FontFactory.GetFont("georgia", 7f);

            Chunk beginning = new Chunk(footer, georgia);

            PdfPTable tabFot = new PdfPTable(1);
            PdfPCell cell;
            tabFot.SetTotalWidth(new float[] { 575f });
            tabFot.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell = new PdfPCell(new Phrase(beginning));
            cell.Border = 0;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 10, 50, writer.DirectContent);
        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }
}
