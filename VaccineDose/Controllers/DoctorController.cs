using System.Linq;
using System.Web.Http;
using AutoMapper;
using System.Collections.Generic;
using System;

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
                    var dbDoctor = entities.Doctors.Where(x => x.IsApproved == true).ToList();
                    IEnumerable<DoctorDTO> doctorDTOs = Mapper.Map<IEnumerable<DoctorDTO>>(dbDoctor);
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
                    // 1- save doctor
                    Doctor doctorDB = Mapper.Map<Doctor>(doctorDTO);
                    entities.Doctors.Add(doctorDB);
                    entities.SaveChanges();

                    // 2- add entry into user
                    User userDB = new User();
                    userDB.MobileNumber = doctorDTO.MobileNo;
                    userDB.Password = doctorDTO.Password;
                    userDB.UserType = "DOCTOR";
                    entities.Users.Add(userDB);
                    entities.SaveChanges();

                    // 3- send email to doctor
                    doctorDTO.ID = doctorDB.ID;
                    UserEmail.DoctorEmail(doctorDTO);

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
                return new Response<DoctorDTO>(false, ex.Message, null);
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
                    entities.SaveChanges();
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
                        var clinicDTOs = Mapper.Map<List<ClinicDTO>>(dbClinics);
                        return new Response<IEnumerable<ClinicDTO>>(true, null, clinicDTOs);
                    }
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ClinicDTO>>(false, GetMessageFromExceptionObject(e), null);

            }
        }
    }
}
