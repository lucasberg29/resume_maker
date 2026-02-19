using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class TechnicalSkill
    {
        [JsonPropertyName("position")]
        public int Position { get; set; } = 0;
        [JsonPropertyName("text")]
        public string Text { get; set; } = "";
        [JsonPropertyName("type")]
        public string Type { get; set; } = "";
        [JsonPropertyName("style")]
        public Style Style { get; set; } = new Style();
    }
}