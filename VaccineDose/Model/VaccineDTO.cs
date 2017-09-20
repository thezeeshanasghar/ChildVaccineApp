using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineDose.Model;

namespace VaccineDose
{
    public class VaccineDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> MinAge { get; set; }
        public Nullable<int> MaxAge { get; set; }

        public List<BrandDTO> Brands { get; set; }
    }
}