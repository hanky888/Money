using System.Text.Json.Serialization;

namespace Money.Dtos
{
    public class Time
    {
        [JsonPropertyName("updated")]
        public string Updated { get; set; }
        [JsonPropertyName("updatedISO")]
        public string UpdatedISO { get; set; }
        [JsonPropertyName("updateduk")]
        public string UpdatedUK { get; set; }
    }
}
