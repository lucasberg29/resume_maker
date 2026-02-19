using DocumentFormat.OpenXml.Presentation;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class Experience
    {
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
        public List<Element> BulletPoints { get; set; } = new();
    }
}