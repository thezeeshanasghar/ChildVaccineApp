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
                    ChildDTO ClinicDTO = Mapper.Map<ChildDTO>(dbChild);
                    return new Response<ChildDTO>(true, null, ClinicDTO);
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
                    Child existingChild = entities.Children.Where(x => x.MobileNumber == childDTO.MobileNumber).FirstOrDefault();
                    if (existingChild == null)
                    {
                        User userDB = new User();
                        userDB.MobileNumber = childDTO.MobileNumber;
                        userDB.Password = childDTO.Password;
                        userDB.UserType = "PARENT";
                        entities.Users.Add(userDB);

                        entities.Children.Add(childDB);
                        entities.SaveChanges();
                    }
                    else
                    {
                        childDB.Password = childDTO.Password = existingChild.Password;
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

                    UserEmail.ParentEmail(entities.Children.Include("Clinic").Where(x => x.ID == childDTO.ID).FirstOrDefault());

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
                    dbChild.CountryCode = childDTO.CountryCode;
                    dbChild.MobileNumber = childDTO.MobileNumber;
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
                    var children = entities.Children.Where(c => c.MobileNumber == id).ToList();
                    if (children != null)
                    {
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
                document.Add(new Paragraph("Vaccine Schedule"));
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage=100;
                VDConnectionString entities = new VDConnectionString();

                var child = entities.Children.FirstOrDefault(c => c.ID == childId);
                var dbSchedules = child.Schedules.OrderBy(x => x.Date).ToList();
                 var scheduleDoses = from schedule in dbSchedules
                              group schedule.Dose by schedule.Date into scheduleDose
                              select new { Date = scheduleDose.Key, Doses = scheduleDose.ToList() };
                var imgPath= System.Web.Hosting.HostingEnvironment.MapPath("~/Content/img");
                foreach (var schedule in scheduleDoses)
                { 
                    PdfPCell cell = new PdfPCell(new Phrase(schedule.Date.Date.ToString()));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    //cell.Border = 0;
                    table.AddCell(cell);
                    foreach(var dose in schedule.Doses)
                    {
                        table.AddCell(dose.Name);
                        // add a image
                        Image img = Image.GetInstance(imgPath + "\\injectionEmpty.png");
                        
                        PdfPCell imageCell = new PdfPCell(img, true);
                        imageCell.PaddingBottom = 5;
                        imageCell.Colspan = 1; // either 1 if you need to insert one cell
                        imageCell.Border = 0;
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

        [HttpGet]
        [Route("api/child/checkUniqueMobile")]
        public HttpResponseMessage CheckUniqueMobile(string MobileNumber)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    Child childDB = entities.Children.Where(x => x.MobileNumber == MobileNumber).FirstOrDefault();
                    if (childDB == null)
                        return Request.CreateResponse((HttpStatusCode)200);
                    else
                    {
                        int HTTPResponse = 400;
                        var response = Request.CreateResponse((HttpStatusCode)HTTPResponse);
                        response.ReasonPhrase = "Mobile Number already exists";
                        return response;
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}
