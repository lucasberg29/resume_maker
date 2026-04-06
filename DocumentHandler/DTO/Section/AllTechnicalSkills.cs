using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Section
{
    public class AllTechnicalSkills
    {
        [JsonPropertyName("technicalSkillsHeader")]
        public Element TechnicalSkillsHeader { get; set; } = new Element();
        [JsonPropertyName("technicalSkills")]
        public List<TechnicalSkill> TechnicalSkills { get; set; } = new List<TechnicalSkill>(); 
    }
}