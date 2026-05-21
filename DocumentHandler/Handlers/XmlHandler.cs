using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentHandler.DTO;
using DocumentHandler.DTO.Attribute;
using DocumentHandler.DTO.Paragraphs;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace DocumentHandler.Handlers
{
    public class XmlHandler
    {
        public static void SaveResumeToDocx(Resume resume, string filePath)
        {
            using var wordDoc = WordprocessingDocument.Create( filePath, WordprocessingDocumentType.Document);

            var mainPart = wordDoc.AddMainDocumentPart();
            mainPart.Document = new Document(new Body());

            var body = mainPart.Document.Body;

            if (body != null)
            {
                body.Append(CreateParagraph(
                    resume.PersonalInfo.FullName.Elements[0].Text,
                    bold: true,
                    fontSize: "14"));

                body.Append(CreateParagraph(resume.PersonalInfo.Contact.Elements[0].Text));
                body.Append(CreateParagraph(resume.PersonalInfo.Contact.Elements[1].Text));

                body.Append(new Paragraph(new Run(new Break())));
            }

            mainPart.Document.Save();
        }

        internal static bool ExportResumeToDOCX(string fileName, Resume currentResume, string resumeFolderPath)
        {
            string path = Path.Combine(resumeFolderPath, fileName);
            using var doc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document);
            var mainPart = doc.AddMainDocumentPart();
            mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document(new Body());

            // Full Name
            var fullName = currentResume.PersonalInfo.FullName;

            var fullNameRun = CreateRunText(fullName.Elements.First().ElementStyle, fullName.Elements.First().Text);
            var fullNameParagraph = CreateParagraph(fullName, fullNameRun);

            if (mainPart.Document.Body != null)
            {
                mainPart.Document.Body.AppendChild(fullNameParagraph);
            }

            // Contact Info
            var contact = currentResume.PersonalInfo.Contact;

            var contactRun = CreateRunText(contact.Elements[0].ElementStyle, contact.Elements[0].Text);

            contactRun.Append(CreateRunText(contact.Elements[0].ElementStyle, " - "));
            contactRun.Append(CreateRunText(contact.Elements[1].ElementStyle, contact.Elements[1].Text));
            contactRun.Append(CreateRunText(contact.Elements[1].ElementStyle, " - "));
            contactRun.Append(CreateRunText(contact.Elements[2].ElementStyle, contact.Elements[2].Text));

            var contactParagraph = CreateParagraph(contact, contactRun);

            mainPart.Document?.Body?.AppendChild(contactParagraph);

            //var socialParagraph = CreateSocialMediaIconsParagraph(doc, currentResume, resumeFolderPath);
            //mainPart.Document?.Body?.Append(socialParagraph);

            mainPart.Document?.Save();

            return true;
        }

        private static Run CreateRunText(ElementStyle style, string text)
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

        private static Paragraph CreateParagraph(ResumeParagraph paragraph, Run run)
        {
            var paragraphStyle = paragraph.ParagraphStyle;

            var paragraphProperties = new ParagraphProperties();
            switch (paragraphStyle.TextAlignment?.ToLower())
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

            //paragraphProperties.SpacingBetweenLines = new SpacingBetweenLines()
            //{
            //    Before = "100",
            //    After = "100",
            //    Line = "100",
            //    LineRule = LineSpacingRuleValues.Auto
            //};

            // 100 equals 5 pt - 1 to 20 then

            var lineSpacing =  (paragraphStyle.LineSpacing * 20).ToString();

            paragraphProperties.SpacingBetweenLines = new SpacingBetweenLines()
            {
                Before = "100",
                After = "100",
                Line = lineSpacing,
                LineRule = LineSpacingRuleValues.Auto
            };

            return new Paragraph(paragraphProperties, run);
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

        private static Paragraph CreateSocialMediaIconsParagraph(WordprocessingDocument doc, Resume currentResume, string resumeFolderPath)
        {
            var paragraphProperties = new ParagraphProperties();

            paragraphProperties.Append(new Justification() { Val = JustificationValues.Center });

            var paragraph = new Paragraph(paragraphProperties);

            foreach (var link in currentResume.PersonalInfo.SocialMediaLinks)
            {
                string imagePath = Path.Combine(resumeFolderPath, link.FilePath);

                paragraph.Append(
                    CreateHyperlinkedImage(doc, imagePath, link.ElementStyle.Hyperlink, 300000L, 300000L)
                );

                // spacing between icons
                paragraph.Append(new Run(new Text("  ")));
            }

            return paragraph;
        }

        private static Hyperlink CreateHyperlinkedImage(WordprocessingDocument doc, string imagePath, string url,
                                        long widthEmu = 300000L, long heightEmu = 300000L)
        {
            var mainPart = doc.MainDocumentPart;

            var hyperlinkRel = mainPart?.AddHyperlinkRelationship(
                new Uri(url),
                true);

            var imagePart = mainPart?.AddImagePart(ImagePartType.Png);
            using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                imagePart?.FeedData(stream);
            }

            var relId = mainPart?.GetIdOfPart(imagePart);

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
            { Id = hyperlinkRel?.Id };
        }
    }
}
