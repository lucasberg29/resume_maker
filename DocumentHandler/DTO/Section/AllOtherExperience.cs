using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Section
{
    public class AllOtherExperience
    {
        [JsonPropertyName("otherExperienceHeader")]
        public Element OtherExperienceHeader { get; set; } = new Element("Other Experience");
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