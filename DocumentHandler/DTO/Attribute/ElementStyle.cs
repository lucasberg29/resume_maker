using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Attribute
{
    public class ElementStyle
    {
        [JsonPropertyName("fontSize")]
        public int FontSize { get; set; } = 10;
        [JsonPropertyName("fontFamily")]
        public string FontFamily { get; set; } = "Roboto";
        [JsonPropertyName("margin")]
        public string Margin { get; set; } = "0, 0, 0, 0";
        [JsonPropertyName("color")]
        public string Color { get; set; } = "#4c1130";
        [JsonPropertyName("isBold")]
        public bool IsBold { get; set; } = false;
        [JsonPropertyName("isItalic")]
        public bool IsItalic { get; set; } = false;
        [JsonPropertyName("hyperlink")]
        public string Hyperlink { get; set; } = string.Empty;
    }
}
