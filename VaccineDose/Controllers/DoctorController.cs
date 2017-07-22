using System.Linq;
using System.Web.Http;
using AutoMapper;
using System.Collections.Generic;
using VaccineDose.Model;
using System;
using VaccineDose.App_Code;

namespace VaccineDose.Controllers
{
    public class DoctorController : ApiController
    {
        #region C R U D
        public Response<IEnumerable<DoctorDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbDoctor = entities.Doctors.Where(x => x.IsApproved == true).ToList();
                    IEnumerable<DoctorDTO> doctorDTOs = Mapper.Map<IEnumerable<DoctorDTO>>(dbDoctor);
                    return new Response<IEnumerable<DoctorDTO>>(true, null, doctorDTOs);
                }
            }
            catch(Exception e)
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
                    return new Response<IEnumerable<DoctorDTO>>(true, null, doctorDTOs);
                }
            }
            catch(Exception e)
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
                    return new Response<DoctorDTO>(true, null, doctorDTO);
                }
            }
            catch(Exception e)
            {
                return new Response<DoctorDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<DoctorDTO> Post(DoctorDTO doctorDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
               
                try
                {
                    Doctor doctorDB = Mapper.Map<Doctor>(doctorDTO);
                    entities.Doctors.Add(doctorDB);
                    User userDB = new User();
                    userDB.MobileNumber = doctorDTO.MobileNo;
                    userDB.Password = doctorDTO.Password;
                    userDB.UserType = "DOCTOR";
                    entities.Users.Add(userDB);
                    entities.SaveChanges();
                    doctorDTO.ID = doctorDB.ID;

                    // send email to doctor
                     UserEmail.DoctorEmail(doctorDTO);

                    return new Response<DoctorDTO>(true, null, doctorDTO);
                }
                catch (Exception ex)
                {
                    return new Response<DoctorDTO>(false, ex.Message, null);
                }
               
                               
            }
            
        }
       
        public Response<DoctorDTO> Put(int Id, DoctorDTO doctorDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbDoctor = entities.Doctors.Where(c => c.ID == Id).FirstOrDefault();
                    dbDoctor = Mapper.Map<DoctorDTO, Doctor>(doctorDTO, dbDoctor);
                    //entities.Entry<Doctor>(dbDoctor).State = System.Data.Entity.EntityState.Modified;
                    entities.SaveChanges();
                    return new Response<DoctorDTO>(true, null, doctorDTO);
                }
            }
            catch(Exception e)
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
            catch(Exception ex)
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
                    entities.SaveChanges();
                    return new Response<string>(true, null, "approved");
                }
            }
            catch(Exception e)
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
                        var clinicDTOs = Mapper.Map<List<ClinicDTO>>(dbClinics);
                        return new Response<IEnumerable<ClinicDTO>>(true, null, clinicDTOs);
                    }
                }
            }
            catch(Exception e)
            {
                return new Response<IEnumerable<ClinicDTO>>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        [Route("api/doctor/{id}/online-clinic")]
        public Response<ClinicDTO> GetOnlineClinicOfaDoctor(int id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var doctor = entities.Doctors.FirstOrDefault(c => c.ID == id);
                    if (doctor == null)
                        return new Response<ClinicDTO>(false, "Doctor not found", null);
                    else
                    {
                        var dbClinic = doctor.Clinics.Where(x => x.IsOnline==true).FirstOrDefault();
                        if (dbClinic == null)
                            return new Response<ClinicDTO>(false, "Clinic not found", null);
                        var clinicDTO = Mapper.Map<ClinicDTO>(dbClinic);
                        return new Response<ClinicDTO>(true, null, clinicDTO);
                    }
                }
            }
            catch (Exception e)
            {
                return new Response<ClinicDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        private static string GetMessageFromExceptionObject(Exception ex)
        {
            String message = ex.Message;
            message += (ex.InnerException != null) ? ("<br />" + ex.InnerException.Message) : "";
            message += (ex.InnerException.InnerException != null) ? ("<br />" + ex.InnerException.InnerException.Message) : "";
            return message;
        }
    }
}
