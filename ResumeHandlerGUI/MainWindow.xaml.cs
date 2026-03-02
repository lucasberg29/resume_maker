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
    public partial class MainWindow : Window
    {
        private readonly WindowManager _windowManager;

        //public static DocumentHandler.DocumentHandler _documentHandler = new();

        public static WPFDocumentHandler _wpfDocumentHandler = new();
        FlowDocument flowDoc = new FlowDocument();

        private static Style GetStyle(string key)
        {
            return (Style)Application.Current.FindResource(key);
        }

        public MainWindow()
        {
            _windowManager = new WindowManager(this);

            _wpfDocumentHandler = new WPFDocumentHandler();

            InitializeComponent();
            UpdateResume();
        }

        private void SubscribeClicksToWindowManager()
        {
            // TODO: Add all click events to the window manager

        }

        public void UpdateResume()
        {
            _wpfDocumentHandler.UpdateResume();
            DocViewer.Document = _wpfDocumentHandler.FlowDocument;
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
                _wpfDocumentHandler.DocumentHandler.AddTechnicalSkill(dialog.SkillName, dialog.SkillType.ToLower());
                flowDoc = new FlowDocument();
                _wpfDocumentHandler.UpdateResume();
            }
        }

        private void SaveResume_Click(object sender, RoutedEventArgs e)
        {
            _wpfDocumentHandler.DocumentHandler.SaveResume();
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
                _wpfDocumentHandler.UpdateResume();
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
                _wpfDocumentHandler.UpdateResume();
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
                _wpfDocumentHandler.UpdateResume();
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
                _wpfDocumentHandler.UpdateResume();
            }
        }

        private void ClearSocialMediaLinks_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement clear social media links functionality
            _wpfDocumentHandler.DocumentHandler.CurrentResume.SocialMediaLinks.Clear();
             flowDoc = new FlowDocument();
            _wpfDocumentHandler.UpdateResume();
        }

        private void ExportToDOCX_Click(object sender, RoutedEventArgs e)
        {
            _wpfDocumentHandler.DocumentHandler.ExportResumeToDOCX();
            MessageBox.Show("Resume exported to DOCX successfully!");
        }

        private void ExportToPDF_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                _wpfDocumentHandler.DocumentHandler.SaveResume();
                MessageBox.Show("Resume saved!");
            }

            if (e.Key == Key.F5)
            {
                _wpfDocumentHandler.UpdateResume();
            }
        }
    }
}
