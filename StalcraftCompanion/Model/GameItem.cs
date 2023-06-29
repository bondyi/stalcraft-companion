using System.Text.Json.Serialization;

namespace StalcraftCompanion.Model
{
    public class Lines
    {
        [JsonPropertyName("ru")]
        public string Ru { get; set; }

        [JsonPropertyName("en")]
        public string En { get; set; }
    }

    public class Name
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("lines")]
        public Lines Lines { get; set; }
    }

    public class GameItem
    {
        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("name")]
        public Name Name { get; set; }
    }
}
