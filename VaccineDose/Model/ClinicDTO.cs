using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose
{
    public class ClinicDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string OffDays { get; set; }
        public string PhoneNumber { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public int DoctorID { get; set; }
        public bool IsOnline { get; set; }
        public DoctorDTO Doctor { get; set; }

    }
}