using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using VaccineDose.App_Code;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    public class ChildController : BaseController
    {
        #region C R U D
        public Response<IEnumerable<ChildDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
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
                using (VDConnectionString entities = new VDConnectionString())
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
                using (VDConnectionString entities = new VDConnectionString())
                {
                    Child childDB = Mapper.Map<Child>(childDTO);
                    // check for existing parent 
                    User user = entities.Users.Where(x => x.MobileNumber == childDTO.MobileNumber).FirstOrDefault();

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
                            cvd.IsDone = false;
                            cvd.Date = childDTO.DOB.AddDays(ds.GapInDays);
                            entities.Schedules.Add(cvd);
                            entities.SaveChanges();
                        }
                    }
                    Child c = entities.Children.Include("Clinic").Where(x => x.ID == childDTO.ID).FirstOrDefault();
                    UserEmail.ParentEmail(c);

                    // generate SMS and save it to the db
                    string sms = UserSMS.ParentSMS(c);
                    Message m = new Message();
                    m.MobileNumber = c.User.MobileNumber;
                    m.SMS = sms;
                    m.Status = "PENDING";
                    entities.Messages.Add(m);
                    entities.SaveChanges();

                    return new Response<ChildDTO>(true, null, childDTO);
                }
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
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbChild = entities.Children.Where(c => c.ID == childDTO.ID).FirstOrDefault();
                    dbChild.Name = childDTO.Name;
                    dbChild.FatherName = childDTO.FatherName;
                    dbChild.PreferredDayOfWeek = childDTO.PreferredDayOfWeek;
                    dbChild.Gender = childDTO.Gender;
                    dbChild.City = childDTO.City;
                    dbChild.PreferredDayOfReminder = childDTO.PreferredDayOfReminder;
                    dbChild.PreferredSchedule = childDTO.PreferredSchedule;
                    dbChild.IsEPIDone = childDTO.IsEPIDone;
                    dbChild.IsVerified = childDTO.IsVerified;

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
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbSchedules = entities.Schedules.Where(s => s.ChildId == Id).ToList();
                    var dbFollowUps = entities.FollowUps.Where(f => f.ChildID == Id).ToList();
                    var dbChild = entities.Children.Where(c => c.ID == Id).FirstOrDefault();
                    var children = entities.Children.Where(c => c.UserID == dbChild.UserID).ToList();
                    //delete child schedules
                    foreach (var schedule in dbSchedules)
                    {
                        entities.Schedules.Remove(schedule);
                    }
                    //delete child followup history
                    foreach (var followup in dbFollowUps)
                    {
                        entities.FollowUps.Remove(followup);
                    }
                    //delete user also, iff child its self a parent
                    if (children.Count == 1)
                    {
                        entities.Users.Remove(dbChild.User);
                    }
                    //delete child now
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
                using (VDConnectionString entities = new VDConnectionString())
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
                using (VDConnectionString entities = new VDConnectionString())
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
                using (VDConnectionString entities = new VDConnectionString())
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
            using (VDConnectionString entities = new VDConnectionString())
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
                                    FileName =dbScheduleChild.Name.Replace(" ","")+"_Schedule_" +DateTime.Now.ToString("MMMM-dd-yyyy")+ ".pdf"
                                }
                            }
                },
                StatusCode = HttpStatusCode.OK
            };
        }

        private Stream CreateSchedulePdf(int childId)
        {
            //Access db data
            VDConnectionString entities = new VDConnectionString();
            var dbChild = entities.Children.Include("Clinic").Where(x => x.ID == childId).FirstOrDefault();
            var dbDoctor = dbChild.Clinic.Doctor;
            var child = entities.Children.FirstOrDefault(c => c.ID == childId);
            var dbSchedules = child.Schedules.OrderBy(x => x.Date).ToList();
            var scheduleDoses = from schedule in dbSchedules
                                group schedule.Dose by schedule.Date into scheduleDose
                                select new { Date = scheduleDose.Key, Doses = scheduleDose.ToList() };

            int count = 0;
            //
            using (var document = new Document(PageSize.A4, 50, 50, 25, 25))
            {
                var output = new MemoryStream();

                var writer = PdfWriter.GetInstance(document, output);
                writer.CloseStream = false;

                document.Open();
                GetPDFHeading(document, "Child Vaccine Schedule");

                //Table 1 for description above Schedule table
                PdfPTable upperTable = new PdfPTable(2);
                float[] upperTableWidths = new float[] { 250f, 250f };
                upperTable.HorizontalAlignment = 0;
                upperTable.TotalWidth = 500f;
                upperTable.LockedWidth = true;
                upperTable.SetWidths(upperTableWidths);

                upperTable.AddCell(CreateCell("Clinic", "bold", 1, "left", "description"));
                upperTable.AddCell(CreateCell("Patient", "bold", 1, "right", "description"));

                upperTable.AddCell(CreateCell(dbChild.Clinic.Name, "", 1, "left", "description"));
                //upperTable.AddCell(CreateCell("Father: " + dbChild.FatherName, "", 1, "right", "description"));
                upperTable.AddCell(CreateCell(dbChild.Name, "", 1, "right", "description"));

                upperTable.AddCell(CreateCell("Clinic Ph: " + dbChild.Clinic.PhoneNumber, "", 1, "left", "description"));
                upperTable.AddCell(CreateCell(dbChild.FatherName, "", 1, "right", "description"));

                upperTable.AddCell(CreateCell("Doctor: " + dbDoctor.FirstName, "", 1, "left", "description"));
                //upperTable.AddCell(CreateCell("Child: " + dbChild.Name, "", 1, "right", "description"));
                upperTable.AddCell(CreateCell(dbChild.User.MobileNumber, "", 1, "right", "description"));

                upperTable.AddCell(CreateCell("Doctor Ph: " + dbDoctor.PhoneNo, "", 1, "left", "description"));
                upperTable.AddCell(CreateCell("", "", 1, "right", "description"));
                document.Add(upperTable);
                //
                document.Add(new Paragraph(""));
                document.Add(new Chunk("\n"));
                //Schedule Table
                float[] widths = new float[] { 30f, 100f, 100f, 50f, 50f, 70f, 100f };

                PdfPTable table = new PdfPTable(7);
                table.HorizontalAlignment = 0;
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(widths);

                table.AddCell(CreateCell("S#", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Vaccine", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Due Date", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Weight", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Height", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Circumference", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Injected", "backgroudLightGray", 1, "center", "scheduleRecords"));

                var imgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/img");
                foreach (var schedule in scheduleDoses)
                {
                    //PdfPCell cell = new PdfPCell(new Phrase(schedule.Date.Date.ToString("dd-MM-yyyy")));
                    //cell.Colspan = 2;
                    //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //cell.Border = 0;
                    //table.AddCell(cell);

                    foreach (var dose in schedule.Doses)
                    {
                        count++;
                        table.AddCell(CreateCell(count.ToString(), "", 1, "center", "scheduleRecords"));
                        table.AddCell(CreateCell(dose.Name, "", 1, "", "scheduleRecords"));

                        // select only injected dose schedule
                        var dbSchedule = dose.Schedules.Where(x => x.DoseId == dose.ID).FirstOrDefault();

                        table.AddCell(CreateCell(schedule.Date.Date.ToString("dd-MM-yyyy"), "", 1, "", "scheduleRecords"));
                        table.AddCell(CreateCell(dbSchedule.Weight.ToString(), "", 1, "", "scheduleRecords"));
                        table.AddCell(CreateCell(dbSchedule.Height.ToString(), "", 1, "", "scheduleRecords"));
                        table.AddCell(CreateCell(dbSchedule.Circle.ToString(), "", 1, "", "scheduleRecords"));


                        ////  add a image
                        //var isDone = dbSchedule.Where(x => x.IsDone).FirstOrDefault();
                        string injectionPath = "";
                        if (dbSchedule.IsDone)
                        {
                            injectionPath = "\\injectionFilled.png";
                        }
                        else
                        {
                            injectionPath = "\\injectionEmpty.png";
                        }
                        Image img = Image.GetInstance(imgPath + injectionPath);
                        img.ScaleAbsolute(2f, 2f);
                        PdfPCell imageCell = new PdfPCell(img, true);
                        imageCell.PaddingBottom = 5;
                        imageCell.Colspan = 1; // either 1 if you need to insert one cell
                        //imageCell.Border = 0;
                        imageCell.FixedHeight = 20f;
                        imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(imageCell);
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
                    VDConnectionString entities = new VDConnectionString();
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

                    upperTable.AddCell(CreateCell("Dr "+dbDoctor.FirstName+ dbDoctor.LastName, "bold", 1, "left", "description"));
                    //upperTable.AddCell(CreateCell("Invoice", "bold", 1, "right", "description"));
                    upperTable.AddCell(CreateCell("Invoice # " + dbDoctor.InvoiceNumber, "bold", 1, "right", "description"));
                    upperTable.AddCell(CreateCell("", "", 1, "left", "description"));
                    upperTable.AddCell(CreateCell("", "", 1, "right", "description"));
                    
                    upperTable.AddCell(CreateCell(dbChild.Clinic.Name, "bold", 1, "left", "description"));

                    //upperTable.AddCell(CreateCell("Clinic Ph: " + dbChild.Clinic.PhoneNumber, "noColor", 1, "left", "description"));

                    upperTable.AddCell(CreateCell("Date: " + DateTime.Now, "bold", 1, "right", "description"));


                    if (childDTO.IsConsultationFee)
                    {
                        consultaionFee = (int)dbDoctor.ConsultationFee;
                    }
                    //  upperTable.AddCell(CreateCell("Consultation Fee: " + consultaionFee, "noColor", 1, "left", "description"));
                    //upperTable.AddCell(CreateCell("", "", 1, "left", "description"));
                    //upperTable.AddCell(CreateCell("", "", 1, "right", "description"));

                    upperTable.AddCell(CreateCell("", "", 1, "left", "description"));
                    upperTable.AddCell(CreateCell("Bill To: "+ dbChild.Name, "noColor", 1, "right", "description"));
                    //upperTable.AddCell(CreateCell("", "", 1, "left", "description"));
                    //upperTable.AddCell(CreateCell("Father: " + dbChild.FatherName, "", 1, "right", "description"));
                    //upperTable.AddCell(CreateCell("", "", 1, "left", "description"));
                    //upperTable.AddCell(CreateCell("Child: " + dbChild.Name, "", 1, "right", "description"));
                    upperTable.AddCell(CreateCell("P: "+dbDoctor.PhoneNo, "bold", 1, "left", "description"));
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
                            if (schedule.IsDone && schedule.BrandId>0)
                            {
                                count++;
                                table.AddCell(CreateCell(count.ToString(), "", 1, "center", "invoiceRecords"));
                                table.AddCell(CreateCell(schedule.Dose.Vaccine.Name, "", 1, "center", "invoiceRecords"));
                                if (childDTO.IsBrand)
                                {
                                    table.AddCell(CreateCell(schedule.Brand.Name, "", 1, "center", "invoiceRecords"));
                                }
                                var brandAmounts = entities.BrandAmounts.Where(x => x.BrandID == schedule.BrandId).FirstOrDefault();
                                amount = amount + Convert.ToInt32(brandAmounts.Amount);
                                table.AddCell(CreateCell(brandAmounts.Amount.ToString(), "", 1, "right", "invoiceRecords"));
                            }

                        }

                    }

                    //table.AddCell(CreateCell("Total(PKR)", "", col - 1, "right", "invoiceRecords"));

                    //add consultancy fee
                    if (childDTO.IsConsultationFee)
                    {
                        amount = amount + (int)dbDoctor.ConsultationFee;
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
                    bottomTable.AddCell(CreateCell("Total Amount: "+amount.ToString()+"/-", "bold", 1, "right", "description"));

                    var imgcellLeft = CreateCell("", "", 1, "left", "description");
                    imgcellLeft.PaddingTop = 5;
                    bottomTable.AddCell(imgcellLeft);

                    var imgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/UserImages");

                    Image img = Image.GetInstance(imgPath + "\\" + dbDoctor.SignatureImage);
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
                                    FileName = childName.Replace(" ","") +"_FollowUp"+"_"+DateTime.Now.Date.ToString("MMMM-dd-yyyy")+".pdf"
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
            Font ColFont = FontFactory.GetFont(FontFactory.HELVETICA, 30, Font.BOLD);
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
                using (VDConnectionString entities = new VDConnectionString())
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
            VDConnectionString entities = new VDConnectionString();
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
                upperTable.AddCell(CreateCell("Date of Birth: "+child.DOB.ToString("dd-MM-yyyy"), "bold", 1, "right", "description"));

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
                float[] widths = new float[] { 30f, 200f,270f};
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
                    table.AddCell(new PdfPCell(new Phrase( (followUps.IndexOf(item) + 1) + "")));
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
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbChild = entities.Children.Where(c => c.ID == childDTO.ID).FirstOrDefault();
                    //give notification to old doctor
                    UserEmail.DoctorEmail(Mapper.Map<ChildDTO>(dbChild),"old");
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


    }
}
