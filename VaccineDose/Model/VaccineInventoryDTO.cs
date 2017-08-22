using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose.Model
{
    public class VaccineInventoryDTO
    {
        public int ID { get; set; }

        public int Count { get; set; }

        public int VaccineID { get; set; }

        public int DoctorID { get; set; }

    }
}