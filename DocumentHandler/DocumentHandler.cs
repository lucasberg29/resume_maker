using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace DocumentHandler
{
    public class DocumentHandler : IDocumentHandler
    {
        public Resume CurrentResume = new Resume();

        private string DocumentPath = string.Empty;
        private string DataFolderName = "Data";
        private string ResumeFolderName = "Resume";
        public string ResumeFolderPath { get; } = Path.Combine(AppContext.BaseDirectory, "Data", "Resume");

        public string ResumeFileName = "Resume.docx";

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

        private void ParseResume()
        {
            JsonReaderWriter.ReadResumeFromJson(ref CurrentResume);

            Console.WriteLine($"Resume loaded from {DocumentPath}");
        }

        public void InitHandler()
        {
            CreateFolder();
        }

        public bool SaveResume()
        {
            // Copy Images is social media links
            foreach (var socialMediaLink in CurrentResume.SocialMediaLinks)
            {

                CopyResumeImage(socialMediaLink.FilePath, ResumeFolderPath);

                socialMediaLink.FilePath = Path.Combine(ResumeFolderPath, socialMediaLink.FileName);
            }

            JsonReaderWriter.WriteResumeToJson(CurrentResume);

            XmlParser.SaveResumeToDocx(CurrentResume, "Resume.docx");
            return true;
        }

        //public string CreateSampleDocument(string fileName)
        //{
        //    string path = Path.Combine(ResumeFolderPath, fileName);

        //    File.Delete(path);

        //    using var doc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document);
        //    var mainPart = doc.AddMainDocumentPart();
        //    mainPart.Document = new Document(new Body());

        //    mainPart.Document.Body.AppendChild(new Paragraph(new Run(new Text("Hello, this is a test resume!"))));

        //    if (path.EndsWith(".docx"))
        //    {
        //        return DocumentPath;
        //    }

        //    return DocumentPath;
        //}

        public void LoadResumeFromDocument(string docPath, string safeFileName)
        {
            DocumentPath = docPath;
            ResumeFileName = safeFileName;
            ParseResume();
        }

        public string GetResumeFileName()
        {
            return ResumeFileName;
        }

        public void AddTechnicalSkill(string skillName, string skillType)
        {
            CurrentResume.TechnicalSkills.Add(new TechnicalSkill
            {
                Name = skillName,
                Type = skillType
            });
        }

        public void AddExperience(Experience experience)
        {
            CurrentResume.Experience.Add(experience);
        }

        public void AddEducation()
        {
            throw new NotImplementedException();
        }

        public void AddSkill()
        {
            throw new NotImplementedException();
        }

        public void AddSocialMediaLink(SocialMediaLink socialMediaLink)
        {
            CurrentResume.SocialMediaLinks.Add(socialMediaLink);
        }

        public static string CopyResumeImage(string sourcePath, string destinationFolder)
        {
            Directory.CreateDirectory(destinationFolder);

            string fileName = System.IO.Path.GetFileName(sourcePath);
            string destinationPath = System.IO.Path.Combine(destinationFolder, fileName);

            int counter = 1;

            while (File.Exists(destinationPath))
            {
                string name = System.IO.Path.GetFileNameWithoutExtension(fileName);
                string ext = System.IO.Path.GetExtension(fileName);

                destinationPath = System.IO.Path.Combine(
                    destinationFolder,
                    $"{name}_{counter}{ext}"
                );

                counter++;
            }

            File.Copy(sourcePath, destinationPath);

            return System.IO.Path.GetFileName(destinationPath);
        }
    }
}
