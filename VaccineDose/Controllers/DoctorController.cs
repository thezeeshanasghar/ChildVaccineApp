using System.Linq;
using System.Web.Http;
using AutoMapper;
using System.Collections.Generic;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    public class DoctorController : ApiController
    {
        #region C R U D
        public Response<IEnumerable<DoctorDTO>> Get()
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbDoctor = entities.Doctors.Where(x=>x.IsApproved==true).ToList();
                IEnumerable<DoctorDTO> doctorDTOs = Mapper.Map<IEnumerable<DoctorDTO>>(dbDoctor);
                return new Response<IEnumerable<DoctorDTO>>(true, null, doctorDTOs);
            }
        }
        [Route("~/api/doctor/unapproved")]
        public Response<IEnumerable<DoctorDTO>> GetUnApproved()
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbDoctor = entities.Doctors.Where(x => x.IsApproved == false).ToList();
                IEnumerable<DoctorDTO> doctorDTOs = Mapper.Map<IEnumerable<DoctorDTO>>(dbDoctor);
                return new Response<IEnumerable<DoctorDTO>>(true, null, doctorDTOs);
            }
        }

        public Response<DoctorDTO> Get(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbDoctor = entities.Doctors.Where(c => c.ID == Id).FirstOrDefault();
                DoctorDTO doctorDTO = Mapper.Map<DoctorDTO>(dbDoctor);
                return new Response<DoctorDTO>(true, null, doctorDTO);
            }
        }

        public Response<DoctorDTO> Post(DoctorDTO doctorDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
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
                
                return new Response<DoctorDTO>(true, null, doctorDTO);
                               
            }
            
        }
       
        public Response<DoctorDTO> Put(int Id, DoctorDTO doctorDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbDoctor = entities.Doctors.Where(c => c.ID == Id).FirstOrDefault();
                dbDoctor = Mapper.Map<DoctorDTO,Doctor>(doctorDTO,dbDoctor);
                //entities.Entry<Doctor>(dbDoctor).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
                return new Response<DoctorDTO>(true, null, doctorDTO);
            }
        }

        public Response<string> Delete(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbDoctor = entities.Doctors.Where(c => c.ID == Id).FirstOrDefault();
                entities.Doctors.Remove(dbDoctor);
                entities.SaveChanges();
                return new Response<string>(true, null, "record deleted");
            }
        }

        #endregion

        [Route("~/api/doctor/approve/{id}")]
        [HttpGet]
        public Response<string> Approve(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbDoctor = entities.Doctors.Where(c => c.ID == Id).FirstOrDefault();
                dbDoctor.IsApproved = true;
                entities.SaveChanges();
                return new Response<string>(true, null, "approved");
            }
        }

        [Route("api/doctor/{id}/clinics")]
        public Response<IEnumerable<ClinicDTO>> GetClinics(int id)
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

    }
}
