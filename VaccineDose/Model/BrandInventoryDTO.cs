using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose.Model
{
    public class BrandInventoryDTO
    {
        public int ID { get; set; }

        public int Count { get; set; }

        public int VaccineID { get; set; }

        public int DoctorID { get; set; }
        public DoctorDTO Doctor { get; set; }

        public VaccineDTO Vaccine { get; set; }


    }
}