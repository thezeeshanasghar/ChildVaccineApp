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
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<int> Lat { get; set; }
        public Nullable<int> Long { get; set; }
        public int DoctorID { get; set; }
    }
}