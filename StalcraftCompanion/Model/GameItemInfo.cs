using System.Text.Json.Serialization;

namespace StalcraftCompanion.Model
{
    public class Element
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("key")]
        public Key Key { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("name")]
        public Name Name { get; set; }

        [JsonPropertyName("formatted")]
        public Formatted Formatted { get; set; }
    }

    public class Formatted
    {
        [JsonPropertyName("value")]
        public Value Value { get; set; }

        [JsonPropertyName("nameColor")]
        public string NameColor { get; set; }

        [JsonPropertyName("valueColor")]
        public string ValueColor { get; set; }
    }

    public class InfoBlock
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("title")]
        public Title Title { get; set; }

        [JsonPropertyName("elements")]
        public List<Element> Elements { get; set; }

        [JsonPropertyName("text")]
        public Text Text { get; set; }
    }

    public class Key
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("key")]
        public string KeyValue { get; set; }

        [JsonPropertyName("lines")]
        public Lines Lines { get; set; }
    }

    public class GameItemInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("name")]
        public Name Name { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("infoBlocks")]
        public List<InfoBlock> InfoBlocks { get; set; }
    }

    public class Status
    {
        [JsonPropertyName("state")]
        public string State { get; set; }
    }

    public class Text
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("lines")]
        public Lines Lines { get; set; }
    }

    public class Title
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("lines")]
        public Lines Lines { get; set; }
    }

    public class Value
    {
        [JsonPropertyName("ru")]
        public string Ru { get; set; }

        [JsonPropertyName("en")]
        public string En { get; set; }
    }
}
