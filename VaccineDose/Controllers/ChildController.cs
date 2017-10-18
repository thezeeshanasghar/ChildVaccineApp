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
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    //[RoutePrefix("api/child")]
    public class ChildController : BaseController
    {
        #region C R U D

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

                    // get doctor schedule and apply it to child and save in Schedule page
                    Clinic clinic = entities.Clinics.Where(x => x.ID == childDTO.ClinicID).FirstOrDefault();
                    Doctor doctor = clinic.Doctor;
                    IEnumerable<DoctorSchedule> dss = doctor.DoctorSchedules;
                    foreach (DoctorSchedule ds in dss)
                    {
                        Schedule cvd = new Schedule();
                        cvd.ChildId = childDTO.ID;
                        cvd.DoseId = ds.DoseID;
                        cvd.IsDone = false;
                        cvd.Date = childDTO.DOB.AddDays(ds.GapInDays);
                        entities.Schedules.Add(cvd);
                        entities.SaveChanges();
                    }
                    Child c = entities.Children.Include("Clinic").Where(x => x.ID == childDTO.ID).FirstOrDefault();
                    UserEmail.ParentEmail(c);
                    
                    // generate SMS and save it to the db
                    string sms = UserEmail.ParentSMS(c);
                    Message m = new Message();
                    m.MobileNumber = c.User.MobileNumber;
                    m.SMS = sms;
                    m.Status = "PENDING";
                    entities.Messages.Add(m);
                    entities.SaveChanges();
                    // 

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
                    var dbChild = entities.Children.Where(c => c.ID == Id).FirstOrDefault();
                    entities.Children.Remove(dbChild);
                    entities.SaveChanges();
                    return new Response<string>(true, null, "record deleted");
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


        [Route("api/child/{id}/schedule")]
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
        [Route("api/child/{id}/GetChildAgainstMobile")]
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
        [Route("api/child/{id}/GetCustomScheduleAgainsClinic")]
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
        [HttpGet]
        [Route("api/child/{id}/download-pdf")]
        public HttpResponseMessage DownloadPDF(int id)
        {
            var stream = CreatePdf(id);

            return new HttpResponseMessage
            {
                Content = new StreamContent(stream)
                {
                    Headers =
                            {
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
        private Stream CreatePdf(int childId)
        {
            using (var document = new Document(PageSize.A4, 50, 50, 25, 25))
            {
                var output = new MemoryStream();

                var writer = PdfWriter.GetInstance(document, output);
                writer.CloseStream = false;

                document.Open();
                Font ColFont = FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD);
                Chunk chunkCols = new Chunk("Vaccine Schedule", ColFont);
                document.Add(new Paragraph(chunkCols));
                document.Add(new Paragraph(new Chunk("")));
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;
                VDConnectionString entities = new VDConnectionString();

                var child = entities.Children.FirstOrDefault(c => c.ID == childId);
                var dbSchedules = child.Schedules.OrderBy(x => x.Date).ToList();
                var scheduleDoses = from schedule in dbSchedules
                                    group schedule.Dose by schedule.Date into scheduleDose
                                    select new { Date = scheduleDose.Key, Doses = scheduleDose.ToList() };
                var imgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/img");
                foreach (var schedule in scheduleDoses)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(schedule.Date.Date.ToString("dd-MM-yyyy")));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Border = 0;
                    table.AddCell(cell);
                    foreach (var dose in schedule.Doses)
                    {
                        PdfPCell cell1 = new PdfPCell(new Phrase(dose.Name));
                        cell1.Border = 0;
                        table.AddCell(cell1);
                        // add a image
                        Image img = Image.GetInstance(imgPath + "\\injectionEmpty.png");
                        img.ScaleAbsolute(2f, 2f);
                        PdfPCell imageCell = new PdfPCell(img, true);
                        imageCell.PaddingBottom = 5;
                        imageCell.Colspan = 1; // either 1 if you need to insert one cell
                        imageCell.Border = 0;
                        imageCell.FixedHeight = 40f;
                        imageCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table.AddCell(imageCell);
                    }


                    //imageCell.setHorizontalAlignment(Element.ALIGN_CENTER);

                }

                document.Add(table);
                document.Close();

                output.Seek(0, SeekOrigin.Begin);

                return output;
            }
        }

        [HttpPost]
        [Route("api/child/invoice")]
        public HttpResponseMessage GenerateInvoicePDF(ChildDTO childDTO)
        {
            try
            {
                Stream stream;
                int amount = 0;
                int count = 0;
                int col = 3;
                

                using (var document = new Document(PageSize.A4, 50, 50, 25, 25))
                {
                    var output = new MemoryStream();

                    var writer = PdfWriter.GetInstance(document, output);
                    writer.CloseStream = false;

                    document.Open();
              
                    Font ColFont = FontFactory.GetFont(FontFactory.HELVETICA, 25, Font.BOLD);
                    Chunk chunkCols = new Chunk("My Vaccs", ColFont);
                    Paragraph chunkParagraph = new Paragraph();
                    chunkParagraph.Alignment = Element.ALIGN_CENTER;
                    chunkParagraph.Add(chunkCols);
                    document.Add(chunkParagraph);
                    document.Add(new Paragraph(""));
                    document.Add(new Chunk("\n"));

                    VDConnectionString entities = new VDConnectionString();
                    var dbDoctor = entities.Doctors.Where(x => x.ID == childDTO.DoctorID).FirstOrDefault();
                    dbDoctor.InvoiceNumber = (dbDoctor.InvoiceNumber > 0) ? dbDoctor.InvoiceNumber + 1 : 1;

                    Font invoiceNumberFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD);
                    Chunk invoiceNumberLable = new Chunk("Invoice#: ", invoiceNumberFont);
                    Phrase invoicePhrase = new Phrase();
                    invoicePhrase.Add(invoiceNumberLable);

                    //Font invoiceNumberValueFont = FontFactory.GetFont(FontFactory.HELVETICA,10);
                    Chunk invoiceNumberValueLable = new Chunk(dbDoctor.InvoiceNumber.ToString());
                    Phrase invoiceNumberValuePhrase = new Phrase();
                    invoiceNumberValuePhrase.Add(invoiceNumberValueLable);

                    Paragraph invoiceParagraph = new Paragraph();
                    invoiceParagraph.Add(invoicePhrase);
                    invoiceParagraph.Add(invoiceNumberValuePhrase);
                    document.Add(invoiceParagraph);


                    if (childDTO.IsConsultationFee)
                    {
                        Font consultationFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD);
                        Chunk consaltationLable = new Chunk("Consaltation Fee: ", consultationFont);
                        Phrase consPhrase = new Phrase();
                        consPhrase.Add(consaltationLable);
 
                        Chunk consultationValueLable = new Chunk(dbDoctor.ConsultationFee.ToString());
                        Phrase consValuePhrase = new Phrase();
                        consValuePhrase.Add(consultationValueLable);

                        Paragraph consultaionParagraph = new Paragraph();
                        consultaionParagraph.Add(consPhrase);
                        consultaionParagraph.Add(consValuePhrase);
                        document.Add(consultaionParagraph);
                       
                    }

                    document.Add(new Paragraph(""));
                     document.Add(new Chunk("\n"));

                    float[] widths = new float[] { 30f, 200f, 100f};
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

                    PdfPCell countHeader = new PdfPCell(new Phrase("#"));
                    countHeader.BackgroundColor = GrayColor.LIGHT_GRAY;
                    countHeader.Colspan = 1;
                    countHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(countHeader);

                    PdfPCell doseHeader = new PdfPCell(new Phrase("Vaccine"));
                    doseHeader.BackgroundColor = GrayColor.LIGHT_GRAY;
                    doseHeader.Colspan = 1;
                    doseHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(doseHeader);
                    if (childDTO.IsBrand)
                    {
                        PdfPCell brandHeader = new PdfPCell(new Phrase("Brand"));
                        brandHeader.BackgroundColor = GrayColor.LIGHT_GRAY;
                        brandHeader.Colspan = 1;
                        brandHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(brandHeader);
                    }
                  

                    PdfPCell amountHeader = new PdfPCell(new Phrase("Price"));
                    amountHeader.BackgroundColor = GrayColor.LIGHT_GRAY;
                    amountHeader.Colspan = 1;
                    amountHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(amountHeader);

                  
                    var dbSchedules = entities.Schedules.Include("Dose").Include("Brand").Where(x => x.ChildId == childDTO.ID).ToList();
                      
                    if (dbSchedules.Count != 0)
                    {
                       
                        foreach (var schedule in dbSchedules)
                        {
                        
                            if (schedule.Date.ToString("dd/MM/yyyy") == childDTO.InvoiceDate.ToString("MM/dd/yyyy") && schedule.IsDone)
                            {
                                count++;
                                PdfPCell countCell = new PdfPCell(new Phrase(count.ToString()));
                                countCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(countCell);

                                PdfPCell doseCell = new PdfPCell(new Phrase(schedule.Dose.Vaccine.Name));
                                table.AddCell(doseCell);
                                if (childDTO.IsBrand)
                                {
                                    PdfPCell brandCell = new PdfPCell(new Phrase(schedule.Brand.Name));
                                    table.AddCell(brandCell);
                                }
                                var brandAmounts = entities.BrandAmounts.Where(x => x.BrandID == schedule.BrandId).FirstOrDefault();
                                amount = amount + Convert.ToInt32(brandAmounts.Amount);
                                PdfPCell amountCell = new PdfPCell(new Phrase(brandAmounts.Amount.ToString()));
                                amountCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                table.AddCell(amountCell);
                            }
                          
                        }
                       
                    }

                    
                    PdfPCell totalCell = new PdfPCell(new Phrase("Total(PKR)"));
                    totalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    totalCell.Colspan = col-1;
                    table.AddCell(totalCell);
                    //add consultancy fee
                    if (childDTO.IsConsultationFee)
                    {
                        amount = amount + (int)dbDoctor.ConsultationFee;
                    }
                    PdfPCell totalAmountCell = new PdfPCell(new Phrase(amount.ToString()));
                    totalAmountCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    totalAmountCell.Colspan = 1;
                    table.AddCell(totalAmountCell);
                    entities.SaveChanges();
                    document.Add(table);
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
                                    FileName = "myfile.pdf"
                                }
                            }
                    },
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
           
        }

        [HttpPost]
        [Route("api/child/followup")]
        public Response<List<FollowUpDTO>> GetFollowUp(FollowUpDTO followUpDto)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbFollowUps = entities.FollowUps.Where(f => f.DoctorID == followUpDto.DoctorID && f.ChildID == followUpDto.ChildID).ToList();
                    List<FollowUpDTO> followUpDTOs = Mapper.Map<List<FollowUpDTO>>(dbFollowUps);
                    return new Response<List<FollowUpDTO>>(true, null, followUpDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<List<FollowUpDTO>>(false, GetMessageFromExceptionObject(e), null);

            }

        }
    }
}
