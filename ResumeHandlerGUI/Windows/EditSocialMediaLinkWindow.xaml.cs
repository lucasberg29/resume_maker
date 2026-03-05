using DocumentFormat.OpenXml.Wordprocessing;
using DocumentHandler.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ResumeHandlerGUI.Windows
{
    /// <summary>
    /// Interaction logic for EditSocialMediaLinkWindow.xaml
    /// </summary>
    public partial class EditSocialMediaLinkWindow : Window
    {
        string _socialMediaLinkName = "";

        public EditSocialMediaLinkWindow(string socialMediaLinkName)
        {
            _socialMediaLinkName = socialMediaLinkName;

            InitializeComponent();

            LoadSocialMediaLink();
        }

        private void LoadSocialMediaLink()
        {
            var socialMediaLink = MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.SocialMediaLinks
                .FirstOrDefault(s => s.Name == _socialMediaLinkName);

            if (socialMediaLink != null)
            {
                FileText.Text = socialMediaLink.FileName;
                NameTextBox.Text = socialMediaLink.Name;
                HyperlinkTextBox.Text = socialMediaLink.Hyperlink;
                AltTextBox.Text = socialMediaLink.Alt;
            }
        }

        private void BrowseFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateSocialMediaLinkButton_Click(object sender, RoutedEventArgs e)
        {
            var socialMediaLink = MainWindow._wpfDocumentHandler.DocumentHandler.GetSocialMediaLinkByName(_socialMediaLinkName);

            socialMediaLink.FileName = FileText.Text;
            socialMediaLink.Hyperlink = HyperlinkTextBox.Text;
            socialMediaLink.Alt = AltTextBox.Text;

            MainWindow._wpfDocumentHandler.DocumentHandler.UpdateSocialMediaLink(socialMediaLink);

            MainWindow._wpfDocumentHandler.UpdateResume();

            DialogResult = true;

            Close();
        }

    }
}
