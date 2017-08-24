using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose.Model
{
    public class VaccineBrandDTO
    {
        public int ID { get; set; }

        public string BrandName { get; set; }

        public int VaccineID { get; set; }
        public VaccineDTO Vaccine { get; set; }


    }
}