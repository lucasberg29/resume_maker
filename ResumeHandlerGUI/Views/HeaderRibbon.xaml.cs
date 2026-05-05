using DocumentFormat.OpenXml.Presentation;
using DocumentHandler.DTO;
using DocumentHandler.DTO.Paragraphs;
using DocumentHandler.DTO.Section;
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
        ResumeParagraph fullNameParagraph;
        ResumeParagraph contactParagraph;

        Element? fullNameElement;
        Element? phoneNumberElement;
        Element? emailElement;
        Element? locationElement;
        Element? introductionElement;

        List<SocialMediaLink> socialMediaLinks = new List<SocialMediaLink>();

        public HeaderRibbon()
        {
            InitializeComponent();
            UpdateFields();
        }

        public void OnSelected()
        {
            UpdateFields();
        }

        public void UpdateFields()
        {
            var personalInfo = MainWindow._wpfDocumentHandler.DocumentHandler.GetPersonalInfo();
            UpdatePersonalInfo(personalInfo);
            UpdateSocialMediaLinks(personalInfo);
        }

        private void UpdateSocialMediaLinks(PersonalInfo personalInfo)
        {
            SocialMediaLinksListBox.Items.Clear();

            var socialMediaLinks = personalInfo.SocialMediaLinks.OrderBy(socialMediaLink => socialMediaLink.Position).ToList();

            foreach (var socialMediaLink in socialMediaLinks)
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var nameBlock = new TextBlock
                {
                    Text = socialMediaLink.Name,
                    VerticalAlignment = VerticalAlignment.Center
                };

                Grid.SetColumn(nameBlock, 0);
                grid.Children.Add(nameBlock);

                var moveUpInOrderButton = new Button
                {
                    Content = "↑",
                    Width = 20,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0, 0, 5, 0)
                };

                moveUpInOrderButton.Click += (s, e) =>
                {
                    MainWindow._wpfDocumentHandler.MoveUpInOrder(socialMediaLink);
                    MainWindow._wpfDocumentHandler.UpdateResume();
                    MainWindow._uiManager.Update();
                };

                Grid.SetColumn(moveUpInOrderButton, 1);
                grid.Children.Add(moveUpInOrderButton);

                var editButton = new Button
                {
                    Content = "⚙",
                    Width = 20,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0, 0, 0, 0)
                };

                editButton.Click += (s, e) =>
                {
                    MainWindow._windowManager.EditSocialMediaLink(socialMediaLink.Id);
                    MainWindow._wpfDocumentHandler.UpdateResume();
                };

                Grid.SetColumn(editButton, 2);
                grid.Children.Add(editButton);

                var item = new ListBoxItem
                {
                    Content = grid,
                    IsSelected = socialMediaLink.Active,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch
                };

                SocialMediaLinksListBox.Items.Add(item);
            }
        }

        private void UpdatePersonalInfo(PersonalInfo personalInfo)
        {
            fullNameParagraph = personalInfo.FullName;
            contactParagraph = personalInfo.Contact;
            socialMediaLinks = personalInfo.SocialMediaLinks;

            fullNameElement = personalInfo.FullName.Elements.First();

            phoneNumberElement = MainWindow._wpfDocumentHandler.DocumentHandler.GetPhoneNumber();
            emailElement = MainWindow._wpfDocumentHandler.DocumentHandler.GetEmail();
            locationElement = MainWindow._wpfDocumentHandler.DocumentHandler.GetLocation();
            introductionElement = MainWindow._wpfDocumentHandler.DocumentHandler.GetIntroduction();

            FullName.Text = personalInfo.FullName.Elements.First().Text;

            Email.Text = personalInfo.Contact.Elements[0].Text;
            PhoneNumber.Text = personalInfo.Contact.Elements[1].Text;
            Location.Text = personalInfo.Contact.Elements[2].Text;

            MaskPhoneNumber();

            Introduction.Text = MainWindow._wpfDocumentHandler.DocumentHandler.GetIntroduction().Text;
        }

        private void MaskPhoneNumber()
        {
            var phoneNumber = PhoneNumber.Text;

            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            var phoneNumberOnlyNumbers = phoneNumber;
            if (phoneNumberOnlyNumbers.Length < 10)
            {
                return;
            }

            var maskedPhoneNumber = "";

            maskedPhoneNumber += $"({phoneNumberOnlyNumbers[0]}{phoneNumberOnlyNumbers[1]}{phoneNumberOnlyNumbers[2]})";
            maskedPhoneNumber += $" {phoneNumberOnlyNumbers[3]}{phoneNumberOnlyNumbers[4]}{phoneNumberOnlyNumbers[5]}";
            maskedPhoneNumber += $" - {phoneNumberOnlyNumbers[6]}{phoneNumberOnlyNumbers[7]}{phoneNumberOnlyNumbers[8]}{phoneNumberOnlyNumbers[9]}";

            PhoneNumber.Text = maskedPhoneNumber; 
        }

        public void UpdateMainWindow()
        {
            MainWindow? mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.UpdateResume();
            }
        }

        private void NewFullNameInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            FullName.Text = NewFullNameInputField.Text;

            MainWindow._wpfDocumentHandler.DocumentHandler.SetFullName(FullName.Text);
            UpdateMainWindow();
        }

        private void NewPhoneNumberInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            PhoneNumber.Text = NewPhoneNumberInputField.Text;

            MaskPhoneNumber();

            MainWindow._wpfDocumentHandler.DocumentHandler.SetPhoneNumber(PhoneNumber.Text);
            UpdateMainWindow();
        }

        private void NewEmailInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            Email.Text = NewEmailInputField.Text;

            MainWindow._wpfDocumentHandler.DocumentHandler.SetEmail(Email.Text);
            UpdateMainWindow();
        }

        private void NewLocationInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            Location.Text = NewLocationInputField.Text;

            MainWindow._wpfDocumentHandler.DocumentHandler.SetLocation(Location.Text);
            UpdateMainWindow();
        }

        private void NewIntroductionInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            Introduction.Text = NewIntroductionInputField.Text;

            MainWindow._wpfDocumentHandler.DocumentHandler.SetIntroduction(Introduction.Text);
            UpdateMainWindow();
        }

        private void SkillsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                var listBoxItem = listBox.Items[i] as ListBoxItem;
                var grid = listBoxItem.Content as Grid;

                var linkNameBlock = grid.Children[0] as TextBlock;
                string socialMediaLink = linkNameBlock.Text.ToString();
                var isActive = listBoxItem.IsSelected;
                MainWindow._wpfDocumentHandler.DocumentHandler.SetSocialMediaLinkActive(socialMediaLinks[i].Id, isActive);
            }

            UpdateMainWindow();
        }

        private void AddLinkButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._windowManager.AddSocialMediaLink();
            MainWindow._wpfDocumentHandler.UpdateResume();
        }

        private void UpdateLinkButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FirtParagraphButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._windowManager?.EditParagraphStyling(fullNameParagraph.Id);
        }

        private void SecondParagraphButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._windowManager?.EditParagraphStyling(contactParagraph.Id);
        }

        private void FullNameStylingButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._windowManager?.EditElementStyling(fullNameElement.Id);
        }
    }
}
