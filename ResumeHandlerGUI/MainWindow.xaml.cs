using ResumeHandlerGUI.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ResumeHandlerGUI
{
    public partial class MainWindow : Window
    {
        private readonly WindowManager _windowManager;

        public static DocumentHandler.DocumentHandler _documentHandler;
        FlowDocument flowDoc = new FlowDocument();

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
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                FontFamily = new FontFamily("Roboto Serif, Arial, sans-serif")
            };

            var FullNameParagraph = new System.Windows.Documents.Paragraph(text)
            {
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 5),
            };

            flowDoc.Blocks.Add(FullNameParagraph);

            string contatInfo = $"{_documentHandler.CurrentResume.Email} - {_documentHandler.CurrentResume.PhoneNumber} - {_documentHandler.CurrentResume.Location}";
            AddParagraph(contatInfo);

            string introduction = _documentHandler.CurrentResume.Introduction;
            AddParagraph(introduction);

            var header = new Paragraph(new Run("Technical Skills"))
            {
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 5)
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
                // Create table with 2 columns
                var table = new Table();

                // Left column (Title), Right column (Location)
                table.Columns.Add(new TableColumn() { Width = new GridLength(3, GridUnitType.Star) });
                table.Columns.Add(new TableColumn() { Width = new GridLength(1, GridUnitType.Star) });

                // One row in a row group
                var rowGroup = new TableRowGroup();
                var row = new TableRow();

                // Title cell
                var titleCell = new TableCell(new Paragraph(new Run(exp.JobTitle))
                {
                    FontSize = 11,
                    FontWeight = FontWeights.Bold
                });

                // Location cell (right aligned)
                var locationCell = new TableCell(new Paragraph(new Run(exp.Location))
                {
                    FontSize = 11,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Right
                });

                // Add cells and row
                row.Cells.Add(titleCell);
                row.Cells.Add(locationCell);
                rowGroup.Rows.Add(row);
                table.RowGroups.Add(rowGroup);

                flowDoc.Blocks.Add(table);

                foreach (var bullet in exp.BulletPoints)
                {
                    var bulletParagraph = new Paragraph(new Run($"◇ {bullet}"))
                    {
                        FontSize = 11,
                        Margin = new Thickness(20, 0, 0, 5)
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
            var dialog = new AddSkillDialog
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
            var dialog = new AddSocialMediaLinkWindow
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                flowDoc = new FlowDocument();
                UpdateResume();
            }
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
