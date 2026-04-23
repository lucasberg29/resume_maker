using DocumentHandler.DTO.Paragraphs;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Section
{
    public class AllEducation
    {
        [JsonPropertyName("educationHeader")]
        public ResumeParagraph EducationHeader { get; set; } = new ResumeParagraph("EducationHeader", "Education");
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

        public List<ResumeParagraph> GetAllParagraphs()
        {
            List<ResumeParagraph> paragraphs = new List<ResumeParagraph>();
            paragraphs.Add(EducationHeader);
            foreach (var education in Education)
            {
                paragraphs.AddRange(education.GetAllParagraphs());
            }
            return paragraphs;
        }
    }
}