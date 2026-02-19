using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class SocialMediaLink
    {
        [JsonPropertyName("position")]
        public int Position { get; set; } = 0;
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
        [JsonPropertyName("style")]
        public Style Style { get; set; } = new Style();
    }
}