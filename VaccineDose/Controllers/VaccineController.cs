using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.Collections;
using AutoMapper;
using System.Threading;

namespace VaccineDose.Controllers
{
    public class VaccineController : ApiController
    {
        #region C R U D

        public Response<IEnumerable<VaccineDTO>> Get()
        {
            using(VDConnectionString entities = new VDConnectionString())
            {
                var dbVaccines = entities.Vaccines.ToList();
                IEnumerable<VaccineDTO> vaccineDTOs = Mapper.Map<IEnumerable<VaccineDTO>>(dbVaccines);
                return new Response<IEnumerable<VaccineDTO>>(true,null, vaccineDTOs);
            }
        }

        public Response<VaccineDTO> Get(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbVaccine = entities.Vaccines.Where(c => c.ID == Id).FirstOrDefault();
                VaccineDTO vaccineDTO = Mapper.Map<VaccineDTO>(dbVaccine);
                return new Response<VaccineDTO>(true, null, vaccineDTO);
            }
        }

        public Response<VaccineDTO> Post(VaccineDTO vaccineDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                Vaccine vaccinedb = Mapper.Map<Vaccine>(vaccineDTO);
                entities.Vaccines.Add(vaccinedb);
                entities.SaveChanges();
                vaccineDTO.ID = vaccinedb.ID;
                return new Response<VaccineDTO>(true, null, vaccineDTO);
                
            }
        }
        public Response<VaccineDTO> Put(int Id, VaccineDTO vaccineDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbVaccine = entities.Vaccines.Where(c => c.ID == Id).FirstOrDefault();
                dbVaccine = Mapper.Map<VaccineDTO, Vaccine>(vaccineDTO, dbVaccine);
                entities.SaveChanges();
                return new Response<VaccineDTO>(true, null, vaccineDTO);
            }
        }

        public Response<string> Delete(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbVaccine = entities.Vaccines.Where(c => c.ID == Id).FirstOrDefault();
                entities.Vaccines.Remove(dbVaccine);
                entities.SaveChanges();
                return new Response<string>(true, null, "record deleted");
            }
        }

        #endregion

        [Route("api/vaccine/{id}/dosses")]
        public Response<IEnumerable<DoseDTO>> GetDosses(int id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var vaccine = entities.Vaccines.FirstOrDefault(c => c.ID == id);
                if (vaccine == null)
                    return new Response<IEnumerable<DoseDTO>>(false, "Vaccine not found",null);
                else
                {
                    var dbDosses = vaccine.Doses.ToList();
                    var dossesDTOs = Mapper.Map<List<DoseDTO>>(dbDosses);
                    return new Response<IEnumerable<DoseDTO>>(true, null, dossesDTOs);
                }
            }
        }

        //[Route("api/vaccine/{id}/dose-rules")]
        //public Response<IEnumerable<DoseRuleDTO>> GetDoseRules(int id)
        //{
        //    using (VDConnectionString entities = new VDConnectionString())
        //    {
        //        var vaccine = entities.Vaccines.FirstOrDefault(c => c.ID == id);
        //        if (vaccine == null)
        //            return new Response<IEnumerable<DoseRuleDTO>>(false, "Vaccine not found", null);
        //        else
        //        {
        //            var dbDoseRules = vaccine.DoseRules.ToList();
        //            var doseRulesDTOs = Mapper.Map<List<DoseRuleDTO>>(dbDoseRules);
        //            return new Response<IEnumerable<DoseRuleDTO>>(true, null, doseRulesDTOs);
        //        }
        //    }
        //}
    }
}
