using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

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

            var socialMediaLinks = bodyElements.ElementAt(3);

            resume.Introduction = bodyElements.ElementAt(3).InnerText.Trim();


            int experienceIndex = FindTextIndexByText("Experience", docPath);

            int educationIndex = FindTextIndexByText("Education", docPath); 

            int skillsIndex = FindTextIndexByText("Skills", docPath);   
            return resume;
        }

        public static void SaveResumeToDocx(Resume resume, string filePath)
        {
            using var wordDoc = WordprocessingDocument.Create( filePath, WordprocessingDocumentType.Document);

            var mainPart = wordDoc.AddMainDocumentPart();
            mainPart.Document = new Document(new Body());

            var body = mainPart.Document.Body;

            body.Append(CreateParagraph(
                resume.FullName,
                bold: true,
                fontSize: "14"));

            body.Append(CreateParagraph(resume.Email));
            body.Append(CreateParagraph(resume.PhoneNumber));

            body.Append(new Paragraph(new Run(new Break())));


            mainPart.Document.Save();
        }

        private static Paragraph CreateParagraph( string text, bool bold = false, string fontSize = "12")
        {
            var runProps = new RunProperties(
                new FontSize { Val = fontSize });

            if (bold)
                runProps.Append(new Bold());

            var run = new Run(runProps, new Text(text));
            return new Paragraph(run);
        }

        private static int FindTextIndexByText(string text, string docPath )
        {
            int index= -1;
            using var wordDoc = WordprocessingDocument.Open(docPath, false);
            return index;
        }
    }
}
