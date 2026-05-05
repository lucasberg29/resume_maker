using DocumentHandler.DTO.Attribute;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class SocialMediaLink
    {
        public static int SocialMediaLinkIdCounter { get; set; } = 0;

        public SocialMediaLink()
        {
            Id = GetID();
        }

        private int GetID()
        {
            SocialMediaLinkIdCounter = SocialMediaLinkIdCounter + 1;
            return SocialMediaLinkIdCounter;
        }

        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;
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
        [JsonPropertyName("elementStyle")]
        public ElementStyle ElementStyle { get; set; } = new ElementStyle();
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;
    }
}