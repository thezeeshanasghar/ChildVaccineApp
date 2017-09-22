using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;


namespace VaccineDose.Controllers
{
    public class MessageController : ApiController
    {
        // GET: Message
        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(
                @"03465430413,Respected Parent's
Your Son Harib has been registered at Clinic G-10
ID: 3465430413
Password: 0778
Clinic Phone Number 03465430414
Doctor Phone Number: 03465430413
@
03335196658,Respected Parent's
Your Son Harib has been registered at Clinic G-10
ID: 3465430413
Password: 0778
Clinic Phone Number 03465430414
Doctor Phone Number: 03465430413");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return response;

        }
    }
}