using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class Education
    {
        [JsonPropertyName("position")]
        public int Position { get; set; } = 0;
        [JsonPropertyName("programTitle")]
        public string ProgramTitle { get; set; } = "";
        [JsonPropertyName("location")]
        public string Location { get; set; } = "";
        [JsonPropertyName("collegeName")]
        public string CollegeName { get; set; } = "";
        [JsonPropertyName("collegeWebsiteLink")]
        public string CollegeWebsiteLink { get; set; } = "";
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("bulletPoints")]
        public List<string> BulletPoints { get; set; } = new List<string>();
    }
}