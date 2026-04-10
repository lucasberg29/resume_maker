using ResumeHandlerGUI.Handlers;
using ResumeHandlerGUI.Managers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResumeHandlerGUI
{
    public partial class MainWindow : Window
    {
        public static WindowManager? _windowManager;
        public static readonly WPFDocumentHandler _wpfDocumentHandler = new();
        public static UiManager _uiManager = new();

        public MainWindow()
        {
            _windowManager = new WindowManager(this);

            InitializeComponent();

            UpdateResume();

            CreateHeaderRibbons();

            CheckErrors();  
        }

        public void Update()
        {
            _wpfDocumentHandler.UpdateResume();
            DocViewer.Document = _wpfDocumentHandler.FlowDocument;
        }

        private void CheckErrors()
        {
            var errors = DocumentHandler.Handlers.DocumentHandler.ErrorHandler.GetErros();

            if (errors.Count > 0)
            {
                string allErrors = string.Join("\n\n", errors.Select(e => $"Error in {e.Location} at line {e.LineNumber}: {e.Message}"));
                MessageBox.Show($"Resume loaded with {errors.Count} errors. Please check the error log for details. \n{allErrors}", "Resume Loaded", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CreateHeaderRibbons()
        {
            MenuRibbonSelected.Content = _uiManager._headerRibbon;
        }

        private void MenuOption_Click(object sender, RoutedEventArgs e)
        {
            SelectMenu((MenuItem)sender);

            switch (sender)
            {
                case MenuItem menuItem when menuItem == Resume:
                    MenuRibbonSelected.Content = _uiManager._resumeRibbon;
                    break;
                case MenuItem menuItem when menuItem == Header:
                    MenuRibbonSelected.Content = _uiManager._headerRibbon;
                    break;
                case MenuItem menuItem when menuItem == TechnicalSkills:
                    MenuRibbonSelected.Content = _uiManager._technicalSkillsRibbon;
                    break;
                case MenuItem menuItem when menuItem == Experience:
                    MenuRibbonSelected.Content = _uiManager._experienceRibbon;
                    break;
                case MenuItem menuItem when menuItem == Education:
                    MenuRibbonSelected.Content = _uiManager._educationRibbon;
                    break;
                case MenuItem menuItem when menuItem == OtherExperience:
                    MenuRibbonSelected.Content = _uiManager._otherExperienceRibbon;
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

        public void UpdateUI()
        {
            _uiManager._headerRibbon.UpdateFields();
            _uiManager._technicalSkillsRibbon.UpdateFields();
            _uiManager._experienceRibbon.UpdateFields();
            _uiManager._educationRibbon.UpdateFields();
            _uiManager._otherExperienceRibbon.UpdateFields();
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
