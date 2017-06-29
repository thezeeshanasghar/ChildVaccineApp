using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose
{
    public class DoctorDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string PMDC { get; set; }
        public bool IsApproved { get; set; }
    }
}