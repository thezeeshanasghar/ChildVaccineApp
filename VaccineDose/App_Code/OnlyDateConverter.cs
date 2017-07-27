using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace VaccineDose
{
    public class OnlyDateConverter : IsoDateTimeConverter
    {
        public OnlyDateConverter()
        {
            DateTimeFormat = "dd-MM-yyyy";
        }
    }

    public class UserEmail
    {
        public static string teamEmail = WebConfigurationManager.AppSettings["TeamEmail"];
        public static string teamEmailPassword = WebConfigurationManager.AppSettings["TeamEmailPassword"];
        public static string userName { get; set; }
        public static string userEmail { get; set; }

        public static void ParentEmail(Child child)
        {
            string body = "Respected Parent,<br>";
            if (child.Gender == "Boy")
                body += "Your Son <b>" + child.Name + "</b>";

            if (child.Gender == "Girl")
                body += "Your Daughter <b>" + child.Name + "</b>";

            body += " has been registered at Clinic ";
            body += "<b>" + child.Clinic.Name + "</b><br>";
               
            
            body += "ID: <b>" + child.MobileNumber + "</b><br>Password: <b>" + child.Password + "</b><br>Contact# : "
                + "Clinic Phone Number <b>" + child.Clinic.PhoneNumber + "</b><br>";
            
                body += "Doctor Phone Number: <b>" + child.Clinic.Doctor.PhoneNo + "<b><br>";
            //TODO: website and android link
            SendEmail(child.Name, child.Email, body);
        }
        public static void DoctorEmail(DoctorDTO doctor)
        {
            string body = "Hi " + "<b>" + doctor.FirstName + " " + doctor.LastName + "</b>, <br />"
                + "You are succesfully registered in <b>MyVaccs</b>.<br />"
                + "Your accounter credentials are: <br />"
                + "ID/Mobile Number: " + doctor.MobileNo + "<br />"
                + "Password: " + doctor.MobileNo + "<br />";
            SendEmail(doctor.FirstName, doctor.Email, body);
        }

        public static bool SendEmail(string userName, string userEmail, string body)
        {

            using (MailMessage mail = new MailMessage(teamEmail, userEmail))
            {
                mail.Subject = "AFZ-Tech Team";
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(teamEmail, teamEmailPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mail);
                return true;
            }

        }
    }
}