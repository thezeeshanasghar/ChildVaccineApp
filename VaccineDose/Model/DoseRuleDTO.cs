using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose
{
    public class DoseRuleDTO
    {
        public int ID { get; set; }
        public int DoseFrom { get; set; }
        public int DoseTo { get; set; }
        public int Days { get; set; }
        public int VaccineID { get; set; }

    }
}