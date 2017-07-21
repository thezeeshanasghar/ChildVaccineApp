using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace VaccineDose.Controllers
{
    public class DoseRuleController : ApiController
    {
        //public Response<IEnumerable<DoseRuleDTO>> Get(int Id)
        //{
        //    try
        //    {
        //        using (VDConnectionString entities = new VDConnectionString())
        //        {
        //            var dbDoseRules = entities.DoseRules.Where(x => x.DoseFrom == Id).ToList();
        //            IEnumerable<DoseRuleDTO> doseRulesDTOs = Mapper.Map<IEnumerable<DoseRuleDTO>>(dbDoseRules);
        //            return new Response<IEnumerable<DoseRuleDTO>>(true, null, doseRulesDTOs);
        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        return new Response<IEnumerable<DoseRuleDTO>>(false, GetMessageFromExceptionObject(e), null);

        //    }
        //}
        //public Response<DoseRuleDTO> Post(DoseRuleDTO doseRuleDTO)
        //{
        //    try
        //    {
        //        using (VDConnectionString entities = new VDConnectionString())
        //        {
        //            DoseRule dbDoseRule = Mapper.Map<DoseRule>(doseRuleDTO);
        //            entities.DoseRules.Add(dbDoseRule);
        //            entities.SaveChanges();
        //            doseRuleDTO.ID = dbDoseRule.ID;
        //            return new Response<DoseRuleDTO>(true, null, doseRuleDTO);

        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        return new Response<DoseRuleDTO>(false, GetMessageFromExceptionObject(e), null);

        //    }
        //}

        //public Response<string> Delete(int Id)
        //{
        //    try
        //    {
        //        using (VDConnectionString entities = new VDConnectionString())
        //        {
        //            var dbDoseRule = entities.DoseRules.Where(c => c.ID == Id).FirstOrDefault();
        //            entities.DoseRules.Remove(dbDoseRule);
        //            entities.SaveChanges();
        //            return new Response<string>(true, null, "record deleted");
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        if (ex.InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
        //            return new Response<string>(false, "Cannot delete child because it schedule exits. Delete the child schedule first.", null);
        //        else
        //            return new Response<string>(false, GetMessageFromExceptionObject(ex), null);
        //    }
        //}
        //private static string GetMessageFromExceptionObject(Exception ex)
        //{
        //    String message = ex.Message;
        //    message += (ex.InnerException != null) ? ("<br />" + ex.InnerException.Message) : "";
        //    message += (ex.InnerException.InnerException != null) ? ("<br />" + ex.InnerException.InnerException.Message) : "";
        //    return message;
        //}

    }
}
