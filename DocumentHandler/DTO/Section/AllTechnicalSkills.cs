using DocumentHandler.DTO.Paragraphs;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Section
{
    public class AllTechnicalSkills
    {
        [JsonPropertyName("technicalSkillsHeader")]
        public ResumeParagraph TechnicalSkillsHeader { get; set; } = new ResumeParagraph("TechnicalSkillHeader", "Technical Skills");
        [JsonPropertyName("technicalSkills")]
        public List<TechnicalSkill> TechnicalSkills { get; set; } = new List<TechnicalSkill>();
        [JsonPropertyName("separator")]
        public Element Separator { get; set; } = new Element("◈");
    }
}