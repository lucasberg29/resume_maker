using DocumentHandler.DTO;
using DocumentHandler.DTO.Attribute;
using DocumentHandler.DTO.Section;
using DocumentHandler.Interfaces;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ResumeHandlerGUI.Handlers
{
    public class WPFDocumentHandler : Control
    {
        public FlowDocument FlowDocument = new();
        public IDocumentHandler DocumentHandler = new DocumentHandler.Handlers.DocumentHandler();

        public WPFDocumentHandler()
        {
            FlowDocument = new FlowDocument();

            DocumentHandler.InitHandler();

            string lastResume = Properties.Settings.Default.LastResume;

            if (lastResume != string.Empty)
            {
                string lastResumePath = Path.Combine(Directory.GetCurrentDirectory(), lastResume);
                DocumentHandler.LoadResumeFromDocument(lastResumePath, lastResume);
            }
        }

        private static Style GetStyle(string key)
        {
            return (Style)Application.Current.FindResource(key);
        }

        public void UpdateResume()
        {
            FlowDocument = new FlowDocument();

            UpdateHeader();
            UpdateTechnicalSkills();
            UpdateExperience();
            UpdateEducation();
            UpdateSkills();
        }

        private void UpdateHeader()
        {
            var personalInfo = DocumentHandler.GetPersonalInfo();

            // Full Name
            var fullNameRun = CreateRun(personalInfo.FullName.Elements.First());

            var fullNameParagraph = new Paragraph(fullNameRun)
            {
                Style = DtoStyleToWindowsStyle(personalInfo.FullName.ParagraphStyle)
            };

            FlowDocument.Blocks.Add(fullNameParagraph);

            // Contact Info
            var contact = DocumentHandler.GetPersonalInfo().Contact;

            var emailRun = CreateRun(contact.Elements[0]);
            var phoneRun = CreateRun(contact.Elements[1]);
            var locationRun = CreateRun(contact.Elements[2]);

            var contatInfoParagraph = new Paragraph
            {
                Style = DtoStyleToWindowsStyle(contact.ParagraphStyle)
            };

            contatInfoParagraph.Inlines.Add(emailRun);
            contatInfoParagraph.Inlines.Add(new Run(" - "));
            contatInfoParagraph.Inlines.Add(phoneRun);
            contatInfoParagraph.Inlines.Add(new Run(" - "));
            contatInfoParagraph.Inlines.Add(locationRun);

            FlowDocument.Blocks.Add(contatInfoParagraph);

            // SocialMediaLinks
            var socialParagraph = new Paragraph
            {
                Margin = new Thickness(0, 0, 0, 0),
                TextAlignment = TextAlignment.Center
            };

            var socialMediaLinks = personalInfo.SocialMediaLinks.OrderBy(s => s.Position).ToList();

            foreach (var link in socialMediaLinks)
            {
                if (!link.Active)
                {
                    continue;
                }

                string imagePath = link.FilePath;

                if (!File.Exists(imagePath))
                {
                    continue;
                }

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.DecodePixelWidth = 128;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();

                var image = new System.Windows.Controls.Image
                {
                    Source = bitmap,
                    Width = 24,
                    Height = 24,
                    SnapsToDevicePixels = true,
                    UseLayoutRounding = true,
                    Margin = new Thickness(2, 2, 2, 2),
                };

                image.SnapsToDevicePixels = true;
                image.UseLayoutRounding = true;

                RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.HighQuality);

                var imageContainer = new InlineUIContainer(image)
                {
                    BaselineAlignment = BaselineAlignment.Center
                };

                var hyperlink = new Hyperlink
                {
                    NavigateUri = new Uri(link.ElementStyle.Hyperlink),
                    TextDecorations = null,
                };

                hyperlink.RequestNavigate += (s, e) =>
                {
                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri)
                        {
                            UseShellExecute = true
                        });
                };

                hyperlink.Inlines.Add(imageContainer);

                socialParagraph.Inlines.Add(hyperlink);
                socialParagraph.Inlines.Add(new Run());
            }

            FlowDocument.Blocks.Add(socialParagraph);

            // Introduction
            var introduction = personalInfo.Introduction.Elements.First();
            var introductionRun = CreateRun(introduction);
            var introductionParagraph = new Paragraph(introductionRun)
            {
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0),
            };

            FlowDocument.Blocks.Add(introductionParagraph);
        }

        private void UpdateTechnicalSkills()
        {
            var technicalSkills = DocumentHandler.GetAllTechnicalSkills();    

            Style headerStyle = DtoStyleToWindowsStyle(technicalSkills.TechnicalSkillsHeader.Elements.First().ElementStyle);

            var technicalSkillsHeader = new Paragraph(new Run(technicalSkills.TechnicalSkillsHeader.Elements.First().Text))
            {
                Style = headerStyle,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            FlowDocument.Blocks.Add(technicalSkillsHeader);

            string technicalSkillsText = string.Empty;

            if (technicalSkills.TechnicalSkills.Where(t => t.Type == "language").Count() > 0)
            {
                technicalSkillsText = string.Join(" ◈ ", technicalSkills.TechnicalSkills.Where(t => t.Type == "language").Select(t => t.Text));
                AddParagraph(technicalSkillsText);
            }

            if(technicalSkills.TechnicalSkills.Where(t => t.Type == "framework").Count() > 0)
            {
                technicalSkillsText = string.Join(" ◈ ", technicalSkills.TechnicalSkills.Where(t => t.Type == "framework").Select(t => t.Text));
                AddParagraph(technicalSkillsText);
            }

            if (technicalSkills.TechnicalSkills.Where(t => t.Type == "tool").Count() > 0)
            {
                technicalSkillsText = string.Join(" ◈ ", technicalSkills.TechnicalSkills.Where(t => t.Type == "tool").Select(t => t.Text));
                AddParagraph(technicalSkillsText);
            }
        }

        private void UpdateExperience()
        {
            //Experience Header
            var experiences = DocumentHandler.GetAllExperience();

            var run = CreateRun(experiences.ExperienceHeader.Elements.First());

            var experienceHeader = new Paragraph(run)
            {
                Style = DtoStyleToWindowsStyle(experiences.ExperienceHeader.ParagraphStyle),
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            FlowDocument.Blocks.Add(experienceHeader);

            foreach (var exp in experiences.Experiences)
            {
                var table = new Table();

                table.Columns.Add(new TableColumn() { Width = new GridLength(3, GridUnitType.Star) });
                table.Columns.Add(new TableColumn() { Width = new GridLength(1, GridUnitType.Star) });

                var rowGroup = new TableRowGroup();
                var row = new TableRow();

                var titleCell = new TableCell(new Paragraph(CreateRun(exp.JobTitle))
                {
                    FontSize = 11,
                    FontWeight = FontWeights.Bold
                });

                var locationCell = new TableCell(new Paragraph(CreateRun(exp.JobTitle))
                {
                    FontSize = 11,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Right
                });

                row.Cells.Add(titleCell);
                row.Cells.Add(locationCell);
                rowGroup.Rows.Add(row);

                var secondRow = new TableRow();

                var companyNameCell = new TableCell(new Paragraph(new Run(exp.CompanyName.Text))
                {
                    FontSize = 11,
                    FontStyle = FontStyles.Italic
                });

                var finalDate = $"{exp.StartDate.ToString("MMMM yyyy")} - {exp.EndDate.ToString("MMMM yyyy")}";

                var durationCell = new TableCell(new Paragraph(new Run(exp.StartDate.ToString("MMMM yyyy")))
                {
                    FontSize = 11,
                    FontStyle = FontStyles.Italic,
                    TextAlignment = TextAlignment.Right
                });

                secondRow.Cells.Add(companyNameCell);
                secondRow.Cells.Add(durationCell);


                rowGroup.Rows.Add(secondRow);

                for (int i = 0; i < exp.BulletPoints.Count; i++)
                {
                    var bulletPointRow = new TableRow();

                    var paragraph = new Paragraph(new Run($"◇ {exp.BulletPoints[i].Elements.First().Text}"))
                    {
                        Style = DtoStyleToWindowsStyle(exp.BulletPoints[i].Elements.First().ElementStyle),
                        TextAlignment = TextAlignment.Left,
                        Margin = new Thickness(0, 0, 0, 1)
                    };

                    var tableCell = new TableCell(paragraph)
                    {
                        ColumnSpan = 2
                    };

                    bulletPointRow.Cells.Add(tableCell);
                    rowGroup.Rows.Add(bulletPointRow);
                }

                table.RowGroups.Add(rowGroup);
                FlowDocument.Blocks.Add(table);
            }
        }

        private void UpdateEducation()
        {
            var education = DocumentHandler.GetAllEducation();

            Style headerStyle = DtoStyleToWindowsStyle(education.EducationHeader.Elements.First().ElementStyle);

            var educationHeader = new Paragraph(new Run(education.EducationHeader.Elements.First().Text))
            {
                Style = headerStyle,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            FlowDocument.Blocks.Add(educationHeader);


            foreach (var edu in education.Education)
            {
                var table = new Table();

                table.Columns.Add(new TableColumn() { Width = new GridLength(3, GridUnitType.Star) });
                table.Columns.Add(new TableColumn() { Width = new GridLength(1, GridUnitType.Star) });

                var rowGroup = new TableRowGroup();
                var row = new TableRow();

                var titleCell = new TableCell(new Paragraph(new Run(edu.ProgramTitle.Text))
                {
                    FontSize = 11,
                    FontWeight = FontWeights.Bold
                });

                var locationCell = new TableCell(new Paragraph(new Run(edu.Location.Text))
                {
                    FontSize = 11,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Right
                });

                row.Cells.Add(titleCell);
                row.Cells.Add(locationCell);
                rowGroup.Rows.Add(row);

                var secondRow = new TableRow();

                var companyNameCell = new TableCell(new Paragraph(new Run(edu.CollegeName.Text))
                {
                    FontSize = 11,
                    FontStyle = FontStyles.Italic
                });

                var finalDate = $"{edu.StartDate.ToString("MMMM yyyy")} - {edu.EndDate.ToString("MMMM yyyy")}";

                var durationCell = new TableCell(new Paragraph(new Run(edu.StartDate.ToString("MMMM yyyy")))
                {
                    FontSize = 11,
                    FontStyle = FontStyles.Italic,
                    TextAlignment = TextAlignment.Right
                });

                secondRow.Cells.Add(companyNameCell);
                secondRow.Cells.Add(durationCell);

                rowGroup.Rows.Add(secondRow);

                table.RowGroups.Add(rowGroup);

                FlowDocument.Blocks.Add(table);

                foreach (var bullet in edu.BulletPoints)
                {
                    var bulletParagraph = new Paragraph(new Run($"◇ {bullet}"))
                    {
                        FontSize = 11,
                        Margin = new Thickness(0, 0, 0, 0)
                    };
                    FlowDocument.Blocks.Add(bulletParagraph);
                }
            }
        }

        private void UpdateSkills()
        {
            var allOtherExperience = DocumentHandler.GetAllOtherExperience();   

            Style headerStyle = DtoStyleToWindowsStyle(allOtherExperience.OtherExperienceHeader.Elements.First().ElementStyle);

            var educationHeader = new Paragraph(new Run(allOtherExperience.OtherExperienceHeader.Elements.First().Text))
            {
                Style = headerStyle,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            FlowDocument.Blocks.Add(educationHeader);

            var skills = allOtherExperience.OtherExperiences;

            foreach (var skill in skills)
            {
                Style style = DtoStyleToWindowsStyle(skill.Element.ElementStyle);

                var bulletParagraph = new Paragraph(new Run($"◇ {skill.Element.Text}"))
                {
                    Style = style,
                };

                FlowDocument.Blocks.Add(bulletParagraph);
            }
        }

        public void MoveUpInOrder(SocialMediaLink socialMediaLink)
        {
            if (socialMediaLink.Position <= 1)
            {
                return;
            }

            var socialMediaLinks = DocumentHandler.GetPersonalInfo().SocialMediaLinks;

            for (int i = 0; i < socialMediaLinks.Count; i++)
            {
                if (socialMediaLinks[i].Position == socialMediaLink.Position - 1)
                {
                    DocumentHandler.SetPosition(socialMediaLinks[i], socialMediaLinks[i].Position + 1);
                    break;
                }
            }

            DocumentHandler.SetPosition(socialMediaLink, socialMediaLink.Position - 1);
        }

        public void MoveUpInOrder(TechnicalSkill paragraphId)
        {

        }

        private Run CreateRun(Element element)
        {
            var elementStyle = DtoStyleToWindowsStyle(element.ElementStyle);

            var run = new Run(element.Text)
            {
                Style = elementStyle
            };

            return run;
        }

        private Style DtoStyleToWindowsStyle(ElementStyle style)
        {
            Style newStyle = new Style();

            newStyle.Setters.Add(new Setter(FontWeightProperty, style.IsBold ? FontWeights.Bold : FontWeights.Normal));
            newStyle.Setters.Add(new Setter(FontSizeProperty, Convert.ToDouble(style.FontSize)));
            newStyle.Setters.Add(new Setter(FontFamilyProperty, new FontFamily(style.FontFamily)));
            newStyle.Setters.Add(new Setter(ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(style.Color))));

            List<int> numbers = style.Margin.Split(',').Select(int.Parse).ToList();

            Thickness margin = new Thickness();

            if (numbers.Count == 4)
            {
                margin = new Thickness(numbers[0], numbers[1], numbers[2], numbers[3]);
            }

            newStyle.Setters.Add(new Setter(MarginProperty, margin));

            return newStyle;
        }

        private Style DtoStyleToWindowsStyle(ParagraphStyle style)
        {
            Style newStyle = new Style();

            // Margin
            List<int> marginNumbers = style.Margin.Split(',').Select(int.Parse).ToList();
            Thickness margin = marginNumbers.Count == 4
                ? new Thickness(marginNumbers[0], marginNumbers[1], marginNumbers[2], marginNumbers[3])
                : new Thickness();

            newStyle.Setters.Add(new Setter(MarginProperty, margin));

            // Padding
            List<int> paddingNumbers = style.Padding.Split(',').Select(int.Parse).ToList();
            Thickness padding = paddingNumbers.Count == 4
                ? new Thickness(paddingNumbers[0], paddingNumbers[1], paddingNumbers[2], paddingNumbers[3])
                : new Thickness();

            newStyle.Setters.Add(new Setter(PaddingProperty, padding));

            // Text Alignment
            TextAlignment alignment = style.TextAlignment switch
            {
                "left" => TextAlignment.Left,
                "center" => TextAlignment.Center,
                "right" => TextAlignment.Right,
                "justify" => TextAlignment.Justify,
                _ => TextAlignment.Left  // default
            };

            // Line Spacing
            if (style.LineSpacing > 0)
            {
                newStyle.Setters.Add(new Setter(Block.LineHeightProperty, style.LineSpacing));
            }

            newStyle.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, alignment));

            return newStyle;
        }

        private bool AddParagraph(string paragraphText)
        {
            var text = new System.Windows.Documents.Run(paragraphText)
            {
                FontSize = 10,
                FontFamily = new FontFamily("Roboto Serif, Arial, sans-serif")
            };

            var paragraph = new System.Windows.Documents.Paragraph(text)
            {
                Margin = new Thickness(0, 0, 0, 5),
                TextAlignment = TextAlignment.Center
            };

            FlowDocument.Blocks.Add(paragraph);

            return true;
        }
    }
}
