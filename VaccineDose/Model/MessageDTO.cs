using Newtonsoft.Json;
using System;

namespace VaccineDose.Model
{
    public class MessageDTO
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string MobileNumber { get; set; }
        public string SMS { get; set; }
        // [JsonIgnore]
        public string ApiResponse { get; set; }
        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime Created { get; set; }
        public int UserID { get; set; }
        public UserDTO User { get; set; }
    }
}