using DocumentFormat.OpenXml.Presentation;
using DocumentHandler.DTO.Attribute;
using DocumentHandler.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Paragraphs
{
    public class ResumeParagraph
    {
        public static int ParagraphIdCounter { get; set; } = 0;

        public ResumeParagraph()
        {
            Id =  GetID();
            ParagraphHandler.Register(this);
        }

        private int GetID()
        {
            ParagraphIdCounter = ParagraphIdCounter + 1;
            return ParagraphIdCounter;
        }

        [JsonPropertyName("paragraphTag")]
        public string ParagraphTag { get; set; } = string.Empty;    
        [JsonPropertyName("elements")]
        public List<Element> Elements { get; set; } = new();
        [JsonPropertyName("separator")]
        public string Separator { get; set; } = "◈" ;
        [JsonPropertyName("paragraphStyle")]
        public ParagraphStyle ParagraphStyle { get; set; } = new();
        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;
    }
}
