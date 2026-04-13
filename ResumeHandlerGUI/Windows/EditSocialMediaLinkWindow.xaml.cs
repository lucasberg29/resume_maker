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
        int _socialMediaLinkId = 0;

        public EditSocialMediaLinkWindow(int id)
        {
            _socialMediaLinkId = id;

            InitializeComponent();

            LoadSocialMediaLink();
        }

        private void LoadSocialMediaLink()
        {
            var socialMediaLink = MainWindow._wpfDocumentHandler.DocumentHandler.GetPersonalInfo().SocialMediaLinks
                .FirstOrDefault(s => s.Id == _socialMediaLinkId);

            if (socialMediaLink != null)
            {
                LinkSelected.Text = socialMediaLink.Name;   
                FileText.Text = socialMediaLink.FileName;
                NameTextBox.Text = socialMediaLink.Name;
                HyperlinkTextBox.Text = socialMediaLink.ElementStyle.Hyperlink;
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
            var socialMediaLink = MainWindow._wpfDocumentHandler.DocumentHandler.GetSocialMediaLinkById(_socialMediaLinkId);

            socialMediaLink.FileName = FileText.Text;
            socialMediaLink.ElementStyle.Hyperlink = HyperlinkTextBox.Text;
            socialMediaLink.Alt = AltTextBox.Text;

            MainWindow._wpfDocumentHandler.DocumentHandler.UpdateSocialMediaLink(socialMediaLink);

            MainWindow._wpfDocumentHandler.UpdateResume();

            DialogResult = true;

            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var socialMediaLink = MainWindow._wpfDocumentHandler.DocumentHandler.GetSocialMediaLinkById(_socialMediaLinkId);
            bool deletedSuccesfully = MainWindow._wpfDocumentHandler.DocumentHandler.DeleteSocialMediaLink(_socialMediaLinkId);

            if (deletedSuccesfully)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Failed to delete the social media link.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);    
            }
        }
    }
}
