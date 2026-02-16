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

            var text = new Run(_documentHandler.CurrentResume.FullName)
            {
                Style = GetStyle("Resume.FullName")
            };

            var FullNameParagraph = new System.Windows.Documents.Paragraph(text)
            {
                Style = GetStyle("Resume.FullNameParagraph")
            };

            flowDoc.Blocks.Add(FullNameParagraph);

            string contatInfo = $"{_documentHandler.CurrentResume.Email} - {_documentHandler.CurrentResume.PhoneNumber} - {_documentHandler.CurrentResume.Location}";
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
            string introduction = _documentHandler.CurrentResume.Introduction;
            AddParagraph(introduction);

            var header = new Paragraph(new Run("Technical Skills"))
            {
                Style = GetStyle("Resume.SectionHeader")
            };

            header.BorderBrush = Brushes.Black;
            header.BorderThickness = new Thickness(0, 0, 0, 1);

            flowDoc.Blocks.Add(header);

            string technicalSkills = string.Join(" ◈ ", _documentHandler.CurrentResume.TechnicalSkills.Where(t => t.Type == "language").Select(t => t.Name));
            AddParagraph(technicalSkills);

            technicalSkills = string.Join(" ◈ ", _documentHandler.CurrentResume.TechnicalSkills.Where(t => t.Type == "framework").Select(t => t.Name));
            AddParagraph(technicalSkills);

            technicalSkills = string.Join(" ◈ ", _documentHandler.CurrentResume.TechnicalSkills.Where(t => t.Type == "tool").Select(t => t.Name));
            AddParagraph(technicalSkills);

            var experienceHeader = new Paragraph(new Run("Experience"))
            {
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 5)
            };

            experienceHeader.BorderBrush = Brushes.Black;
            experienceHeader.BorderThickness = new Thickness(0, 0, 0, 1);

            flowDoc.Blocks.Add(experienceHeader);

            var experience = _documentHandler.CurrentResume.Experience;

            foreach (var exp in experience)
            {
                var table = new Table();

                table.Columns.Add(new TableColumn() { Width = new GridLength(3, GridUnitType.Star) });
                table.Columns.Add(new TableColumn() { Width = new GridLength(1, GridUnitType.Star) });

                var rowGroup = new TableRowGroup();
                var row = new TableRow();

                var titleCell = new TableCell(new Paragraph(new Run(exp.JobTitle))
                {
                    FontSize = 11,
                    FontWeight = FontWeights.Bold
                });

                var locationCell = new TableCell(new Paragraph(new Run(exp.Location))
                {
                    FontSize = 11,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Right
                });

                row.Cells.Add(titleCell);
                row.Cells.Add(locationCell);
                rowGroup.Rows.Add(row);

                var secondRow = new TableRow();

                var companyNameCell = new TableCell(new Paragraph(new Run(exp.CompanyName))
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

                table.RowGroups.Add(rowGroup);

                flowDoc.Blocks.Add(table);

                foreach (var bullet in exp.BulletPoints)
                {
                    var bulletParagraph = new Paragraph(new Run($"◇ {bullet}"))
                    {
                        FontSize = 11,
                        Margin = new Thickness(0, 0, 0, 0)
                    };
                    flowDoc.Blocks.Add(bulletParagraph);
                }
            }

            DocViewer.Document = flowDoc;
        }

        private bool AddParagraph(string paragraphText)
        {
            var text = new System.Windows.Documents.Run(paragraphText)
            {
                FontSize = 11,
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
            ShowResume("selectedPath", "openFileDialog.SafeFileName");
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
