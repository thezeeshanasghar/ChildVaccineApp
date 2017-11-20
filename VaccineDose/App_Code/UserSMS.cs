using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace VaccineDose.App_Code
{
    public class UserSMS
    {
        public static string DoctorSMS(DoctorDTO doctor)
        {
            string body = "Hi Dr. " + doctor.FirstName + " " + doctor.LastName + " \n"
                + "You are registered at Vaccs.io\n\n"
                + "Your account credentials are: \n"
                + "ID/Mobile Number: " + doctor.MobileNumber + "\n"
                + "Password: " + doctor.Password + "\n"
                + "http://vaccs.io/";
            SendSMS(doctor.CountryCode, doctor.MobileNumber, doctor.Email, body);
            return body;
        }
        public static string ParentSMS(Child child)
        {
            string body = "Dear Parents\n";
            if (child.Gender == "Boy")
                body += "Your Son " + child.Name;

            if (child.Gender == "Girl")
                body += "Your Daughter " + child.Name;

            body += " has been registered at Clinic ";
            body += child.Clinic.Name + "\n";

            body += "ID: " + child.User.MobileNumber + "\n Password: " + child.User.Password
                 + "\nClinic# " + child.Clinic.PhoneNumber + "\n"
                 + "http://vaccs.io/";

            SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, body);
            return body;
        }

        public static string SendSMS(string CountryCode, string MobileNumber, string Email, string text)
        {
            // SMSified API endpoint.
            //http://58.65.138.38:8181/sc/smsApi/sendSms?username=vccsio&password=123456&mobileNumber=923345022330&message=Test%20Sherjeel&mask=VACCS%20IO
            string webTarget = "http://58.65.138.38:8181/sc/smsApi/sendSms?username=vccsio&password=123456&mobileNumber={0}&message={1}&mask=VACCS%20IO";

            // Create new HTTP request.
            string url = String.Format(webTarget, CountryCode + MobileNumber, HttpUtility.HtmlEncode(text));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            string responseStr = "";
            using (StreamReader reader = new StreamReader(stream))
            {
                responseStr = reader.ReadToEnd();
                Console.WriteLine("Zeeshan");
                Console.WriteLine(responseStr);
                //dynamic jObj = JsonConvert.DeserializeObject(responseStr);
                //Console.WriteLine(jObj.returnString);
            }
            return responseStr;
        }
    }
}