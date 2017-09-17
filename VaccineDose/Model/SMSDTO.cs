using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose.Model
{
    public class SMSDTO
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string MobileNumber { get; set; }
        public string Message { get; set; }
        [JsonIgnore]
        public string Status { get; set; }
    }
}