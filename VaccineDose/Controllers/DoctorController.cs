using System.Linq;
using System.Web.Http;
using AutoMapper;
using System.Collections.Generic;
using System;
using VaccineDose.App_Code;
using System.IO;
using System.Web;
using System.Globalization;
using System.Net.Http;
using System.Net;
using WebApi.OutputCache.V2;

namespace VaccineDose.Controllers
{
    [AutoInvalidateCacheOutput]
    public class DoctorController : BaseController
    {
        #region C R U D
        public Response<IEnumerable<DoctorDTO>> Get()
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbDoctor = entities.Doctors.ToList();
                    IEnumerable<DoctorDTO> doctorDTOs = Mapper.Map<IEnumerable<DoctorDTO>>(dbDoctor);
                    foreach (var item in doctorDTOs)
                        item.MobileNumber = dbDoctor.Where(x => x.ID == item.ID).First().User.MobileNumber;
                    return new Response<IEnumerable<DoctorDTO>>(true, null, doctorDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<DoctorDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        [Route("~/api/doctor/approved")]
        public Response<IEnumerable<DoctorDTO>> GetApproved()
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbDoctor = entities.Doctors.Where(x => x.IsApproved == true).ToList();
                    IEnumerable<DoctorDTO> doctorDTOs = Mapper.Map<IEnumerable<DoctorDTO>>(dbDoctor);
                    foreach (var item in doctorDTOs)
                        item.MobileNumber = dbDoctor.Where(x => x.ID == item.ID).First().User.MobileNumber;
                    return new Response<IEnumerable<DoctorDTO>>(true, null, doctorDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<DoctorDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        [Route("~/api/doctor/unapproved")]
        public Response<IEnumerable<DoctorDTO>> GetUnApproved()
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbDoctor = entities.Doctors.Where(x => x.IsApproved == false).ToList();
                    IEnumerable<DoctorDTO> doctorDTOs = Mapper.Map<IEnumerable<DoctorDTO>>(dbDoctor);
                    foreach (var item in doctorDTOs)
                        item.MobileNumber = dbDoctor.Where(x => x.ID == item.ID).First().User.MobileNumber;
                    return new Response<IEnumerable<DoctorDTO>>(true, null, doctorDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<DoctorDTO>>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<DoctorDTO> Get(int Id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbDoctor = entities.Doctors.Where(c => c.ID == Id).FirstOrDefault();
                    DoctorDTO doctorDTO = Mapper.Map<DoctorDTO>(dbDoctor);
                    doctorDTO.MobileNumber = dbDoctor.User.MobileNumber;
                    return new Response<DoctorDTO>(true, null, doctorDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<DoctorDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<DoctorDTO> Post(DoctorDTO doctorDTO)
        {
            try
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                doctorDTO.FirstName = textInfo.ToTitleCase(doctorDTO.FirstName);
                doctorDTO.LastName = textInfo.ToTitleCase(doctorDTO.LastName);
                doctorDTO.DisplayName = textInfo.ToTitleCase(doctorDTO.DisplayName);
                using (VDEntities entities = new VDEntities())
                {

                    // 1- send email to doctor
                    UserEmail.DoctorEmail(doctorDTO);

                    // 2- save User first
                    User userDB = new User();
                    userDB.MobileNumber = doctorDTO.MobileNumber;
                    userDB.Password = doctorDTO.Password;
                    userDB.CountryCode = doctorDTO.CountryCode;
                    userDB.UserType = "DOCTOR";
                    entities.Users.Add(userDB);
                    entities.SaveChanges();

                    // 2- save Doctor 
                    Doctor doctorDB = Mapper.Map<Doctor>(doctorDTO);
                    doctorDB.ValidUpto = null;
                    doctorDB.UserID = userDB.ID;
                    entities.Doctors.Add(doctorDB);
                    entities.SaveChanges();
                    doctorDTO.ID = doctorDB.ID;

                    //generate SMS and save it to the db
                    UserSMS.DoctorSMS(doctorDTO);

                    // 4- check if clinicDto exsist; then save clinic as well
                    if (doctorDTO.ClinicDTO != null && !String.IsNullOrEmpty(doctorDTO.ClinicDTO.Name))
                    {
                        doctorDTO.ClinicDTO.Name = textInfo.ToTitleCase(doctorDTO.ClinicDTO.Name);

                        doctorDTO.ClinicDTO.DoctorID = doctorDB.ID;

                        Clinic clinicDB = Mapper.Map<Clinic>(doctorDTO.ClinicDTO);
                        entities.Clinics.Add(clinicDB);
                        entities.SaveChanges();

                        doctorDTO.ClinicDTO.ID = clinicDB.ID;
                    }
                }
                return new Response<DoctorDTO>(true, null, doctorDTO);
            }
            catch (Exception ex)
            {
                return new Response<DoctorDTO>(false, GetMessageFromExceptionObject(ex), null);
            }
        }



        public Response<DoctorDTO> Put(int Id, DoctorDTO doctorDTO)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbDoctor = entities.Doctors.Where(c => c.ID == Id).FirstOrDefault();
                    dbDoctor.FirstName = doctorDTO.FirstName;
                    dbDoctor.LastName = doctorDTO.LastName;
                    dbDoctor.DisplayName = doctorDTO.DisplayName;
                    dbDoctor.IsApproved = doctorDTO.IsApproved;
                    dbDoctor.Email = doctorDTO.Email;
                    dbDoctor.PMDC = doctorDTO.PMDC;
                    dbDoctor.PhoneNo = doctorDTO.PhoneNo;
                    dbDoctor.ShowPhone = doctorDTO.ShowPhone;
                    dbDoctor.ShowMobile = doctorDTO.ShowMobile;
                    dbDoctor.Qualification = doctorDTO.Qualification;
                    dbDoctor.AdditionalInfo = doctorDTO.AdditionalInfo;

                    //dbDoctor = Mapper.Map<DoctorDTO, Doctor>(doctorDTO, dbDoctor);
                    //entities.Entry<Doctor>(dbDoctor).State = System.Data.Entity.EntityState.Modified;
                    entities.SaveChanges();
                    return new Response<DoctorDTO>(true, null, doctorDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<DoctorDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<string> Delete(int Id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbDoctor = entities.Doctors.Where(c => c.ID == Id).FirstOrDefault();
                    entities.Doctors.Remove(dbDoctor);
                    entities.SaveChanges();
                    return new Response<string>(true, null, "record deleted");
                }
            }
            catch (Exception ex)
            {
                {
                    if (ex.InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        return new Response<string>(false, "Cannot delete child because it schedule exits. Delete the child schedule first.", null);
                    else
                        return new Response<string>(false, GetMessageFromExceptionObject(ex), null);
                }
            }
        }

        #endregion

        [Route("~/api/doctor/approve/{id}")]
        [HttpGet]
        public Response<string> Approve(int Id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbDoctor = entities.Doctors.Where(c => c.ID == Id).FirstOrDefault();
                    dbDoctor.IsApproved = true;
                    dbDoctor.ValidUpto = DateTime.UtcNow.AddHours(5).AddMonths(3);
                    entities.SaveChanges();

                    var vaccines = entities.Vaccines.ToList();
                    foreach (var vaccine in vaccines)
                    {
                        // add default brands amount and inventory count of doctor
                        var brands = vaccine.Brands;
                        foreach (var brand in brands)
                        {
                            BrandAmount ba = new BrandAmount();
                            ba.Amount = 0;
                            ba.DoctorID = dbDoctor.ID;
                            ba.BrandID = brand.ID;
                            entities.BrandAmounts.Add(ba);

                            BrandInventory bi = new BrandInventory();
                            bi.Count = 0;
                            bi.DoctorID = dbDoctor.ID;
                            bi.BrandID = brand.ID;
                            entities.BrandInventories.Add(bi);
                            entities.SaveChanges();
                        }
                    }

                    return new Response<string>(true, null, "approved");
                }
            }
            catch (Exception e)
            {
                return new Response<string>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        [Route("api/doctor/{id}/clinics")]
        public Response<IEnumerable<ClinicDTO>> GetAllClinicsOfaDoctor(int id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var doctor = entities.Doctors.FirstOrDefault(c => c.ID == id);
                    if (doctor == null)
                        return new Response<IEnumerable<ClinicDTO>>(false, "Doctor not found", null);
                    else
                    {
                        var dbClinics = doctor.Clinics.ToList();
                        List<ClinicDTO> clinicDTOs = new List<ClinicDTO>();
                        foreach (var clinic in dbClinics)
                        {
                            ClinicDTO clinicDTO = Mapper.Map<ClinicDTO>(clinic);
                            clinicDTO.childrenCount = clinic.Children.Count();
                            clinicDTOs.Add(clinicDTO);
                        }
                        //var clinicDTOs = Mapper.Map<List<ClinicDTO>>(dbClinics);
                        return new Response<IEnumerable<ClinicDTO>>(true, null, clinicDTOs);
                    }
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ClinicDTO>>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        [HttpPut]
        [Route("api/doctor/{id}/validUpto")]
        public Response<DoctorDTO> UpdateDate(int id, DoctorDTO doctorDTO)
        {
            try
            {
                using (VDEntities entties = new VDEntities())
                {
                    var dbDoctor = entties.Doctors.Where(x => x.ID == id).FirstOrDefault();
                    dbDoctor.ValidUpto = doctorDTO.ValidUpto;
                    entties.SaveChanges();
                    DoctorDTO doctorDTOs = Mapper.Map<DoctorDTO>(dbDoctor);
                    return new Response<DoctorDTO>(true, null, doctorDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<DoctorDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        [HttpPost()]
        [Route("api/doctor/{id}/update-images")]
        public Response<DoctorDTO> UpdateUploadedImages(int id)
        {
            try
            {
                VDEntities entities = new VDEntities();
                var dbDoctor = entities.Doctors.Where(d => d.ID == id).FirstOrDefault();
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var httpPostedProfileImage = HttpContext.Current.Request.Files["ProfileImage"];
                    var httpPostedSignatureImage = HttpContext.Current.Request.Files["SignatureImage"];

                    if (httpPostedProfileImage != null)
                    {

                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/UserImages"), httpPostedProfileImage.FileName);
                        httpPostedProfileImage.SaveAs(fileSavePath);
                        dbDoctor.ProfileImage = httpPostedProfileImage.FileName;
                    }
                    if (httpPostedSignatureImage != null)
                    {
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/UserImages"), httpPostedSignatureImage.FileName);
                        httpPostedSignatureImage.SaveAs(fileSavePath);
                        dbDoctor.SignatureImage = httpPostedSignatureImage.FileName;
                    }
                    entities.SaveChanges();
                    return new Response<DoctorDTO>(true, null, null);
                }
                return new Response<DoctorDTO>(false, "invalid files in request", null);
            }
            catch (Exception e)
            {
                return new Response<DoctorDTO>(false, "Image not uploaded. \n" + GetMessageFromExceptionObject(e), null);
            }


        }

        [HttpPost()]
        [Route("api/doctor/image")]
        public void UploadFiles()
        {
            try
            {
                VDEntities entities = new VDEntities();
                var dbDoctors = entities.Doctors.ToList();
                var doctor = dbDoctors[dbDoctors.Count - 1];
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedProfileImage = HttpContext.Current.Request.Files["ProfileImage"];
                    var httpPostedSignatureImage = HttpContext.Current.Request.Files["SignatureImage"];

                    if (httpPostedProfileImage != null)
                    {
                        // Validate the uploaded image(optional)
                        // Get the complete file path
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/UserImages"), httpPostedProfileImage.FileName);
                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedProfileImage.SaveAs(fileSavePath);
                        //doctor.ProfileImage = fileSavePath;
                        doctor.ProfileImage = httpPostedProfileImage.FileName;
                    }
                    if (httpPostedSignatureImage != null)
                    {
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/UserImages"), httpPostedSignatureImage.FileName);
                        httpPostedSignatureImage.SaveAs(fileSavePath);
                        doctor.SignatureImage = httpPostedSignatureImage.FileName;
                    }
                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }


        }

        //show doctors and their clinics on change doctor page and also show child data on it
        [Route("api/doctor/{id}/doctor-clinics")]
        public Response<IEnumerable<DoctorDTO>> GetDoctorClinics(int id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbDoctor = entities.Doctors.Where(x => x.IsApproved == true).ToList();
                    var dbChild = entities.Children.Where(x => x.ID == id).FirstOrDefault();

                    IEnumerable<DoctorDTO> doctorDTOs = Mapper.Map<IEnumerable<DoctorDTO>>(dbDoctor);
                    foreach (var doctor in doctorDTOs)
                    {
                        doctor.ChildDTO = Mapper.Map<ChildDTO>(dbChild);
                    }
                    return new Response<IEnumerable<DoctorDTO>>(true, null, doctorDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<DoctorDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }


        [Route("api/doctor/{id}/{pageSize}/{currentPage}/childs/")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public Response<IEnumerable<ChildDTO>> GetAllChildsOfaDoctor(int id,int pageSize,int currentPage, [FromUri] string searchKeyword = "")
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var doctor = entities.Doctors.FirstOrDefault(c => c.UserID == id);
                    if (doctor == null)
                        return new Response<IEnumerable<ChildDTO>>(false, "Doctor not found", null);
                    else
                    {
                        List<ChildDTO> childDTOs = new List<ChildDTO>();
                        var doctorClinics = doctor.Clinics;
                        foreach (var clinic in doctorClinics)
                        {
                            if (!String.IsNullOrEmpty(searchKeyword))
                            {
                                if (searchKeyword.StartsWith("+")) searchKeyword = searchKeyword.Substring(1);
                                if (searchKeyword.StartsWith("0")) searchKeyword = searchKeyword.Substring(1);
                                if (searchKeyword.StartsWith("00")) searchKeyword = searchKeyword.Substring(2);
                                if (searchKeyword.StartsWith("92")) searchKeyword = searchKeyword.Substring(2);
                                childDTOs.AddRange(Mapper.Map<List<ChildDTO>>(clinic.Children.Where(x => x.Name.ToLower()
                                .Contains(searchKeyword.ToLower()) || x.FatherName.ToLower().Contains(searchKeyword.ToLower()) ||
                                 x.User.MobileNumber.Contains(searchKeyword.ToLower())).ToList<Child>()));
                                currentPage = 0;
                            }
                            else
                                childDTOs.AddRange(Mapper.Map<List<ChildDTO>>(clinic.Children.ToList<Child>()));
                        }
                        foreach (var item in childDTOs)
                        {
                            var dbChild = entities.Children.Where(x => x.ID == item.ID).FirstOrDefault();
                            item.MobileNumber = dbChild.User.CountryCode + dbChild.User.MobileNumber;
                        }
                        return new Response<IEnumerable<ChildDTO>>(true, null, childDTOs.OrderByDescending(x => x.ID).ToList().Skip(pageSize * currentPage).Take(pageSize));
                    }
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ChildDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        [HttpPut]
        [Route("api/doctor/{id}/update-permission")]
        public Response<DoctorDTO> UpdateDoctorPermission(int id, DoctorDTO doctorDTO)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbDoctor = entities.Doctors.Where(c => c.ID == id).FirstOrDefault();
                    dbDoctor.AllowInvoice = doctorDTO.AllowInvoice;
                    dbDoctor.AllowFollowUp = doctorDTO.AllowFollowUp;
                    dbDoctor.AllowChart = doctorDTO.AllowChart;
                    dbDoctor.AllowInventory = doctorDTO.AllowInventory;
                    dbDoctor.SMSLimit = doctorDTO.SMSLimit;

                    entities.SaveChanges();
                    return new Response<DoctorDTO>(true, "Record is successfully updated", doctorDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<DoctorDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        [HttpGet]
        [Route("api/doctor/checkUniqueEmail")]
        public HttpResponseMessage CheckUniqueEmail(string Email)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    Doctor doctorDB = entities.Doctors.Where(x => x.Email == Email).FirstOrDefault();
                    if (doctorDB == null)
                        return Request.CreateResponse((HttpStatusCode)200);
                    else
                    {
                        int HTTPResponse = 400;
                        var response = Request.CreateResponse((HttpStatusCode)HTTPResponse);
                        response.ReasonPhrase = "Email already in use";
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
