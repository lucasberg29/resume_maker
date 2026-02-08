using System.Text.Json.Serialization;

namespace DocumentHandler
{
    public class TechnicalSkill
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("type")]
        public string Type { get; set; } = "";
    }
}