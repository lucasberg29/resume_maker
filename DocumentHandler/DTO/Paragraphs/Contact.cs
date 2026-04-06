using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Paragraphs
{
    public class Contact : ResumeParagraph
    {
        public Contact()
        {
            ParagraphTag = "Contact";

            Elements.Add(new Element("Email"));
            Elements.Add(new Element("Phone Number"));
            Elements.Add(new Element("Location"));
        }
    }
}
