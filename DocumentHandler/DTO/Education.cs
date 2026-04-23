using DocumentHandler.DTO.Paragraphs;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class Education
    {
        public static int EducationIdCounter { get; set; } = 0;
        public Education() 
        {
            EducationIdCounter += 1;
            Id = EducationIdCounter;
        }

        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;
        [JsonPropertyName("position")]
        public int Position { get; set; } = 0;
        [JsonPropertyName("programTitle")]
        public Element ProgramTitle { get; set; } = new();
        [JsonPropertyName("location")]
        public Element Location { get; set; } = new();
        [JsonPropertyName("collegeName")]
        public Element CollegeName { get; set; } = new();
        [JsonPropertyName("collegeWebsiteLink")]
        public Element CollegeWebsiteLink { get; set; } = new();
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