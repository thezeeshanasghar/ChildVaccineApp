using System.Linq;
using System.Web.Http;
using AutoMapper;

namespace VaccineDose.Controllers
{
    public class ClinicController : ApiController
    {
        #region C R U D
        public Response<ClinicDTO> Get(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbClinic = entities.Clinics.Where(c => c.ID == Id).FirstOrDefault();
                ClinicDTO ClinicDTO = Mapper.Map<ClinicDTO>(dbClinic);
                return new Response<ClinicDTO>(true, null, ClinicDTO);
            }
        }

        public Response<ClinicDTO> Post([FromBody] ClinicDTO clinicDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                Clinic clinicDb = Mapper.Map<Clinic>(clinicDTO);
                entities.Clinics.Add(clinicDb);
                entities.SaveChanges();
                clinicDTO.ID = clinicDb.ID;
                return new Response<ClinicDTO>(true, null, clinicDTO);
                
            }
        }
        public Response<ClinicDTO> Put(int Id, ClinicDTO clinicDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbClinic = entities.Clinics.Where(c => c.ID == Id).FirstOrDefault();
                dbClinic = Mapper.Map<ClinicDTO, Clinic>(clinicDTO, dbClinic);
                entities.SaveChanges();
                return new Response<ClinicDTO>(true, null, clinicDTO);
            }
        }

        public Response<string> Delete(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbClinic = entities.Clinics.Where(c => c.ID == Id).FirstOrDefault();
                entities.Clinics.Remove(dbClinic);
                entities.SaveChanges();
                return new Response<string>(true, null, "record deleted");
            }
        }

        #endregion

        
    }
}
