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

            Elements.Add(new Element("Email", "Email"));
            Elements.Add(new Element("PhoneNumber", "PhoneNumber"));
            Elements.Add(new Element("Location", "Location"));
        }

        public Element? GetElementByTag(string tag)
        {
            foreach (var element in Elements)
            {
                if (element.Tag == tag)
                {
                    return element;
                }
            }

            return null;
        }

        internal void Init()
        {
            Elements[0].Tag = "Email";
            Elements[1].Tag = "PhoneNumber";
            Elements[2].Tag = "Location";
        }
    }
}
