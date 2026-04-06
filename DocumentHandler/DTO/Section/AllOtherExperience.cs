using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Section
{
    public class AllOtherExperience
    {
        [JsonPropertyName("otherExperienceHeader")]
        public Element OtherExperienceHeader { get; set; } = new Element("Other Experience");
        [JsonPropertyName("otherExperience")]
        public List<OtherExperience> OtherExperience { get; set; } = new List<OtherExperience>();
    }
}