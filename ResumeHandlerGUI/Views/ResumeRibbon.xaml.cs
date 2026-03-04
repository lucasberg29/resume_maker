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
    /// Interaction logic for ResumeRibbon.xaml
    /// </summary>
    public partial class ResumeRibbon : UserControl
    {
        public ResumeRibbon()
        {
            InitializeComponent();
        }

        private void NewResumeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            MainWindow._windowManager.CreateNewResumeWindow();
                
            //MainWindow._wpfDocumentHandler.DocumentHandler.CreateNewResume("New Resume");
            mainWindow.UpdateResume();
        }

        private void SaveResumeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            MainWindow._wpfDocumentHandler.DocumentHandler.SaveResume();
            mainWindow.UpdateResume();
        }
    }
}
