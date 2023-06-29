using System.Text.Json.Serialization;

namespace StalcraftCompanion.Model
{
    public class Character
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("alliance")]
        public string Alliance { get; set; }

        [JsonPropertyName("lastLogin")]
        public DateTime LastLogin { get; set; }

        [JsonPropertyName("displayedAchievements")]
        public List<object> DisplayedAchievements { get; set; }

        [JsonPropertyName("stats")]
        public List<Stat> Stats { get; set; }
    }

    public class Stat
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }
    }
}
