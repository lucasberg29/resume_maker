using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Presentation;
using DocumentHandler.DTO.Attribute;
using DocumentHandler.Handlers;
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
            Id = GetID();
        }

        public Element(string text)
        {
            Text = text;
            Id = GetID();
        }

        public Element(string text, string tag)
        {
            Text = text;
            Tag = tag;
            Id = GetID();
        }

        private int GetID()
        {
            ElementIdCounter = ElementIdCounter + 1;
            return ElementIdCounter;
        }

        [JsonPropertyName("tag")]
        public string Tag { get; set; } = string.Empty; 
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
