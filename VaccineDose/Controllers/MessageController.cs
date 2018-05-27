using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using VaccineDose.Model;
using System.Linq;

namespace VaccineDose.Controllers
{
    [RoutePrefix("api/Message")]
    public class MessageController : BaseController
    {
        public Response<List<MessageDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbMessages = entities.Messages.ToList();
                    var messageDTOs = Mapper.Map<List<MessageDTO>>(dbMessages);
                    return new Response<List<MessageDTO>>(true, null, messageDTOs);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<MessageDTO>>(false, GetMessageFromExceptionObject(ex), null);

            }
            //var response = new HttpResponseMessage(HttpStatusCode.OK);

            //using (VDConnectionString entities = new VDConnectionString())
            //{
            //    var dbMessages = entities.Messages.ToList();
            //    var messageDTOs = Mapper.Map<List<MessageDTO>>(dbMessages);
            //    return Request.CreateResponse(HttpStatusCode.OK, messageDTOs);
            //    var dbSMS = entities.Messages.Where(x => x.Status == "PENDING").ToList();
            //    if (dbSMS.Count > 0)
            //    {
            //        string SMS = "";
            //        foreach (var sms in dbSMS)
            //            SMS += sms.MobileNumber + "," + sms.SMS + "@";
            //        response.Content = new StringContent(SMS);
            //    }
            //}
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            //return response;

        }

        [Route("{id}/doctor")]
        public Response<List<MessageDTO>> Get(int id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbMessages = entities.Messages.Where(x => x.UserID == id).ToList();
                    var messageDTOs = Mapper.Map<List<MessageDTO>>(dbMessages);
                    return new Response<List<MessageDTO>>(true, null, messageDTOs);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<MessageDTO>>(false, GetMessageFromExceptionObject(ex), null);

            }
          

        }
    }
}