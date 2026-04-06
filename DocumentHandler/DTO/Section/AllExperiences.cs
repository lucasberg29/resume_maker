using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Section
{
    public class AllExperiences
    {
        [JsonPropertyName("experienceHeader")]
        public Element ExperienceHeader { get; set; } = new Element("Experience");
        [JsonPropertyName("experiences")]
        public List<Experience> Experiences { get; set; } = new List<Experience>();
    }
}