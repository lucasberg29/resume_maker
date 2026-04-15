using DocumentHandler.DTO;
using DocumentHandler.DTO.Attribute;
using DocumentHandler.DTO.Paragraphs;
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
    public partial class EditElementStylingWindow : Window
    {
        Element? Element = null;

        public EditElementStylingWindow()
        {
            InitializeComponent();

            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source).ToList();
        }

        public void UpdateStyling()
        {
            var elementStyle = Element.ElementStyle;

            // Font size
            FontSizeTextBox.Text = elementStyle.FontSize.ToString();

            // Font family
            var fontFamily = Fonts.SystemFontFamilies.FirstOrDefault(f => f.Source == elementStyle.FontFamily);
            if (fontFamily is null)
            {
                fontFamily = Fonts.SystemFontFamilies.FirstOrDefault(f => f.Source.Contains(elementStyle.FontFamily));
            }

            FontFamilyComboBox.SelectedItem = fontFamily;

            // Margin
            string rawMargins = elementStyle.Margin;

            List<string> margins = rawMargins.Split(',').ToList();

            LeftMarginTextBox.Text = margins[0];
            TopMarginTextBox.Text = margins[1];
            RightMarginTextBox.Text = margins[2];
            BottomMarginTextBox.Text = margins[3];

        }

        public EditElementStylingWindow(int elementId) : this()
        {
            Element = MainWindow._wpfDocumentHandler.DocumentHandler.GetElementById(elementId);
            UpdateStyling();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void UpdateElementStylingButton_Click(object sender, RoutedEventArgs e)
        {
            ElementStyle elementStyle = new ElementStyle()
            {

            };

            MainWindow._wpfDocumentHandler.DocumentHandler.UpdateElementStyling(elementStyle, Element.Id);

            DialogResult = true;

            Close();
        }
    }
}
