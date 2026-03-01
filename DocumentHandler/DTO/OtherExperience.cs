using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class OtherExperience
    {
        [JsonPropertyName("position")]
        public int Position { get; set; } = 0;
        [JsonPropertyName("element")]
        public Element Element { get; set; } = new("Other Experience");
    }
}
