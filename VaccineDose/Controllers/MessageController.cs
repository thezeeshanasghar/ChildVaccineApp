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
    public class MessageController : BaseController
    {
        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbSMS = entities.Messages.Where(x => x.Status == "PENDING").ToList();
                if (dbSMS.Count > 0)
                {
                    string SMS = "";
                    foreach (var sms in dbSMS)
                        SMS += sms.MobileNumber + "," + sms.SMS + "@";
                    response.Content = new StringContent(SMS);
                }
            }
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return response;

        }
    }
}