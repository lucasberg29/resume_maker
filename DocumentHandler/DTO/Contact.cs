using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class Contact
    {
        public Element FullName { get; set; } = new Element();
        [JsonPropertyName("phoneNumber")]
        public Element PhoneNumber { get; set; } = new Element();
        [JsonPropertyName("email")]
        public Element Email { get; set; } = new Element();
        [JsonPropertyName("location")]
        public Element Location { get; set; } = new Element();
        [JsonPropertyName("style")]
        public Style Style { get; set; } = new Style() { TextAlignment = "center" };
    }
}
