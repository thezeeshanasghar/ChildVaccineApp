using System;
using System.Globalization;
using System.Linq;
using System.Web;

namespace VaccineDose.App_Code
{
    public class UserSMS
    {
        static TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        public static string DoctorSMS(DoctorDTO doctor)
        {
            string body = "Hi Dr. " + textInfo.ToTitleCase(doctor.FirstName) + " " + textInfo.ToTitleCase(doctor.LastName) + " \n"
                + "You are registered at Vaccs.io\n\n"
                + "Your account credentials are: \n"
                + "ID/Mobile Number: " + doctor.MobileNumber + "\n"
                + "Password: " + doctor.Password + "\n"
                + "http://vaccs.io/";
            var response = SendSMS(doctor.CountryCode, doctor.MobileNumber, doctor.Email, body);
            addMessageToDB(doctor.MobileNumber, response, body, 1);
            return response;
        }
        public static string ParentSMS(Child child)
        {

            string sms1 = "Mr. " + child.FatherName + "\n";
            if (child.Gender == "Boy")
                sms1 += "Your Son " + textInfo.ToTitleCase(child.Name);

            if (child.Gender == "Girl")
                sms1 += "Your Daughter " + textInfo.ToTitleCase(child.Name);

            sms1 += " has been registered with Dr. " + textInfo.ToTitleCase(child.Clinic.Doctor.FirstName) + " " + textInfo.ToTitleCase(child.Clinic.Doctor.LastName);
            sms1 += " at " + child.Clinic.Name.Replace("&", "and") + "\n";

            var response1 = SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, sms1);

            string sms2 = "ID: " + child.User.MobileNumber + "\nPassword: " + child.User.Password
                  + "\nClinic: " + child.Clinic.PhoneNumber + "\nhttp://vaccs.io/";

            var response2 = SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, sms2);
            addMessageToDB(child.User.MobileNumber, response1, sms1, child.Clinic.Doctor.User.ID);
            addMessageToDB(child.User.MobileNumber, response2, sms2, child.Clinic.Doctor.User.ID);
            minusDoctorSMSCount(child.Clinic.Doctor);
            minusDoctorSMSCount(child.Clinic.Doctor);
            return response1 + response2;

        }
        public static string ParentSMSAlert(string doseName, DateTime scheduleDate, Child child)
        {

            string sms1 = "Mr. " + child.FatherName + "\n";
            sms1 += doseName + " Vaccine for ";
            if (child.Gender == "Boy")
                sms1 += "your son " + textInfo.ToTitleCase(child.Name);

            if (child.Gender == "Girl")
                sms1 += "your daughter " + textInfo.ToTitleCase(child.Name);

            sms1 += " is due ";
            if (scheduleDate.Date == DateTime.UtcNow.AddHours(5).Date)
                sms1 += "Today ";
            else
                sms1 += scheduleDate.Date.ToString("MM-dd-yyyy");

            sms1 += " at " + textInfo.ToTitleCase(child.Clinic.Name) + "\n";
            sms1 += "Plz confirm your appointment with Dr. " + textInfo.ToTitleCase(child.Clinic.Doctor.FirstName) + " " + textInfo.ToTitleCase(child.Clinic.Doctor.LastName);
            sms1 += " @ " + child.Clinic.Doctor.PhoneNo + " OR " + child.Clinic.PhoneNumber;
            var response1 = SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, sms1);
            addMessageToDB(child.User.MobileNumber, response1, sms1, child.Clinic.Doctor.User.ID);
            minusDoctorSMSCount(child.Clinic.Doctor);
            return response1;
        }

        public static string DoctorForgotPasswordSMS(Doctor doctor)
        {
            string body = "";
            body += "Hi " + textInfo.ToTitleCase(doctor.DisplayName);
            body += ",Your password is " + doctor.User.Password;
            var response = SendSMS(doctor.User.CountryCode, doctor.User.MobileNumber, doctor.Email, body);
            addMessageToDB(doctor.User.MobileNumber, response, body, 1);
            return response;
        }
        public static string ParentForgotPasswordSMS(Child child)
        {
            string body = "";
            body += "Hi " + textInfo.ToTitleCase(child.FatherName);
            body += ",Your password is " + child.User.Password;
            var response = SendSMS(child.User.CountryCode, child.User.MobileNumber, child.Email, body);
            addMessageToDB(child.User.MobileNumber, response, body, 1);
            return response;
        }

        public static string ParentFollowUpSMSAlert(FollowUp followUp)
        {
            string sms1 = "Followup visit of ";
            if (followUp.Child.Gender == "Boy")
                sms1 += "son " + textInfo.ToTitleCase(followUp.Child.Name);

            if (followUp.Child.Gender == "Girl")
                sms1 += "daughter " + textInfo.ToTitleCase(followUp.Child.Name);

            sms1 += " is due ";
            if (followUp.NextVisitDate == DateTime.UtcNow.AddHours(5).Date)
                sms1 += "Today";
            else
                sms1 += followUp.NextVisitDate;

            sms1 += " with Dr. " + textInfo.ToTitleCase(followUp.Doctor.FirstName) + " " + textInfo.ToTitleCase(followUp.Doctor.LastName) + " at " + textInfo.ToTitleCase(followUp.Child.Clinic.Name) + ". ";
            sms1 += "Kindly confirm your appointment at " + followUp.Doctor.PhoneNo;

            var response1 = SendSMS(followUp.Child.User.CountryCode, followUp.Child.User.MobileNumber, followUp.Child.Email, sms1);
            addMessageToDB(followUp.Child.User.MobileNumber, response1, sms1, followUp.Child.Clinic.Doctor.User.ID);
            minusDoctorSMSCount(followUp.Child.Clinic.Doctor);

            return response1;
        }

        public static void addMessageToDB(string mobileNumber, string apiResponse, string sms, int userId)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                Message m = new Message();
                m.MobileNumber = mobileNumber;
                m.ApiResponse = apiResponse;
                m.SMS = sms;
                m.UserID = userId;
                entities.Messages.Add(m);
                entities.SaveChanges();
            }
        }
        public static void minusDoctorSMSCount(Doctor doctor)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                Doctor dbDoctor = entities.Doctors.Where(x=>x.ID == doctor.ID).FirstOrDefault();
                dbDoctor.SMSLimit--; 
                entities.SaveChanges();
            }
        }
        public static string SendSMS(string CountryCode, string MobileNumber, string Email, string text)
        {
            
            //string webTarget = "http://icworldsms.com:82/Service.asmx/SendSMS?SessionID=Ud1vaibfSexGvkohsFVVVEzoWrhUKfpylFZqOFVy9EB7CaifKP&CompaignName=text&MobileNo={0}&MaskName=VACCS+IO&Message={1}&MessageType=English";
            //string url = String.Format(webTarget, "0" + MobileNumber, HttpUtility.UrlEncode(text));

            string webTarget = "http://58.65.138.38:8181/sc/smsApi/sendSms?username=vccsio&password=123456&mobileNumber={0}&message={1}&mask=VACCS%20IO";
            string url = String.Format(webTarget, "92" + MobileNumber, HttpUtility.UrlEncode(text));

            return Controllers.VaccineController.sendRequest(url);
        }
    }
}