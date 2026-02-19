using Microsoft.Win32;
using ResumeHandlerGUI.Windows;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ResumeHandlerGUI
{
    public partial class MainWindow : Window
    {
        private readonly WindowManager _windowManager;

        public static DocumentHandler.DocumentHandler _documentHandler;
        FlowDocument flowDoc = new FlowDocument();

        private static Style GetStyle(string key)
        {
            return (Style)Application.Current.FindResource(key);
        }

        public MainWindow()
        {
            InitializeComponent();
            _windowManager = new WindowManager(this);

            _documentHandler = new DocumentHandler.DocumentHandler();
            _documentHandler.InitHandler();
            ShowResume("", "");

            SubscribeClicksToWindowManager();
        }

        private void SubscribeClicksToWindowManager()
        {
            MenuEditFullName.Click += (_, __) => _windowManager.EditFullName();
            MenuEditEmail.Click += (_, __) => _windowManager.EditEmail();
            MenuEditPhoneNumber.Click += (_, __) => _windowManager.EditPhoneNumber();

            MenuAddExperience.Click += (_, __) => _windowManager.AddExperience();

            MenuAddTechnicalSkill.Click += (_, __) => _windowManager.AddTechnicalSkill();
        }

        private void ShowResume(string docPath, string safeFileName)
        {
            _documentHandler.LoadResumeFromDocument(docPath, safeFileName);
            UpdateResume();
        }

        public void UpdateResume()
        {
            flowDoc = new FlowDocument();

            UpdateHeader();

            UpdateTechnicalSkills();

            UpdateExperience();

            UpdateEducation();

            UpdateSkills();

            DocViewer.Document = flowDoc;
        }



        private void UpdateHeader()
        {
            var text = new Run(_documentHandler.CurrentResume.FullName.Text)
            {
                Style = GetStyle("Resume.FullName")
            };

            var FullNameParagraph = new System.Windows.Documents.Paragraph(text)
            {
                Style = GetStyle("Resume.FullNameParagraph")
            };

            flowDoc.Blocks.Add(FullNameParagraph);

            string contatInfo = $"{_documentHandler.CurrentResume.Email.Text} - {_documentHandler.CurrentResume.PhoneNumber.Text} - {_documentHandler.CurrentResume.Location.Text}";
            AddParagraph(contatInfo);

            // SocialMediaLinks

            var socialParagraph = new Paragraph
            {
                Margin = new Thickness(0, 5, 0, 10),
                TextAlignment = TextAlignment.Center
            };

            foreach (var link in _documentHandler.CurrentResume.SocialMediaLinks)
            {
                string imagePath = link.FilePath;

                if (!File.Exists(imagePath))
                {
                    continue;
                }

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                bitmap.DecodePixelHeight = 24;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                var image = new System.Windows.Controls.Image
                {
                    Source = bitmap,
                    Width = 24,
                    Height = 24,
                    Margin = new Thickness(0, 0, 0, 0),
                };

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

            flowDoc.Blocks.Add(socialParagraph);

            // Introduction
            string introduction = _documentHandler.CurrentResume.Introduction.Text;
            AddParagraph(introduction);
        }

        private void UpdateTechnicalSkills()
        {
            var header = new Paragraph(new Run("Technical Skills"))
            {
                Style = GetStyle("Resume.SectionHeader")
            };

            header.BorderBrush = Brushes.Black;
            header.BorderThickness = new Thickness(0, 0, 0, 1);

            flowDoc.Blocks.Add(header);

            string technicalSkills = string.Join(" ◈ ", _documentHandler.CurrentResume.TechnicalSkills.Where(t => t.Type == "language").Select(t => t.Text));
            AddParagraph(technicalSkills);

            technicalSkills = string.Join(" ◈ ", _documentHandler.CurrentResume.TechnicalSkills.Where(t => t.Type == "framework").Select(t => t.Text));
            AddParagraph(technicalSkills);

            technicalSkills = string.Join(" ◈ ", _documentHandler.CurrentResume.TechnicalSkills.Where(t => t.Type == "tool").Select(t => t.Text));
            AddParagraph(technicalSkills);
        }

        private void UpdateExperience()
        {
            Style headerStyle = DtoStyleToWindowsStyle(_documentHandler.CurrentResume.ExperienceHeader.Style);

            var experienceHeader = new Paragraph(new Run(_documentHandler.CurrentResume.ExperienceHeader.Text))
            {
                Style = headerStyle,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            flowDoc.Blocks.Add(experienceHeader);

            var experience = _documentHandler.CurrentResume.Experience;

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
                flowDoc.Blocks.Add(table);
            }
        }

        private void UpdateEducation()
        {
            var educationHeader = new Paragraph(new Run("Education"))
            {
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 5)
            };

            educationHeader.BorderBrush = Brushes.Black;
            educationHeader.BorderThickness = new Thickness(0, 0, 0, 1);

            flowDoc.Blocks.Add(educationHeader);

            var education = _documentHandler.CurrentResume.Education;

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

                flowDoc.Blocks.Add(table);

                foreach (var bullet in edu.BulletPoints)
                {
                    var bulletParagraph = new Paragraph(new Run($"◇ {bullet}"))
                    {
                        FontSize = 11,
                        Margin = new Thickness(0, 0, 0, 0)
                    };
                    flowDoc.Blocks.Add(bulletParagraph);
                }
            }
        }

        private void UpdateSkills()
        {
            var educationHeader = new Paragraph(new Run("Skills"))
            {
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 5)
            };

            educationHeader.BorderBrush = Brushes.Black;
            educationHeader.BorderThickness = new Thickness(0, 0, 0, 1);

            flowDoc.Blocks.Add(educationHeader);

            var skills = _documentHandler.CurrentResume.Skills;

            foreach (var skill in skills)
            {
                Style style = DtoStyleToWindowsStyle(skill.Style);

                var bulletParagraph = new Paragraph(new Run($"◇ {skill.Text}"))
                {
                    Style = style,
                };
                flowDoc.Blocks.Add(bulletParagraph);
            }
        }

        private Style DtoStyleToWindowsStyle(DocumentHandler.DTO.Style style)
        {
            Style newStyle = new Style();

            newStyle.Setters.Add(new Setter(FontWeightProperty, style.Bold ? FontWeights.Bold : FontWeights.Normal));
            newStyle.Setters.Add(new Setter(FontSizeProperty, double.TryParse(style.FontSize, out double fontSize) ? fontSize : 10));
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

            flowDoc.Blocks.Add(paragraph);

            return true;
        }

        private void Click_OpenResume(object sender, RoutedEventArgs e)
        {
            //ShowResume("selectedPath", "openFileDialog.SafeFileName");
        }

        private void AddTechnicalSkill_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddTechnicalSkillWindow
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                _documentHandler.AddTechnicalSkill(dialog.SkillName, dialog.SkillType.ToLower());
                flowDoc = new FlowDocument();
                UpdateResume();
            }
        }

        private void SaveResume_Click(object sender, RoutedEventArgs e)
        {
            _documentHandler.SaveResume();
            MessageBox.Show("Resume saved successfully!");
        }

        private void EditAddress_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddressWindow
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                flowDoc = new FlowDocument();
                UpdateResume();
            }
        }

        private void EditIntroduction_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new IntroductionWindow
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                flowDoc = new FlowDocument();
                UpdateResume();
            }
        }

        private void AddSocialMediaLink_Click(object sender, RoutedEventArgs e)
        {
            var addSocialMediaDialog = new AddSocialMediaLinkWindow
            {
                Owner = this
            };

            if (addSocialMediaDialog.ShowDialog() == true)
            {
                flowDoc = new FlowDocument();
                UpdateResume();
            }
        }

        private void LoadImage(string path)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();

            ProfileImage.Source = bitmap;
        }

        private void EditSocialMediaLink_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EditSocialMediaLinkWindow
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                flowDoc = new FlowDocument();
                UpdateResume();
            }
        }

        private void ClearSocialMediaLinks_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement clear social media links functionality
             _documentHandler.CurrentResume.SocialMediaLinks.Clear();
             flowDoc = new FlowDocument();
             UpdateResume();
        }
    }
}
