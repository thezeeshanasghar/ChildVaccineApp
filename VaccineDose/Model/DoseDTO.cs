using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose
{
    public class DoseDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? MinGap { get; set; }
        public int DoseOrder { get; set; }
        public int VaccineID { get; set; }
        //public virtual Vaccine Vaccine { get; set; }
    }
}