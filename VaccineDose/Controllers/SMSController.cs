using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    public class SMSController : BaseController
    {
        public Response<IEnumerable<SMSDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbSMS = entities.SMS.Where(x => x.Status == "PENDING").ToList();
                    IEnumerable<SMSDTO> smsDTOs = Mapper.Map<IEnumerable<SMSDTO>>(dbSMS);
                     if (smsDTOs != null)
                        {
                            return new Response<IEnumerable<SMSDTO>>(true, null, smsDTOs);
                        }
                        else
                        {
                            return new Response<IEnumerable<SMSDTO>>(false, null, null);
                        }
                     
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<SMSDTO>>(false, GetMessageFromExceptionObject(e), null);
            }

        }

        [HttpGet]
        [Route("api/sms/sms-string")]
        public Response<string> GetSMS()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbSMS = entities.SMS.Where(x => x.Status == "PENDING").ToList();
                    if (dbSMS.Count > 0)
                    {
                        var SMS = "";
                        foreach (var sms in dbSMS)
                        {
                            SMS += sms.MobileNumber + ":" + sms.Message + "_";
                        }
                        return new Response<string>(true, null, SMS);
                    }
                    else
                    {
                        return new Response<string>(false, null, null);
                    }

                }
            }
            catch (Exception e)
            {
                return new Response<string>(false, GetMessageFromExceptionObject(e), null);
            }

        }
    }
}
