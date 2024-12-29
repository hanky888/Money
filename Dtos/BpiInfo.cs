using System.Text.Json.Serialization;

namespace Money.Dtos
{
    public class BpiInfo
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("rate")]
        public string Rate { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("rate_float")]
        public decimal RateFloat { get; set; }
    }
}
