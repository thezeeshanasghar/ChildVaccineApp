
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VaccineDose
{
    public class ChildDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }

        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime  DOB { get; set; }
        public string CountryCode { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string StreetAddress { get; set; }
        public string Gender { get; set; }
        public int PreferredDayOfReminder { get; set; }
        public string City { get; set; }
        public string PreferredSchedule { get; set; }
        public string PreferredDayOfWeek { get; set; }
        public bool IsEPIDone { get; set; }
        public bool IsVerified { get; set; }
        public int ClinicID { get; set; }
        
        //Generate invoice optional fields
        public bool IsBrand { get; set; }
        public bool IsConsultationFee { get; set; }

        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime InvoiceDate { get; set; }

        public int DoctorID { get; set; }

        //To select Vaccine of the child on add-new-child page
        public List<VaccineDTO> ChildVaccines { get; set; }

        public ClinicDTO Clinic { get; set; }

    }
}