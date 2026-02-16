using System.Text.Json.Serialization;

namespace DocumentHandler
{
    public class SocialMediaLink
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("fileName")]
        public string FileName { get; set; } = string.Empty;
        [JsonPropertyName("filePath")]
        public string FilePath { get; set; } = string.Empty;
        [JsonPropertyName("alt")]
        public string Alt { get; set; } = string.Empty;
        [JsonPropertyName("hyperlink")]
        public string Hyperlink { get; set; } = string.Empty;
    }
}