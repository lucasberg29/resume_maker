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
    /// Interaction logic for FullNameWindow.xaml
    /// </summary>
    public partial class FullNameWindow : Window
    {
        public FullNameWindow()
        {
            InitializeComponent();
            CurrentFullName.Text = MainWindow._documentHandler.CurrentResume.FullName.Text;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._documentHandler.CurrentResume.FullName.Text = NewFullNameInputField.Text.Trim();
            DialogResult = true;
            Close();
        }
    }
}
