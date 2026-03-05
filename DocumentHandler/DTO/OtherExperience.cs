using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class OtherExperience
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;
        [JsonPropertyName("position")]
        public int Position { get; set; } = 0;
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("element")]
        public Element Element { get; set; } = new("Other Experience");
    }
}
