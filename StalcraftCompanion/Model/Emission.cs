using System.Text.Json.Serialization;

namespace StalcraftCompanion.Model
{
    public class Emission
    {
        [JsonPropertyName("currentStart")]
        public DateTime CurrentStart { get; set; }

        [JsonPropertyName("previousStart")]
        public DateTime PreviousStart { get; set; }

        [JsonPropertyName("previousEnd")]
        public DateTime PreviousEnd { get; set; }
    }
}
