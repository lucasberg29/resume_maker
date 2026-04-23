using DocumentFormat.OpenXml.Presentation;
using DocumentHandler.DTO.Paragraphs;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class Experience
    {
        public static int ExperienceIdCounter { get; set; } = 0;

        public Experience()
        {
            ExperienceIdCounter += 1;
            Id = ExperienceIdCounter;
        }

        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;
        [JsonPropertyName("position")]
        public int Position { get; set; } = 0;
        [JsonPropertyName("jobTitle")]
        public Element JobTitle { get; set; } = new Element();
        [JsonPropertyName("location")]
        public Element Location { get; set; } = new Element();
        [JsonPropertyName("companyName")]
        public Element CompanyName { get; set; } = new Element();
        [JsonPropertyName("companyWebsiteLink")]
        public Element CompanyWebsiteLink { get; set; } = new Element();
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("bulletPoints")]
        public List<ResumeParagraph> BulletPoints { get; set; } = new();
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        public List<ResumeParagraph> GetAllParagraphs()
        {
            return BulletPoints;
        }
    }
}