using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class OtherExperience
    {
        public static int OtherExperienceIdCounter { get; set; } = 0;

        public OtherExperience()
        {
            OtherExperienceIdCounter += 1;
            Id = OtherExperienceIdCounter;
        }

        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;
        [JsonPropertyName("position")]
        public int Position { get; set; } = 0;
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("element")]
        public Element Element { get; set; } = new("Other Experience");
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;
    }
}
