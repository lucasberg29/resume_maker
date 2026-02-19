using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class Skill
    {
        [JsonPropertyName("position")]
        public int Position { get; set; } = 0;
        [JsonPropertyName("text")]
        public string Text { get; set; } = "";
        [JsonPropertyName("style")]
        public Style Style { get; set; } = new Style();
    }
}
