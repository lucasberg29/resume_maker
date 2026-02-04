using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentHandler
{
    public class DocumentHandler
    {
        public string DataFolderName = "Data";
        public string ResumeFolderName = "Resume";
        public string ResumeFileName = "Resume.docx";
        public string ResumeFolderPath { get; } = Path.Combine(AppContext.BaseDirectory, "Data", "Resume");

        public void InitHandler()
        {
            CreateFolder();
            //CreateFile();
        }

        private bool CreateFile(string ResumeFileName)
        {
            string resumePath = Path.Combine(
                AppContext.BaseDirectory,
                DataFolderName,
                ResumeFolderName,
                ResumeFileName
            );

            DocumentReaderWriter.CreateDocument(resumePath);

            return true;
        }

        private void CreateFolder()
        {
            string basePath = AppContext.BaseDirectory;

            string folderPath = Path.Combine(
                basePath,
                DataFolderName,
                ResumeFolderName
            );

            Directory.CreateDirectory(folderPath);
        }

        public string CreateSampleDocument(string fileName)
        {
            string path = Path.Combine(ResumeFolderPath, fileName);

            if (File.Exists(path))
                File.Delete(path);

            using var doc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document);
            var mainPart = doc.AddMainDocumentPart();
            mainPart.Document = new Document(new Body());

            mainPart.Document.Body.AppendChild(new Paragraph(new Run(new Text("Hello, this is a test resume!"))));

            return path;
        }

    }
}
