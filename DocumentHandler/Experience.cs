using DocumentFormat.OpenXml.Presentation;
using System.Text.Json.Serialization;

namespace DocumentHandler
{
    public class Experience
    {
        [JsonPropertyName("jobTitle")]
        public string JobTitle { get; set; } = "";
        [JsonPropertyName("location")]
        public string Location { get; set; } = "";
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; } = "";
        [JsonPropertyName("companyWebsiteLink")]
        public string CompanyWebsiteLink { get; set; } = "";
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("bulletPoints")]
        public List<string> BulletPoints { get; set; } = new List<string>();
    }
}