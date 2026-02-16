using DocumentFormat.OpenXml.Spreadsheet;
using DocumentHandler;
using Microsoft.Win32;
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

namespace ResumeHandlerGUI
{
    public partial class AddSocialMediaLinkWindow : Window
    {
        private SocialMediaLink _socialMediaLink = new SocialMediaLink();

        public AddSocialMediaLinkWindow()
        {
            InitializeComponent();
        }

        private void BrowseFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "PNG Images (*.png)|*.png|JPEG Images (*.jpg;*.jpeg)|*.jpg;*.jpeg",
                Title = "Select Profile Picture"
            };

            if (dialog.ShowDialog() == true)
            {
                _socialMediaLink.FileName = dialog.FileName;
                BrowseFileTextBox.Text = dialog.FileName;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // tester
            string name = "Portfolio Website";

            _socialMediaLink.FilePath = BrowseFileTextBox.Text;
            _socialMediaLink.FileName = System.IO.Path.GetFileName(BrowseFileTextBox.Text);
            _socialMediaLink.Name = NameTextBox.Text;
            _socialMediaLink.Hyperlink = HyperlinkTextBox.Text.Trim();
            _socialMediaLink.Alt = AltTextBox.Text.Trim();

            MainWindow._documentHandler.AddSocialMediaLink(_socialMediaLink);
            DialogResult = true;
            Close();
        }
    }
}
