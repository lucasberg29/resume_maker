using DocumentHandler.DTO.Paragraphs;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Section
{
    public class AllOtherExperience
    {
        [JsonPropertyName("otherExperienceHeader")]
        public ResumeParagraph OtherExperienceHeader { get; set; } = new ResumeParagraph("OtherExperienceHeader", "Other Experience");
        [JsonPropertyName("otherExperience")]
        public List<OtherExperience> OtherExperiences { get; set; } = new List<OtherExperience>();

        public bool DeleteOtherExperience(int id)
        {
            var otherExperience = OtherExperiences.Find(otherExperience => otherExperience.Id == id);

            if (otherExperience != null)
            {
                OtherExperiences.Remove(otherExperience);
                return true;
            }

            return false;
        }
    }
}