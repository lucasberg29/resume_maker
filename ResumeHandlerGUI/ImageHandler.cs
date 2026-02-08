using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Documents;
using A = DocumentFormat.OpenXml.Drawing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace ResumeHandlerGUI
{
    internal class ImageHandler
    {

        public void ExtractImages(DocumentFormat.OpenXml.Drawing.Paragraph paragraph, string docPath)
        {
            using var wordDoc = WordprocessingDocument.Open(docPath, false);

            foreach (var drawing in paragraph.Descendants<Drawing>())
            {
                var blip = drawing.Descendants<A.Blip>().FirstOrDefault();
                if (blip == null) continue;

                string relId = blip.Embed.Value;
                var imagePart = (ImagePart)wordDoc.MainDocumentPart.GetPartById(relId);

                using var stream = imagePart.GetStream();
                byte[] imageBytes = ReadAllBytes(stream);
            }
        }

        static byte[] ReadAllBytes(Stream stream)
        {
            using MemoryStream ms = new();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
