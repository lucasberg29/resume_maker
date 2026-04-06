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
    /// Interaction logic for AddressWindow.xaml
    /// </summary>
    public partial class AddressWindow : Window
    {
        public AddressWindow()
        {
            InitializeComponent();
            CurrentAddress.Text = MainWindow._wpfDocumentHandler.DocumentHandler.GetLocation().Text;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._wpfDocumentHandler.DocumentHandler.SetLocation(NewAddressInputField.Text.Trim());
            DialogResult = true;
            Close();
        }
    }
}
