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

            FullName.Text = MainWindow._documentHandler.CurrentResume.FullName.Text;  
            PhoneNumber.Text = MainWindow._documentHandler.CurrentResume.PhoneNumber.Text;
        }

        private void NewFullNameInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newValue = NewFullNameInputField.Text;
            FullName.Text = newValue;

            MainWindow._documentHandler.CurrentResume.FullName.Text = newValue;
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            mainWindow.UpdateResume();
        }

        private void NewPhoneNumberInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newValue = NewPhoneNumberInputField.Text;
            PhoneNumber.Text = newValue;

            MainWindow._documentHandler.CurrentResume.PhoneNumber.Text = newValue;
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            mainWindow.UpdateResume();
        }

        private void NewEmailInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newValue = NewEmailInputField.Text;
            Email.Text = newValue;

            MainWindow._documentHandler.CurrentResume.Email.Text = newValue;
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            mainWindow.UpdateResume();
        }

        private void NewLocationInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newValue = NewLocationInputField.Text;
            Location.Text = newValue;

            MainWindow._documentHandler.CurrentResume.Location.Text = newValue;
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            mainWindow.UpdateResume();
        }

        private void SkillsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSkills = SkillsListBox.SelectedItems
                                 .Cast<ListBoxItem>()
                                 .Select(i => i.Content.ToString())
                                 .ToList();



            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            mainWindow.UpdateResume();
        }
    }
}
