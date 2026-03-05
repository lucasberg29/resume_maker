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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResumeHandlerGUI.Views
{
    /// <summary>
    /// Interaction logic for HeaderRibbon.xaml
    /// </summary>
    public partial class HeaderRibbon : UserControl
    {
        public HeaderRibbon()
        {
            InitializeComponent();
            UpdateFields();
        }

        public void OnSelected()
        {
            UpdateFields();
        }

        private void UpdateFields()
        {
            FullName.Text = MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.FullName.Text;
            PhoneNumber.Text = MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.PhoneNumber.Text;
            Introduction.Text = MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.Introduction.Text;

            SocialMediaLinksListBox.Items.Clear();

            foreach (var socialMediaLink in MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.SocialMediaLinks)
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var nameBlock = new TextBlock
                {
                    Text = socialMediaLink.Name,
                    VerticalAlignment = VerticalAlignment.Center
                };

                var button = new Button
                {
                    Content = "⚙",
                    Width = 20,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0, 0, 0, 0)
                };

                button.Click += (s, e) =>
                {
                    MainWindow._windowManager.EditSocialMediaLink(socialMediaLink.Name);
                    MainWindow._wpfDocumentHandler.UpdateResume();
                };

                Grid.SetColumn(nameBlock, 0);
                Grid.SetColumn(button, 1);
                grid.Children.Add(nameBlock);
                grid.Children.Add(button);

                var item = new ListBoxItem
                {
                    Content = grid,
                    IsSelected = socialMediaLink.Active,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch
                };

                SocialMediaLinksListBox.Items.Add(item);
            }
        }

        private void NewFullNameInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newValue = NewFullNameInputField.Text;
            FullName.Text = newValue;

            MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.FullName.Text = newValue;
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            mainWindow.UpdateResume();
        }

        private void NewPhoneNumberInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newValue = NewPhoneNumberInputField.Text;
            PhoneNumber.Text = newValue;

            MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.PhoneNumber.Text = newValue;
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            mainWindow.UpdateResume();
        }

        private void NewEmailInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newValue = NewEmailInputField.Text;
            Email.Text = newValue;

            MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.Email.Text = newValue;
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            mainWindow.UpdateResume();
        }

        private void NewLocationInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newValue = NewLocationInputField.Text;
            Location.Text = newValue;

            MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.Location.Text = newValue;
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            mainWindow.UpdateResume();
        }

        private void NewIntroductionInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newValue = NewIntroductionInputField.Text;
            Introduction.Text = newValue;

            MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.Introduction.Text = newValue;
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.UpdateResume();
        }

        private void SkillsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            foreach (var item in listBox.Items)
            {
                var listBoxItem = item as ListBoxItem;
                var grid = listBoxItem.Content as Grid;

                var linkNameBlock = grid.Children[0] as TextBlock;
                string socialMediaLink = linkNameBlock.Text.ToString();
                var isActive = listBoxItem.IsSelected;
                MainWindow._wpfDocumentHandler.DocumentHandler.SetSocialMediaLinkActive(socialMediaLink, isActive);
            }

            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.UpdateResume();
            }
        }

        private void AddLinkButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._windowManager.AddSocialMediaLink();
            MainWindow._wpfDocumentHandler.UpdateResume();
        }

        private void UpdateLinkButton_Click(object sender, RoutedEventArgs e)
        {

        }   
    }
}
