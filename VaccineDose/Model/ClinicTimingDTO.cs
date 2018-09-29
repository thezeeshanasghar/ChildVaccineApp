using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose.Model
{
    public class ClinicTimingDTO
    {
        public int ID { get; set; }
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Session { get; set; }
        public int ClinicID { get; set; }
        public bool IsOpen { get; set; }
    }
}