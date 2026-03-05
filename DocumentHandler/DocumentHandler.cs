using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentHandler.DTO;
using DocumentHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using DocumentFormat.OpenXml.CustomProperties;

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
        public static string CurrentResumeDataName = "data.json";  

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
            JsonReaderWriter.ReadResumeFromJson(ref CurrentResume, CurrentResumeDataName);
        }

        public void InitHandler()
        {
            CreateFolder();
        }

        public bool SaveResume()
        {
            foreach (var socialMediaLink in CurrentResume.SocialMediaLinks)
            {
                string result = CopyResumeImage(socialMediaLink.FilePath, ResumeFolderPath);

                if (result != "")
                {
                    socialMediaLink.FilePath = Path.Combine(ResumeFolderPath, socialMediaLink.FileName);
                }
            }

            JsonReaderWriter.WriteResumeToJson(CurrentResume, CurrentResumeDataName);

            XmlParser.SaveResumeToDocx(CurrentResume, "Resume.docx");
            return true;
        }

        public void LoadResumeFromDocument(string docPath, string safeFileName)
        {
            DocumentPath = docPath;
            ResumeFileName = safeFileName;

            CurrentResumeDataName = safeFileName;

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
                Text = skillName,
                Type = skillType
            });
        }

        public void AddExperience(Experience experience)
        {
            CurrentResume.Experience.Add(experience);
        }

        public void AddSocialMediaLink(SocialMediaLink socialMediaLink)
        {
            CurrentResume.SocialMediaLinks.Add(socialMediaLink);
        }

        public static string CopyResumeImage(string sourcePath, string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            string fileName = System.IO.Path.GetFileName(sourcePath);
            string destinationPath = System.IO.Path.Combine(destinationFolder, fileName);

            int counter = 1;

            while (File.Exists(destinationPath))
            {
                return "";
            }

            File.Copy(sourcePath, destinationPath);

            return System.IO.Path.GetFileName(destinationPath);
        }

        public bool ExportResumeToDOCX(string fileName = "Resume.docx")
        {
            // Open docx file
            string path = Path.Combine(ResumeFolderPath, fileName);
            using var doc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document);
            var mainPart = doc.AddMainDocumentPart();
            mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document(new Body());

            // Full Name
            var run = CreateRunText(CurrentResume.FullName.Style, CurrentResume.FullName.Text);
            var paragraph = CreateParagraph(CurrentResume.FullName, run);

            mainPart.Document.Body.AppendChild(paragraph);

            // Contact Info
            var paragraphProperties = new ParagraphProperties();
            switch (CurrentResume.Email.Style.TextAlignment?.ToLower())
            {
                case "center":
                    paragraphProperties.Append(new Justification() { Val = JustificationValues.Center });
                    break;

                case "right":
                    paragraphProperties.Append(new Justification() { Val = JustificationValues.Right });
                    break;

                default:
                    paragraphProperties.Append(new Justification() { Val = JustificationValues.Left });
                    break;
            }

            var contactParagraph = new Paragraph(paragraphProperties);

            contactParagraph.Append(
                CreateRunText(CurrentResume.Email.Style, CurrentResume.Email.Text)
            );

            contactParagraph.Append(
                CreateRunText(CurrentResume.Email.Style, " - ")
            );

            contactParagraph.Append(
                CreateRunText(CurrentResume.PhoneNumber.Style, CurrentResume.PhoneNumber.Text)
            );

            contactParagraph.Append(
                CreateRunText(CurrentResume.PhoneNumber.Style, " - ")
            );

            contactParagraph.Append(
                CreateRunText(CurrentResume.Location.Style, CurrentResume.Location.Text)
            );

            mainPart.Document.Body.AppendChild(contactParagraph);

            var socialParagraph = CreateSocialMediaIconsParagraph(doc);
            mainPart.Document.Body.Append(socialParagraph);

            mainPart.Document.Save();

            return true;
        }

        private Paragraph CreateParagraph(Element element, Run run)
        {
            var paragraphProperties = new ParagraphProperties();
            switch (element.Style.TextAlignment?.ToLower())
            {
                case "center":
                    paragraphProperties.Append(new Justification() { Val = JustificationValues.Center });
                    break;

                case "right":
                    paragraphProperties.Append(new Justification() { Val = JustificationValues.Right });
                    break;

                default:
                    paragraphProperties.Append(new Justification() { Val = JustificationValues.Left });
                    break;
            }

            paragraphProperties.SpacingBetweenLines = new SpacingBetweenLines()
            {
                Before = "100",
                After = "100",
                Line = "100",
                LineRule = LineSpacingRuleValues.Auto
            };

            return new Paragraph(paragraphProperties, run);
        }

        private Run CreateRunText(DTO.Style style, string text)
        {
            var runProperties = new RunProperties();

            runProperties.Append(
                new RunFonts()
                {
                    Ascii = style.FontFamily,
                    HighAnsi = style.FontFamily,
                    ComplexScript = style.FontFamily
                },
                new FontSize()
                {
                    Val = (style.FontSize * 2).ToString()
                },
                new Color()
                {
                    Val = style.Color.Replace("#", "")
                },
                new Bold()
                {
                    Val = style.IsBold
                },
                new Italic()
                {
                    Val = style.IsItalic
                }
            );

            var run = new Run(
                runProperties,
                new Text()
                {
                    Text = text,
                    Space = SpaceProcessingModeValues.Preserve,
                }
            );

            return run;
        }

        private Hyperlink CreateHyperlinkedImage(WordprocessingDocument doc, string imagePath,string url,
                                                long widthEmu = 300000L,long heightEmu = 300000L)
        {
            var mainPart = doc.MainDocumentPart;

            var hyperlinkRel = mainPart.AddHyperlinkRelationship(
                new Uri(url),
                true);

            var imagePart = mainPart.AddImagePart(ImagePartType.Png);
            using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                imagePart.FeedData(stream);
            }

            var relId = mainPart.GetIdOfPart(imagePart);

            var drawing = new Drawing(
                new DW.Inline(
                    new DW.Extent() { Cx = widthEmu, Cy = heightEmu },
                    new DW.EffectExtent()
                    {
                        LeftEdge = 0L,
                        TopEdge = 0L,
                        RightEdge = 0L,
                        BottomEdge = 0L
                    },
                    new DW.DocProperties()
                    {
                        Id = (UInt32Value)1U,
                        Name = Path.GetFileName(imagePath)
                    },
                    new DW.NonVisualGraphicFrameDrawingProperties(
                        new A.GraphicFrameLocks() { NoChangeAspect = true }),
                    new A.Graphic(
                        new A.GraphicData(
                            new PIC.Picture(
                                new PIC.NonVisualPictureProperties(
                                    new PIC.NonVisualDrawingProperties()
                                    {
                                        Id = (UInt32Value)0U,
                                        Name = Path.GetFileName(imagePath)
                                    },
                                    new PIC.NonVisualPictureDrawingProperties()),
                                new PIC.BlipFill(
                                    new A.Blip()
                                    {
                                        Embed = relId
                                    },
                                    new A.Stretch(new A.FillRectangle())),
                                new PIC.ShapeProperties(
                                    new A.Transform2D(
                                        new A.Offset() { X = 0L, Y = 0L },
                                        new A.Extents() { Cx = widthEmu, Cy = heightEmu }),
                                    new A.PresetGeometry(
                                        new A.AdjustValueList())
                                    { Preset = A.ShapeTypeValues.Rectangle }))
                        )
                        { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                )
            );

            return new Hyperlink(
                new Run(drawing)
            )
            { Id = hyperlinkRel.Id };
        }

        private Paragraph CreateSocialMediaIconsParagraph(WordprocessingDocument doc)
        {
            var paragraphProperties = new ParagraphProperties();

            paragraphProperties.Append(new Justification() { Val = JustificationValues.Center });

            var paragraph = new Paragraph(paragraphProperties);

            foreach (var link in CurrentResume.SocialMediaLinks)
            {
                string imagePath = Path.Combine(ResumeFolderPath, link.FilePath);

                paragraph.Append(
                    CreateHyperlinkedImage(doc, imagePath, link.Hyperlink, 300000L, 300000L)
                );

                // spacing between icons
                paragraph.Append(new Run(new Text("  ")));
            }

            return paragraph;
        }

        public void AddEducation(Education education)
        {
            CurrentResume.Education.Add(education);
        }

        public void AddOtherExperience(OtherExperience otherExperience)
        {
            CurrentResume.OtherExperience.Add(otherExperience);
        }

        public bool CreateNewResume(string resumeName)
        {
            CurrentResumeDataName = resumeName;
            CurrentResumeDataName = string.Concat(resumeName, ".json");
            ParseResume();
            return true;
        }

        public void SetTechnicalSkillActive(string technicalSkillName, bool isActive)
        {
            foreach (var technicalSkill in CurrentResume.TechnicalSkills)
            {
                if (technicalSkill.Text == technicalSkillName)
                {
                    technicalSkill.Active = isActive;
                    break;
                }
            }
        }

        public void SetExperienceActive(string experienceName, bool isActive)
        {
            throw new NotImplementedException();
        }

        public void SetSocialMediaLinkActive(string socialMediaLinkName, bool isActive)
        {
            for (var i = 0; i < CurrentResume.SocialMediaLinks.Count; i++)
            {
                if (CurrentResume.SocialMediaLinks[i].Name == socialMediaLinkName)
                {
                    CurrentResume.SocialMediaLinks[i].Active = isActive;
                    break;
                }
            }
        }

        public void SetEducationActive(string educationName, bool isActive)
        {
            for (var i = 0; i < CurrentResume.SocialMediaLinks.Count; i++)
            {
                if (CurrentResume.SocialMediaLinks[i].Name == educationName)
                {
                    CurrentResume.SocialMediaLinks[i].Active = isActive;
                    break;
                }
            }
        }

        public void SetOtherExperienceActive(string otherExperienceName, bool isActive)
        {
            for (var i = 0; i < CurrentResume.SocialMediaLinks.Count; i++)
            {
                if (CurrentResume.SocialMediaLinks[i].Name == otherExperienceName)
                {
                    CurrentResume.SocialMediaLinks[i].Active = isActive;
                    break;
                }
            }
        }

        public TechnicalSkill GetTechnicalSkillByName(string technicalSkillName)
        {
            for (var i = 0; i < CurrentResume.TechnicalSkills.Count; i++)
            {
                if (CurrentResume.TechnicalSkills[i].Text == technicalSkillName)
                {
                    return CurrentResume.TechnicalSkills[i];
                }
            }

            return new TechnicalSkill();    
        }

        public SocialMediaLink GetSocialMediaLinkByName(string socialMediaLinkName)
        {
            for (var i = 0; i < CurrentResume.SocialMediaLinks.Count; i++)
            {
                if (CurrentResume.SocialMediaLinks[i].Name == socialMediaLinkName)
                {
                    return CurrentResume.SocialMediaLinks[i];
                }
            }

            return new SocialMediaLink();
        }

        public Experience GetExperienceByName(string experienceName)
        {
            for (var i = 0; i < CurrentResume.Experience.Count; i++)
            {
                if (CurrentResume.Experience[i].CompanyName.Text == experienceName)
                {
                    return CurrentResume.Experience[i];
                }
            }

            return new Experience();
        }

        public Education GetEducationByName(string educationName)
        {
            for (var i = 0; i < CurrentResume.Education.Count; i++)
            {
                if (CurrentResume.Education[i].CollegeName == educationName)
                {
                    return CurrentResume.Education[i];
                }
            }

            return new Education();
        }

        public OtherExperience GetOtherExperienceByName(string otherExperienceName)
        {
            for (var i = 0; i < CurrentResume.OtherExperience.Count; i++)
            {
                if (CurrentResume.OtherExperience[i].Name == otherExperienceName)
                {
                    return CurrentResume.OtherExperience[i];
                }
            }

            return new OtherExperience();
        }

        public bool UpdateTechnicalSkill(TechnicalSkill technicalSkill)
        {
            for (var i = 0; i < CurrentResume.TechnicalSkills.Count; i++)
            {
                if (CurrentResume.TechnicalSkills[i].Text == technicalSkill.Text)
                {
                    CurrentResume.TechnicalSkills[i] = technicalSkill;
                    return true;
                }
            }

            return false;
        }

        public bool UpdateExperience(Experience experience)
        {
            for (var i = 0; i < CurrentResume.Experience.Count; i++)
            {
                if (CurrentResume.Experience[i].CompanyName == experience.CompanyName)
                {
                    CurrentResume.Experience[i] = experience;
                    return true;
                }
            }

            return false;
        }

        public bool UpdateSocialMediaLink(SocialMediaLink socialMediaLink)
        {
            for (var i = 0; i < CurrentResume.SocialMediaLinks.Count; i++)
            {
                if (CurrentResume.SocialMediaLinks[i].Name == socialMediaLink.Name)
                {
                    CurrentResume.SocialMediaLinks[i] = socialMediaLink;
                    return true;
                }
            }

            return false;
        }

        public bool UpdateEducation(Education education)
        {
            for (var i = 0; i < CurrentResume.Education.Count; i++)
            {
                if (CurrentResume.Education[i].CollegeName == education.CollegeName)
                {
                    CurrentResume.Education[i] = education;
                    return true;
                }
            }

            return false;
        }

        public bool UpdateOtherExperience(OtherExperience otherExperience)
        {
            for (var i = 0; i < CurrentResume.OtherExperience.Count; i++)
            {
                if (CurrentResume.OtherExperience[i].Name == otherExperience.Name)
                {
                    CurrentResume.OtherExperience[i] = otherExperience;
                    return true;
                }
            }

            return false;
        }
    }
}
