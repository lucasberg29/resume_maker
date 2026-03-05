using DocumentFormat.OpenXml.Presentation;
using ResumeHandlerGUI.Windows;
using System.Windows;
using System.Windows.Documents;

namespace ResumeHandlerGUI
{
    public class WindowManager
    {
        private readonly MainWindow _owner;

        public WindowManager(MainWindow owner)
        {
            _owner = owner;
        }

        public void EditFullName()
        {
            var dialog = new FullNameWindow();

            if (ShowDialog(dialog))
            {
                MainWindow._wpfDocumentHandler.UpdateResume();
            }
        }

        public void EditEmail()
        {
            var dialog = new EmailWindow();

            if (ShowDialog(dialog))
            {
                MainWindow._wpfDocumentHandler.UpdateResume();
            }
        }

        public void EditPhoneNumber()
        {
            var dialog = new PhoneNumberWindow();

            if (ShowDialog(dialog))
            {
                MainWindow._wpfDocumentHandler.UpdateResume();
            }
        }

        public void EditAddress()
        {
            var dialog = new AddressWindow();

            if (ShowDialog(dialog))
            {
                MainWindow._wpfDocumentHandler.UpdateResume();
            }
        }

        public void EditIntroduction()
        {
            var dialog = new IntroductionWindow();

            if (ShowDialog(dialog))
            {
                MainWindow._wpfDocumentHandler.UpdateResume();
            }
        }

        public void AddSocialMediaLink()
        {
            var dialog = new AddSocialMediaLinkWindow();

            if (ShowDialog(dialog))
            {
                MainWindow._wpfDocumentHandler.UpdateResume();
            }
        }

        public void EditSocialMediaLink(string socialMediaLinkName)
        {
            var dialog = new EditSocialMediaLinkWindow(socialMediaLinkName);

            if (ShowDialog(dialog))
            {
                _owner.UpdateResume();
            }
        }

        public void AddExperience()
        {
            var dialog = new AddExperienceWindow();
            if (ShowDialog(dialog))
            {
                MainWindow._wpfDocumentHandler.UpdateResume();
            }
        }

        public void AddTechnicalSkill()
        {
            var dialog = new AddTechnicalSkillWindow();
            if (ShowDialog(dialog))
            {
                MainWindow._wpfDocumentHandler.UpdateResume();
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
