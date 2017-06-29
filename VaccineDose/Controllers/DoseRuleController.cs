using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace VaccineDose.Controllers
{
    public class DoseRuleController : ApiController
    {
        public Response<IEnumerable<DoseRuleDTO>> Get(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbDoseRules = entities.DoseRules.Where(x=> x.DoseFrom== Id).ToList();
                IEnumerable<DoseRuleDTO> doseRulesDTOs = Mapper.Map<IEnumerable<DoseRuleDTO>>(dbDoseRules);
                return new Response<IEnumerable<DoseRuleDTO>>(true, null, doseRulesDTOs);
            }
        }
        public Response<DoseRuleDTO> Post(DoseRuleDTO doseRuleDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                DoseRule dbDoseRule = Mapper.Map<DoseRule>(doseRuleDTO);
                entities.DoseRules.Add(dbDoseRule);
                entities.SaveChanges();
                doseRuleDTO.ID = dbDoseRule.ID;
                return new Response<DoseRuleDTO>(true, null, doseRuleDTO);

            }
        }

        public Response<string> Delete(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbDoseRule = entities.DoseRules.Where(c => c.ID == Id).FirstOrDefault();
                entities.DoseRules.Remove(dbDoseRule);
                entities.SaveChanges();
                return new Response<string>(true, null, "record deleted");
            }
        }

    }
}
