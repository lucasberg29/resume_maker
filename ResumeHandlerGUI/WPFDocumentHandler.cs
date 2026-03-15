using Microsoft.Win32;
using ResumeHandlerGUI.Windows;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ResumeHandlerGUI
{
    public class WPFDocumentHandler : Control
    {
        public FlowDocument FlowDocument = new();
        public DocumentHandler.DocumentHandler DocumentHandler = new();

        public WPFDocumentHandler()
        {
            FlowDocument = new FlowDocument();

            DocumentHandler.InitHandler();

            string lastResume = Properties.Settings.Default.LastResume;

            string lastResumePath = Path.Combine(Directory.GetCurrentDirectory(), lastResume);

            DocumentHandler.LoadResumeFromDocument(lastResumePath, lastResume);
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
            // Full Name
            var run = CreateRun(DocumentHandler.CurrentResume.FullName.Text, DocumentHandler.CurrentResume.FullName.Style);

            var fullNameParagraph = new System.Windows.Documents.Paragraph(run)
            {
                Style = GetStyle("Resume.FullNameParagraph")
            };

            FlowDocument.Blocks.Add(fullNameParagraph);

            // Contact Info
            var emailRun = CreateRun(DocumentHandler.CurrentResume.Email.Text, DocumentHandler.CurrentResume.Email.Style);
            var phoneRun = CreateRun(DocumentHandler.CurrentResume.PhoneNumber.Text, DocumentHandler.CurrentResume.PhoneNumber.Style);
            var locationRun = CreateRun(DocumentHandler.CurrentResume.Location.Text, DocumentHandler.CurrentResume.Location.Style);

            var contatInfoParagraph = new System.Windows.Documents.Paragraph
            {
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0)
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

            foreach (var link in DocumentHandler.CurrentResume.SocialMediaLinks)
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
                    NavigateUri = new Uri(link.Hyperlink),
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
            var introductionRun = CreateRun(DocumentHandler.CurrentResume.Introduction.Text, DocumentHandler.CurrentResume.Introduction.Style);
            var introductionParagraph = new Paragraph(introductionRun)
            {
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0),
            };

            FlowDocument.Blocks.Add(introductionParagraph);
        }

        private Run CreateRun(string text, DocumentHandler.DTO.Style style)
        {
            var fullNameStyle = DtoStyleToWindowsStyle(style);

            var run = new Run(text)
            {
                Style = fullNameStyle
            };

            return run;
        }

        private void UpdateTechnicalSkills()
        {
            Style headerStyle = DtoStyleToWindowsStyle(DocumentHandler.CurrentResume.TechnicalSkillsHeader.Style);

            var technicalSkillsHeader = new Paragraph(new Run(DocumentHandler.CurrentResume.TechnicalSkillsHeader.Text))
            {
                Style = headerStyle,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            FlowDocument.Blocks.Add(technicalSkillsHeader);

            string technicalSkills = string.Join(" ◈ ", DocumentHandler.CurrentResume.TechnicalSkills.Where(t => t.Type == "language").Select(t => t.Text));
            AddParagraph(technicalSkills);

            technicalSkills = string.Join(" ◈ ", DocumentHandler.CurrentResume.TechnicalSkills.Where(t => t.Type == "framework").Select(t => t.Text));
            AddParagraph(technicalSkills);

            technicalSkills = string.Join(" ◈ ", DocumentHandler.CurrentResume.TechnicalSkills.Where(t => t.Type == "tool").Select(t => t.Text));
            AddParagraph(technicalSkills);
        }

        private void UpdateExperience()
        {
            Style headerStyle = DtoStyleToWindowsStyle(DocumentHandler.CurrentResume.ExperienceHeader.Style);

            var experienceHeader = new Paragraph(new Run(DocumentHandler.CurrentResume.ExperienceHeader.Text))
            {
                Style = headerStyle,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            FlowDocument.Blocks.Add(experienceHeader);

            var experience = DocumentHandler.CurrentResume.Experience;

            foreach (var exp in experience)
            {
                var table = new Table();

                table.Columns.Add(new TableColumn() { Width = new GridLength(3, GridUnitType.Star) });
                table.Columns.Add(new TableColumn() { Width = new GridLength(1, GridUnitType.Star) });

                var rowGroup = new TableRowGroup();
                var row = new TableRow();

                var titleCell = new TableCell(new Paragraph(new Run(exp.JobTitle.Text))
                {
                    FontSize = 11,
                    FontWeight = FontWeights.Bold
                });

                var locationCell = new TableCell(new Paragraph(new Run(exp.Location.Text))
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



                    var paragraph = new Paragraph(new Run($"◇ {exp.BulletPoints[i].Text}"))
                    {
                        Style = DtoStyleToWindowsStyle(exp.BulletPoints[i].Style),
                        TextAlignment = TextAlignment.Left,
                        Margin = new Thickness(0, 0, 0, 2)
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
            Style headerStyle = DtoStyleToWindowsStyle(DocumentHandler.CurrentResume.EducationHeader.Style);

            var educationHeader = new Paragraph(new Run(DocumentHandler.CurrentResume.EducationHeader.Text))
            {
                Style = headerStyle,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            FlowDocument.Blocks.Add(educationHeader);

            var education = DocumentHandler.CurrentResume.Education;

            foreach (var edu in education)
            {
                var table = new Table();

                table.Columns.Add(new TableColumn() { Width = new GridLength(3, GridUnitType.Star) });
                table.Columns.Add(new TableColumn() { Width = new GridLength(1, GridUnitType.Star) });

                var rowGroup = new TableRowGroup();
                var row = new TableRow();

                var titleCell = new TableCell(new Paragraph(new Run(edu.ProgramTitle))
                {
                    FontSize = 11,
                    FontWeight = FontWeights.Bold
                });

                var locationCell = new TableCell(new Paragraph(new Run(edu.Location))
                {
                    FontSize = 11,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Right
                });

                row.Cells.Add(titleCell);
                row.Cells.Add(locationCell);
                rowGroup.Rows.Add(row);

                var secondRow = new TableRow();

                var companyNameCell = new TableCell(new Paragraph(new Run(edu.CollegeName))
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
            Style headerStyle = DtoStyleToWindowsStyle(DocumentHandler.CurrentResume.OtherExperienceHeader.Style);

            var educationHeader = new Paragraph(new Run(DocumentHandler.CurrentResume.OtherExperienceHeader.Text))
            {
                Style = headerStyle,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            FlowDocument.Blocks.Add(educationHeader);

            var skills = DocumentHandler.CurrentResume.OtherExperience;

            foreach (var skill in skills)
            {
                Style style = DtoStyleToWindowsStyle(skill.Element.Style);

                var bulletParagraph = new Paragraph(new Run($"◇ {skill.Element.Text}"))
                {
                    Style = style,
                };
                FlowDocument.Blocks.Add(bulletParagraph);
            }
        }

        private Style DtoStyleToWindowsStyle(DocumentHandler.DTO.Style style)
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
