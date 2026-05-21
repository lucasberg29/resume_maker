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
        [JsonPropertyName("indentationLeft")]
        public double IndentationLeft { get; set; } = 0.0;
        [JsonPropertyName("indentationRight")]
        public double IndentationRight { get; set; } = 0.0;
        [JsonPropertyName("lineSpacing")]
        public double LineSpacing { get; set; } = 1.16;
        [JsonPropertyName("spacingBefore")]
        public double SpacingBefore { get; set; } = 0.0;
        [JsonPropertyName("spacingAfter")]
        public double SpacingAfter { get; set; } = 8.0;
    }
}
