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
    /// Interaction logic for EditElementStyling.xaml
    /// </summary>
    public partial class EditElementStylingWindow : Window
    {
        Element? Element = null;

        public EditElementStylingWindow()
        {
            InitializeComponent();
        }

        public EditElementStylingWindow(int elementId) : this()
        {
            Element = MainWindow._wpfDocumentHandler.DocumentHandler.GetElementById(elementId);
        }
    }
}
