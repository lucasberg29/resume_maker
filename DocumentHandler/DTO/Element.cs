using DocumentFormat.OpenXml.Presentation;
using DocumentHandler.DTO.Attribute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class Element
    {
        public static int ElementIdCounter { get; set; } = 0;   

        public Element() 
        {
            ElementIdCounter = ElementIdCounter + 1;
            Id = ElementIdCounter;
        }

        public Element(string text)
        {
            Text = text;
            ElementIdCounter = ElementIdCounter + 1;
            Id = ElementIdCounter;
        }

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
        [JsonPropertyName("style")]
        public ElementStyle ElementStyle { get; set; } = new();
        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;
        [JsonPropertyName("id")]
        public int Id { get; set; }  = 0;
    }
}
