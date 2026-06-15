using Microsoft.Win32;
using ResumeHandlerGUI.Windows;
using System.Windows;

namespace ResumeHandlerGUI.Managers
{
    public class WindowManager
    {
        private readonly MainWindow _owner;

        public WindowManager(MainWindow owner)
        {
            _owner = owner;
        }

        public void AddSocialMediaLink()
        {
            var dialog = new AddSocialMediaLinkWindow();

            if (ShowDialog(dialog))
            {
                _owner.UpdateUI();
                _owner.UpdateResume();
            }
        }

        public void EditParagraphStyling(int paragraphId)
        {
            var dialog = new EditParagraphStylingWindow(paragraphId);
            if (ShowDialog(dialog))
            {
                _owner.UpdateUI();
                _owner.UpdateResume();
            }
        }

        public void EditElementStyling(int elementId)
        {
            var dialog = new EditElementStylingWindow(elementId);
            if (ShowDialog(dialog))
            {
                _owner.UpdateUI();
                _owner.UpdateResume();
            }
        }

        public void EditSocialMediaLink(int id)
        {
            var dialog = new EditSocialMediaLinkWindow(id);

            if (ShowDialog(dialog))
            {
                _owner.UpdateUI();
                _owner.UpdateResume();
            }
        }

        public void EditTechnicalSkill(int id)
        {
            
        }

        public void AddExperience()
        {
            var dialog = new AddExperienceWindow();
            if (ShowDialog(dialog))
            {
                MainWindow._wpfDocumentHandler.UpdateResume();
            }
        }

        public void AddTechnicalSkill(string skillType)
        {
            var dialog = new AddTechnicalSkillWindow(skillType);
            if (ShowDialog(dialog))
            {
                _owner.UpdateUI();
                _owner.UpdateResume();
            }
        }

        public void CreateNewResumeWindow()
        {
            var dialog = new CreateNewResumeWindow();
            if (ShowDialog(dialog))
            {
                MainWindow._wpfDocumentHandler.DocumentHandler.CreateNewResume(dialog.NewResumeNameInputField.Text);

                string resumeFileName = string.Concat(dialog.NewResumeNameInputField.Text, ".json");

                Properties.Settings.Default.LastResume = resumeFileName;
                Properties.Settings.Default.Save();

                MainWindow._wpfDocumentHandler.UpdateResume();
            }
        }

        public void ExportToDocx()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Word Documents (*.docx)|*.docx|All Files (*.*)|*.*",
                Title = "Export Resume to Docx",
                DefaultExt = "docx"
            };

            if (dialog.ShowDialog() == true)
            {
                bool savedSuccessfully = MainWindow._wpfDocumentHandler.DocumentHandler.ExportResumeToDOCX(dialog.FileName);

                if (savedSuccessfully)
                {
                    MessageBox.Show("Saved Successfully!");
                }
            }
        }

        private bool ShowDialog(Window dialog)
        {
            dialog.Owner = _owner;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (dialog.ShowDialog() == true)
            {
                return true;
            }

            return false;
        }
    }
}
