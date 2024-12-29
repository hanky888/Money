using System.Text.Json.Serialization;

namespace Money.Dtos
{

    public class CoinDeskInfo
    {
        [JsonPropertyName("time")]
        public Time Time { get; set; }

        [JsonPropertyName("disclaimer")]
        public string Disclaimer { get; set; }

        [JsonPropertyName("chartName")]
        public string ChartName { get; set; }

        [JsonPropertyName("bpi")]
        public Dictionary<string, BpiInfo> Bpi { get; set; }
    }
}
