using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Attribute
{
    public class ParagraphStyle
    {
        [JsonPropertyName("margin")]
        public string Margin { get; set; } = "0, 0, 0, 0";
        [JsonPropertyName("padding")]
        public string Padding { get; set; } = "0, 0, 0, 0";
        [JsonPropertyName("textAlignment")]
        public string TextAlignment { get; set; } = "left";
    }
}
