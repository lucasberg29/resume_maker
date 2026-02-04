using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Win32;

namespace ResumeHandlerGUI
{
    public partial class MainWindow : Window
    {
        private readonly DocumentHandler.DocumentHandler _handler;

        public MainWindow()
        {
            InitializeComponent();

            _handler = new DocumentHandler.DocumentHandler();
            _handler.InitHandler();
        }

        private void CreateAndPreview_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "[2026 Dev] LucasBerg_Resume.docx";

            string docPath = _handler.CreateSampleDocument(fileName);

            ShowResume(docPath);
        }

        private void ShowResume(string docPath)
        {
            var flowDoc = new FlowDocument();

            using var wordDoc = WordprocessingDocument.Open(docPath, false);
            var body = wordDoc.MainDocumentPart.Document.Body;

            foreach (var para in body.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
            {
                // Check if this paragraph has a bullet
                bool isBullet = para.ParagraphProperties?.NumberingProperties != null;

                string text = string.Join("", para.Descendants<Text>().Select(t => t.Text));

                // Prepend bullet symbol if detected
                if (isBullet) text = "• " + text;

                var wpfParagraph = new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(text))
                {
                    
                    Margin = new Thickness(0, 0, 0, 5) // add spacing between paragraphs
                };

                flowDoc.Blocks.Add(wpfParagraph);
            }

            DocViewer.Document = flowDoc;
        }


        // Button: Open existing resume
        private void Click_OpenResume(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Path.Combine(AppContext.BaseDirectory, "Data", "Resume"),
                Filter = "Word Documents (*.docx)|*.docx",
                Title = "Select a Resume"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedPath = openFileDialog.FileName;


                ShowResume(selectedPath);
            }
        }
    }
}
