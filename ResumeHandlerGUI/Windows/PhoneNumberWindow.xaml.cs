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
    /// <summary>
    /// Interaction logic for PhoneNumberWindow.xaml
    /// </summary>
    public partial class PhoneNumberWindow : Window
    {
        public PhoneNumberWindow()
        {
            InitializeComponent();
            CurrentPhoneNumber.Text = MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.PhoneNumber.Text;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.PhoneNumber.Text = NewPhoneNumberInputField.Text.Trim();
            DialogResult = true;
            Close();
        }
    }
}
