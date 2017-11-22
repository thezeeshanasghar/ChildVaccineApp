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

            sms1 += " has been registered at Clinic ";
            sms1 += child.Clinic.Name + "\n";
            var response1 = SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, sms1);

            string sms2 = "ID: " + child.User.MobileNumber + "\n Password: " + child.User.Password
                  + "\nClinic# " + child.Clinic.PhoneNumber + "\n"
                  + "http://vaccs.io/";

            var response2 = SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, sms2);
            return response1 + response2;
        }

        public static string SendSMS(string CountryCode, string MobileNumber, string Email, string text)
        {
            string webTarget = "http://58.65.138.38:8181/sc/smsApi/sendSms?username=vccsio&password=123456&mobileNumber={0}&message={1}&mask=VACCS%20IO";
            string url = String.Format(webTarget, CountryCode + MobileNumber, HttpUtility.HtmlEncode(text));

            return Controllers.VaccineController.sendRequest(url);
        }
    }
}