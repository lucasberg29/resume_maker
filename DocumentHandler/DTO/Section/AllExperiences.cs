using DocumentFormat.OpenXml.Presentation;
using DocumentHandler.DTO.Paragraphs;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Section
{
    public class AllExperiences
    {
        [JsonPropertyName("experienceHeader")]
        public ResumeParagraph ExperienceHeader { get; set; } = new ResumeParagraph("ExperienceHeader", "Experience");
        [JsonPropertyName("experiences")]
        public List<Experience> Experiences { get; set; } = new List<Experience>();

        public void AddExperience(Experience experience)
        {
            Experiences.Add(experience);
        }

        public bool DeleteExperience(int id) 
        {
            var experience = Experiences.Find(experience => experience.Id == id);

            if (experience != null)
            {
                Experiences.Remove(experience);
                return true;
            }

            return false;
        }

        public List<ResumeParagraph> GetAllParagraphs()
        {
            var paragraphs = new List<ResumeParagraph>();
            foreach (var experience in Experiences)
            {
                paragraphs.AddRange(experience.GetAllParagraphs());
            }
            return paragraphs;
        }

    }
}