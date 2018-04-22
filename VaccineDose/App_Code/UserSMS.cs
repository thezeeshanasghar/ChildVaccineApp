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
            sms1 += child.Clinic.Name.Replace("&", "and") + "\n";
            var response1 = SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, sms1);

            string sms2 = "ID: " + child.User.MobileNumber + "\nPassword: " + child.User.Password
                  + "\nClinic: " + child.Clinic.PhoneNumber + "\nhttp://vaccs.io/";

            var response2 = SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, sms2);
            return response1 + response2;
        }
        public static string ParentSMSAlert(string doseName, DateTime scheduleDate, Child child)
        {
            string sms1 = "Respected Parents\n";
            sms1 += doseName + " Vaccine is due for ";
            if (child.Gender == "Boy")
                sms1 += "your son " + child.Name;

            if (child.Gender == "Girl")
                sms1 += "your daughter " + child.Name;

            sms1 += " A ";
            if (scheduleDate.Date == DateTime.Today.Date)
                sms1 += "Today ";
            else
                sms1 += scheduleDate.Date.ToString("MM-dd-yyyy");

            sms1 += " at " + child.Clinic.Name + "\n";
            sms1 += "Kindly confirm your appointment at ";
            sms1 += child.Clinic.Doctor.PhoneNo + " OR " + child.Clinic.PhoneNumber;
            var response1 = SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, sms1);
            return response1;
        }

        public static string DoctorForgotPasswordSMS(Doctor doctor)
        {
            string body = "";
            body += "Hi " + doctor.DisplayName;
            body += ",Your password is " + doctor.User.Password;
            var response = SendSMS(doctor.User.CountryCode, doctor.User.MobileNumber, doctor.Email, body);
            return response;
        }
        public static string ParentForgotPasswordSMS(Child child)
        {
            string body = "";
            body += "Hi " + child.FatherName;
            body += ",Your password is " + child.User.Password;
            var response = SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, body);
            return response;
        }

        public static string ParentFollowUpSMSAlert(FollowUp followUp)
        {
            string sms1 = "Respected Parent,\n";
            sms1 += "Followup visit of your ";
            if (followUp.Child.Gender == "Boy")
                sms1 += "son " + followUp.Child.Name;

            if (followUp.Child.Gender == "Girl")
                sms1 += "daughter " + followUp.Child.Name;

            sms1 += " is due ";
            if (followUp.NextVisitDate == DateTime.Today.Date)
                sms1 += "Today";
            else
                sms1 += followUp.NextVisitDate;

            sms1 += " at " + followUp.Child.Clinic.Name + ". ";
            sms1 += "Kindly confirm your appointment.";
           // sms1 += followUp.Child.Clinic.Doctor.PhoneNo + " OR " + followUp.Child.Clinic.PhoneNumber;

            var response1 = SendSMS(followUp.Child.User.CountryCode, followUp.Child.User.MobileNumber, followUp.Child.Email, sms1);
            return response1;
        }


        public static string SendSMS(string CountryCode, string MobileNumber, string Email, string text)
        {
            //string webTarget = "http://58.65.138.38:8181/sc/smsApi/sendSms?username=vccsio&password=123456&mobileNumber={0}&message={1}&mask=VACCS%20IO";
            string webTarget = "http://icworldsms.com:82/Service.asmx/SendSMS?SessionID=Ud1vaibfSexGvkohsFVVVEzoWrhUKfpylFZqOFVy9EB7CaifKP&CompaignName=text&MobileNo={0}&MaskName=VACCS+IO&Message={1}&MessageType=English";
            string url = String.Format(webTarget, "0" + MobileNumber, HttpUtility.UrlEncode(text));

            return Controllers.VaccineController.sendRequest(url);
        }
    }
}