using Newtonsoft.Json;
using System;
using VaccineDose.App_Code;

namespace VaccineDose.Model
{
    public class ChildDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }

        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime  DOB { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string StreetAddress { get; set; }
        public string Gender { get; set; }
        public int PreferredDayOfReminder { get; set; }
        public string City { get; set; }
        public string PreferredSchedule { get; set; }
        public string PreferredDayOfWeek { get; set; }
        public bool IsEPIDone { get; set; }
        

    }
}