using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineDose.Model;

namespace VaccineDose
{
    public class ScheduleDTO
    {
        public int ID { get; set; }
        public int ChildId { get; set; }
        public int DoseId { get; set; }

        [JsonConverter(typeof(OnlyDateConverter))]
        public System.DateTime Date { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Circle { get; set; }
        public bool IsDone { get; set; }
        public bool Due2EPI { get; set; }
        public DoseDTO Dose { get; set; }
        public virtual ChildDTO Child { get; set; }
        public List<BrandDTO> Brands { get; set; }
        public BrandDTO Brand { get; set; }
        public int BrandId { get; set; }
        public List<ScheduleBrandDTO> ScheduleBrands { get; set; }
        public int DoctorID { get; set; }


        [JsonConverter(typeof(OnlyDateConverter))]
        public System.DateTime GivenDate { get; set; }

        //Vacations
        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime FromDate { get; set; }

        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime ToDate { get; set; }

        public List<ClinicDTO> Clinics { get; set; }

    }
}