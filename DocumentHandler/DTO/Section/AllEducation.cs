using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Section
{
    public class AllEducation
    {
        [JsonPropertyName("educationHeader")]
        public Element EducationHeader { get; set; } = new Element("Education");
        [JsonPropertyName("education")]
        public List<Education> Education { get; set; } = new List<Education>();
    }
}