using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class Element
    {
        public Element() { }

        public Element(string text)
        {
            Text = text;   
        }

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("style")]
        public Style Style { get; set; } = new();

    }
}
