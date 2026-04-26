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
        }

        public ResumeParagraph(string paragraphTag)
        {
            ParagraphTag = paragraphTag;
            Id = GetID();
        }

        public ResumeParagraph(string paragraphTag, string firstElement)
        {
            ParagraphTag = paragraphTag;
            Elements.Add(new Element(firstElement));
            Id = GetID();
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
