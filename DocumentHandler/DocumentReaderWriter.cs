using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHandler
{
    public class DocumentReaderWriter
    {

        public static void ReadFile(string filePath)
        {
            
        }

        public static void CreateDocument(string path)
        {
            using var doc = WordprocessingDocument.Create(
                path,
                WordprocessingDocumentType.Document
            );

            var mainPart = doc.AddMainDocumentPart();
            mainPart.Document = new Document(new Body());
        }

        public void AddParagraph(string path, string text)
        {
            using var doc = WordprocessingDocument.Open(path, true);
            var body = doc.MainDocumentPart!.Document.Body!;
            body.AppendChild(new Paragraph(new Run(new Text(text))));
        }

        public static void WriteFile(string filePath, string content)
        {

        }


    }
}
