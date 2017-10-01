using Newtonsoft.Json;

namespace VaccineDose.Model
{
    public class MessageDTO
    {
        [JsonIgnore]
        public int ID { get; set; }

        public string MobileNumber { get; set; }
        public string SMS { get; set; }

        [JsonIgnore]
        public string Status { get; set; }
    }
}