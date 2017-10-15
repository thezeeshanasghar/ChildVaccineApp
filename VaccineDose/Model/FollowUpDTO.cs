using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose.Model
{
    public class FollowUpDTO
    {
        public int ID { get; set; }
        public string Disease { get; set; }

        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime Date { get; set; }
        public int ChildID { get; set; }
        public int DoctorID { get; set; }

        public ChildDTO Child { get; set; }
        public DoctorDTO Doctor { get; set; }
    }
}