using DocumentHandler.DTO.Attribute;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class TechnicalSkill
    {
        public static int TechnicalSkillIdCounter { get; set; } = 0;

        public TechnicalSkill() 
        {
            TechnicalSkillIdCounter = TechnicalSkillIdCounter + 1;
            Id = TechnicalSkillIdCounter;
        }

        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;
        [JsonPropertyName("position")]
        public int Position { get; set; } = 0;
        [JsonPropertyName("text")]
        public string Text { get; set; } = "";
        [JsonPropertyName("type")]
        public string Type { get; set; } = "";
        [JsonPropertyName("style")]
        public ElementStyle Style { get; set; } = new ElementStyle();
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = "";   
    }
}