using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Packaging;
using DocumentHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHandler
{
    public class XmlParser
    {
        public static Resume ParseXml(string docPath)
        {
            Resume resume = new Resume();

            using var wordDoc = WordprocessingDocument.Open(docPath, false);

            var bodyElements = wordDoc.MainDocumentPart.Document.Body.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>();

            resume.FullName = bodyElements.ElementAt(0).InnerText.Trim();

            string[] contact = bodyElements.ElementAt(1).InnerText.Trim().Split(" - ", StringSplitOptions.TrimEntries);

            resume.Email = contact[0];
            resume.PhoneNumber = contact[1];
            resume.Location = contact[2];

            // TODO: Add social media links and icons
            var socialMediaLinks = bodyElements.ElementAt(3);


            resume.Introduction = bodyElements.ElementAt(3).InnerText.Trim();


            int experienceIndex = FindTextIndexByText("Experience", docPath);

            int educationIndex = FindTextIndexByText("Education", docPath); 

            int skillsIndex = FindTextIndexByText("Skills", docPath);   

            return resume;

        }


        private static int FindTextIndexByText(string text, string docPath )
        {
            int index= -1;

            using var wordDoc = WordprocessingDocument.Open(docPath, false);

            return index;
        }
    }
}
