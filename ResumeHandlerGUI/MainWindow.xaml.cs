using DocumentFormat.OpenXml.Presentation;
using Microsoft.Win32;
using ResumeHandlerGUI.Views;
using ResumeHandlerGUI.Windows;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ResumeHandlerGUI
{
    public partial class MainWindow : Window
    {
        public static WindowManager? _windowManager;
        public static readonly WPFDocumentHandler _wpfDocumentHandler = new();

        FlowDocument _flowDoc = new FlowDocument();

        private ResumeRibbon _resumeRibbon = new ResumeRibbon();
        private HeaderRibbon _headerRibbon = new HeaderRibbon();
        private TechnicalSkillsRibbon _technicalSkillsRibbon = new TechnicalSkillsRibbon();
        private ExperienceRibbon _experienceRibbon = new ExperienceRibbon();
        private EducationRibbon _educationRibbon = new EducationRibbon();
        private OtherExperienceRibbon _otherExperienceRibbon = new OtherExperienceRibbon();

        public MainWindow()
        {
            _windowManager = new WindowManager(this);

            InitializeComponent();

            UpdateResume();

            CreateHeaderRibbons();
        }

        private void CreateHeaderRibbons()
        {
            MenuRibbonSelected.Content = _headerRibbon;
        }

        private void SubscribeClicksToWindowManager()
        {
            // TODO: Add all click events to the window manager
        }

        private void MenuOption_Click(object sender, RoutedEventArgs e)
        {
            SelectMenu((MenuItem)sender);

            switch (sender)
            {
                case MenuItem menuItem when menuItem == Resume:
                    MenuRibbonSelected.Content = _resumeRibbon;
                    break;
                case MenuItem menuItem when menuItem == Header:
                    MenuRibbonSelected.Content = _headerRibbon;
                    break;
                case MenuItem menuItem when menuItem == TechnicalSkills:
                    MenuRibbonSelected.Content = _technicalSkillsRibbon;
                    break;
                case MenuItem menuItem when menuItem == Experience:
                    MenuRibbonSelected.Content = _experienceRibbon;
                    break;
                case MenuItem menuItem when menuItem == Education:
                    MenuRibbonSelected.Content = _educationRibbon;
                    break;
                case MenuItem menuItem when menuItem == OtherExperience:
                    MenuRibbonSelected.Content = _otherExperienceRibbon;
                    break;
            }
        }

        private void SelectMenu(MenuItem selectedItem)
        {
            foreach (var item in MainMenu.Items)
            {
                if (item is MenuItem menuItem)
                    menuItem.IsChecked = false;
            }

            selectedItem.IsChecked = true;
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
                _flowDoc = new FlowDocument();
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
                _flowDoc = new FlowDocument();
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
                _flowDoc = new FlowDocument();
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
                _flowDoc = new FlowDocument();
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
                _flowDoc = new FlowDocument();
                _wpfDocumentHandler.UpdateResume();
            }
        }

        private void ClearSocialMediaLinks_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement clear social media links functionality
            _wpfDocumentHandler.DocumentHandler.CurrentResume.SocialMediaLinks.Clear();
            _flowDoc = new FlowDocument();
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
