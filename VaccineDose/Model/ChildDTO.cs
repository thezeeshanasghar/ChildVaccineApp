using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose.Model
{
    public class ChildDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public  DateTime  DOB { get; set; }
        public string MobileNumber { get; set; }
        public string StreetAddress { get; set; }
        public string Gender { get; set; }
        public int PreferredDayOfReminder { get; set; }
        public string City { get; set; }
        public string PreferredShedule { get; set; }
    }
}