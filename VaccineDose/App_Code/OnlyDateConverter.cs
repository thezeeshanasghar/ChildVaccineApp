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

        public static void ParentEmail(ChildDTO child)
        {
            //string body = "Respected Parent,<br>";
            //if (child.Gender == "Boy")
            //{
            //    body += "Your Son <b>" + child.Name + "</b>";
            //}

            //if (child.Gender == "Girl")
            //{
            //    body += "Your Daughter <b>" + child.Name + "</b>";
            //}
            //body += " has been registered at Clinic ";
            //var clinic = new Clinic();
            //var doctor = new Doctor();
            //using (VDConnectionString entities = new VDConnectionString())
            //{
            //    clinic = entities.Clinics.Where(x => x.DoctorID == child.DoctorID).Where(x => x.IsOnline == true).FirstOrDefault();
            //    body += "<b>" + clinic.Name + "</b><br>";
            //    doctor = entities.Doctors.Where(x => x.ID == child.DoctorID).Where(x => x.ShowPhone == true).FirstOrDefault();
            //}
            //body += "ID: <b>" + child.MobileNumber + "</b><br>Password: <b>" + child.Password + "</b><br>Contact# : "
            //    + "Clinic Phone Number <b>" + clinic.PhoneNumber + "</b><br>";
            //if (doctor != null)
            //    body += "Doctor Phone Number: <b>" + doctor.PhoneNo + "<b><br>";
            ////TODO: website and android link
            //SendEmail(child.Name, child.Email, body);
        }
        public static void DoctorEmail(DoctorDTO doctor)
        {
            string body = "Hello " + "<b>" + doctor.FirstName + "</b>"
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