using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose
{
    public class DoctorScheduleDTO
    {
        public int ID { get; set; }
        public int DoseID { get; set; }
        public int DoctorID { get; set; }
        public int GapInDays { get; set; }

        public DoctorDTO Doctor { get; set; }
        public DoseDTO Dose { get; set; }

    }
}