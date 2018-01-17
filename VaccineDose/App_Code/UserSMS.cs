using System;

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
            var response = SendSMS(doctor.CountryCode, doctor.MobileNumber, doctor.Email, body);
            return response;
        }
        public static string ParentSMS(Child child)
        {
            string sms1 = "Dear Parents\n";
            if (child.Gender == "Boy")
                sms1 += "Your Son " + child.Name;

            if (child.Gender == "Girl")
                sms1 += "Your Daughter " + child.Name;

            sms1 += " has been registered at ";
            sms1 += child.Clinic.Name.Replace("&","and") + "\n";
            var response1 = SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, sms1);

            string sms2 = "ID: " + child.User.MobileNumber + "\nPassword: " + child.User.Password
                  + "\nClinic: " + child.Clinic.PhoneNumber + "\nhttp://vaccs.io/";

            var response2 = SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, sms2);
            return response1 + response2;
        }

        public static string SendSMS(string CountryCode, string MobileNumber, string Email, string text)
        {
            //string webTarget = "http://58.65.138.38:8181/sc/smsApi/sendSms?username=vccsio&password=123456&mobileNumber={0}&message={1}&mask=VACCS%20IO";
            string webTarget = "http://icworldsms.com:82/Service.asmx/SendSMS?SessionID=Ud1vaibfSexGvkohsFVVVEzoWrhUKfpylFZqOFVy9EB7CaifKP&CompaignName=text&MobileNo={0}&MaskName=VACCS+IO&Message={1}&MessageType=English";
            string url = String.Format(webTarget, "0"+MobileNumber, HttpUtility.UrlEncode(text));

            return Controllers.VaccineController.sendRequest(url);
        }
    }
}