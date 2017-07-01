using System.Linq;
using System.Web.Http;
using AutoMapper;

namespace VaccineDose.Controllers
{
    public class DoseController : ApiController
    {
        #region C R U D
        public Response<DoseDTO> Get(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbDose = entities.Doses.Where(c => c.ID == Id).FirstOrDefault();
                DoseDTO doseDTO = Mapper.Map<DoseDTO>(dbDose);
                return new Response<DoseDTO>(true, null, doseDTO);
            }
        }

        public Response<DoseDTO> Post(DoseDTO doseDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                Dose doseDb = Mapper.Map<Dose>(doseDTO);
                entities.Doses.Add(doseDb);
                entities.SaveChanges();
                doseDTO.ID = doseDb.ID;
                return new Response<DoseDTO>(true, null, doseDTO);
                
            }
        }
        public Response<DoseDTO> Put(int Id, DoseDTO doseDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbDose = entities.Doses.Where(c => c.ID == Id).FirstOrDefault();
                dbDose.Name = doseDTO.Name;
                entities.SaveChanges();
                return new Response<DoseDTO>(true, null, doseDTO);
            }
        }

        public Response<string> Delete(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbDose = entities.Doses.Where(c => c.ID == Id).FirstOrDefault();
                entities.Doses.Remove(dbDose);
                entities.SaveChanges();
                return new Response<string>(true, null, "record deleted");
            }
        }

        #endregion

        
    }
}
