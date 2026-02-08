
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ResumeHandlerGUI
{
    public partial class MainWindow : Window
    {
        private readonly DocumentHandler.DocumentHandler _documentHandler;
        FlowDocument flowDoc = new FlowDocument();

        public MainWindow()
        {
            InitializeComponent();

            _documentHandler = new DocumentHandler.DocumentHandler();
            _documentHandler.InitHandler();

            ShowResume("", "");
        }

        private void CreateAndPreview_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "[2026 Dev] LucasBerg_Resume.docx";
            string docPath = _documentHandler.CreateSampleDocument(fileName);

            ShowResume(docPath, fileName);
        }

        private void ShowResume(string docPath, string safeFileName)
        {
            _documentHandler.LoadResumeFromDocument(docPath, safeFileName);
            UpdateResume();
        }

        private void UpdateResume()
        {
            var text = new System.Windows.Documents.Run(_documentHandler.CurrentResume.FullName)
            {
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                FontFamily = new FontFamily("Roboto Serif")
            };

            var FullNameParagraph = new System.Windows.Documents.Paragraph(text)
            {
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 5),
            };

            flowDoc.Blocks.Add(FullNameParagraph);

            string contatInfo = $"{_documentHandler.CurrentResume.Email} - {_documentHandler.CurrentResume.PhoneNumber} - {_documentHandler.CurrentResume.Location}";
            AddParagraph(contatInfo);

            string technicalSkills = string.Join(" ◈ ", _documentHandler.CurrentResume.TechnicalSkills.Where(t => t.Type == "language").Select(t => t.Name));
            AddParagraph(technicalSkills);

            string experienceHeader = "Experience";
            AddHeader(experienceHeader);

            DocViewer.Document = flowDoc;
        }

        private void AddHeader(string experienceHeader)
        {
            var text = new System.Windows.Documents.Run(experienceHeader)
            {
                FontFamily = new FontFamily("Roboto Serif"),
                FontWeight = FontWeights.Bold,
                FontSize = 12
            };

            var paragraph = new System.Windows.Documents.Paragraph(text)
            {
                Margin = new Thickness(0, 0, 0, 5),
            };

            flowDoc.Blocks.Add(paragraph);
        }

        private bool AddParagraph(string paragraphText)
        {
            var text = new System.Windows.Documents.Run(paragraphText)
            {
                FontSize = 12,
                FontFamily = new FontFamily("Roboto Serif")
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
        }
    }
}
