using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Section
{
    public class AllEducation
    {
        [JsonPropertyName("educationHeader")]
        public Element EducationHeader { get; set; } = new Element("Education");
        [JsonPropertyName("education")]
        public List<Education> Education { get; set; } = new List<Education>();

        public bool DeleteEducation(int id)
        {
            var education = Education.Find(education => education.Id == id);

            if (education != null)
            {
                Education.Remove(education);
                return true;
            }

            return false;
        }
    }
}