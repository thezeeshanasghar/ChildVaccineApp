using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using VaccineDose.Model;

namespace VaccineDose.App_Code
{
    public class UserEmail
    {

        public static string teamEmail = WebConfigurationManager.AppSettings["TeamEmail"];
        public static string teamEmailPassword = WebConfigurationManager.AppSettings["TeamEmailPassword"];
        public static string userName { get; set; }
        public static string userEmail { get; set; }

        public static void ParentEmail(ChildDTO parent)
        {
            string body = "Hello" + parent.Name
              + ", <br>Congratulations you are successfully registered";
            SendEmail(parent.Name, parent.Email, body);
        }
        public static void DoctorEmail(DoctorDTO doctor)
        {
            string body = "Hello" + doctor.FirstName
                + ", <br>Congratulations you are successfully registered";
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