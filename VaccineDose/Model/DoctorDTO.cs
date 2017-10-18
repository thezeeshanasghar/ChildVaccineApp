using Newtonsoft.Json;
using System;

namespace VaccineDose
{
    public class DoctorDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string PMDC { get; set; }
        public bool IsApproved { get; set; }
        public bool ShowPhone { get; set; }
        public bool ShowMobile { get; set; }
        public int InvoiceNumber { get; set; }
        public int ConsultationFee { get; set; }

        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime ValidUpto { get; set; }

        public ClinicDTO ClinicDTO { get; set; }
    }
}