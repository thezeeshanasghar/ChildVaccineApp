﻿using System.Linq;
using System.Web.Http;
using AutoMapper;
using System.Collections.Generic;
using System;
using VaccineDose.App_Code;
using System.IO;
using System.Web;

namespace VaccineDose.Controllers
{
    public class DoctorController : BaseController
    {
        #region C R U D
        public Response<IEnumerable<DoctorDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
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
                using (VDConnectionString entities = new VDConnectionString())
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
                using (VDConnectionString entities = new VDConnectionString())
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
                using (VDConnectionString entities = new VDConnectionString())
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
                using (VDConnectionString entities = new VDConnectionString())
                {

                    // 3- send email to doctor
                    UserEmail.DoctorEmail(doctorDTO);

                    //generate SMS and save it to the db
                    string sms = UserSMS.DoctorSMS(doctorDTO);
                    Message m = new Message();
                    m.MobileNumber = doctorDTO.MobileNumber;
                    m.SMS = sms;
                    m.Status = "PENDING";
                    entities.Messages.Add(m);
                    entities.SaveChanges();

                    // 1- save User first
                    User userDB = new User();
                    userDB.MobileNumber = doctorDTO.MobileNumber;
                    userDB.Password = doctorDTO.Password;
                    userDB.CountryCode = doctorDTO.CountryCode;
                    userDB.UserType = "DOCTOR";
                    entities.Users.Add(userDB);
                    entities.SaveChanges();

                    // 1- save Doctor 
                    Doctor doctorDB = Mapper.Map<Doctor>(doctorDTO);
                    doctorDB.ValidUpto = null;
                    doctorDB.UserID = userDB.ID;
                    entities.Doctors.Add(doctorDB);
                    entities.SaveChanges();

                    doctorDTO.ID = doctorDB.ID;

                    // 4- check if clinicDto exsist; then save clinic as well
                    if (doctorDTO.ClinicDTO != null && !String.IsNullOrEmpty(doctorDTO.ClinicDTO.Name))
                    {
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
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbDoctor = entities.Doctors.Where(c => c.ID == Id).FirstOrDefault();
                    dbDoctor.FirstName = doctorDTO.FirstName;
                    dbDoctor.LastName = doctorDTO.LastName;
                    doctorDTO.IsApproved = doctorDTO.IsApproved;
                    dbDoctor.Email = doctorDTO.Email;
                    dbDoctor.PMDC = doctorDTO.PMDC;
                    dbDoctor.PhoneNo = doctorDTO.PhoneNo;
                    dbDoctor.ShowPhone = doctorDTO.ShowPhone;
                    dbDoctor.ShowMobile = doctorDTO.ShowMobile;
                    dbDoctor.ConsultationFee = doctorDTO.ConsultationFee;
         
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
                using (VDConnectionString entities = new VDConnectionString())
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
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbDoctor = entities.Doctors.Where(c => c.ID == Id).FirstOrDefault();
                    dbDoctor.IsApproved = true;
                    dbDoctor.ValidUpto = DateTime.Now.AddMonths(3);
                    entities.SaveChanges();

                    var vaccines = entities.Vaccines.ToList();
                    foreach (var vaccine in vaccines)
                    {
                        // add default schedule of doctor
                        var doses = vaccine.Doses;


                        for (int i = 0; i < doses.Count; i++)
                        {
                            var dose = doses.ElementAt(i);
                            DoctorSchedule ds = new DoctorSchedule();
                            ds.DoctorID = dbDoctor.ID;
                            ds.DoseID = dose.ID;
                            if (i == 0)
                                ds.GapInDays = 0;
                            else if (i == 1)
                                ds.GapInDays = 42;
                            else if (i == 2)
                                ds.GapInDays = 49;
                            else if (i == 3)
                                ds.GapInDays = 56;
                            else if (i == 4)
                                ds.GapInDays = 63;
                            else if (i == 5)
                                ds.GapInDays = 70;
                            else if (i == 6)
                                ds.GapInDays = 77;

                            entities.DoctorSchedules.Add(ds);
                            entities.SaveChanges();
                        }
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
                using (VDConnectionString entities = new VDConnectionString())
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
                            clinicDTO.Children = clinic.Children.Count();
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
                using (VDConnectionString entties = new VDConnectionString())
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
        public void UpdateUploadedImages(int id)
        {
            try
            {
                VDConnectionString entities = new VDConnectionString();
                var dbDoctor = entities.Doctors.Where(d=>d.ID== id).FirstOrDefault();
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
                }
            }
            catch (Exception e)
            {

            }


        }

        [HttpPost()]
        [Route("api/doctor/image")]
        public void UploadFiles()
        {
            try
            {
                VDConnectionString entities = new VDConnectionString();
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

    }

}
