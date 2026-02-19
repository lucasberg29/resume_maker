using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class Style
    {
        [JsonPropertyName("fontSize")]
        public string FontSize { get; set; } = "10";
        [JsonPropertyName("fontFamily")]
        public string FontFamily { get; set; } = "Roboto";
        [JsonPropertyName("margin")]
        public string Margin { get; set; } = "0, 0, 0, 0";
        [JsonPropertyName("color")]
        public string Color { get; set; } = "black";
        [JsonPropertyName("bold")]
        public bool Bold { get; set; } = false;
    }
}
