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
    /// <summary>
    /// Interaction logic for EditParagraphStyling.xaml
    /// </summary>
    public partial class EditParagraphStylingWindow : Window
    {
        ResumeParagraph? Paragraph = null;    

        public EditParagraphStylingWindow()
        {

            InitializeComponent();
        }

        public void UpdateStyling()
        {
            string rawMargins = Paragraph.ParagraphStyle.Margin;    

            List<string> margins = rawMargins.Split(',').ToList();

            LeftMarginTextBox.Text = margins[0];
            TopMarginTextBox.Text = margins[1]; 
            RightMarginTextBox.Text = margins[2];
            BottomMarginTextBox.Text = margins[3];

            string rawPaddings = Paragraph.ParagraphStyle.Padding;

            List<string> padding = rawPaddings.Split(',').ToList();

            LeftMarginTextBox.Text = padding[0];
            TopMarginTextBox.Text = padding[1];
            RightMarginTextBox.Text = padding[2];
            BottomMarginTextBox.Text = padding[3];

            switch (Paragraph.ParagraphStyle.TextAlignment)
            {
                case "left": 
                    AlignLeft.IsChecked = true; 
                    break;
                case "center": 
                    AlignCenter.IsChecked = true;
                    break;
                case "right": 
                    AlignRight.IsChecked = true; 
                    break;
                case "justify": 
                    AlignJustify.IsChecked = true;
                    break;
            }
        }

        public EditParagraphStylingWindow(int paragraphId) : this()
        {
            Paragraph = MainWindow._wpfDocumentHandler.DocumentHandler.GetParagraphById(paragraphId);

            UpdateStyling();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void UpdateParagraphStylingButton_Click(object sender, RoutedEventArgs e)
        {
            ParagraphStyle paragraphStyle = new ParagraphStyle()
            {
                Margin = $"{LeftMarginTextBox.Text},{TopMarginTextBox.Text},{RightMarginTextBox.Text},{BottomMarginTextBox.Text}",
                Padding = $"{LeftPaddingTextBox.Text},{TopPaddingTextBox.Text},{RightPaddingTextBox.Text},{BottomPaddingTextBox.Text}",
                TextAlignment = AlignLeft.IsChecked == true ? "left" : AlignCenter.IsChecked == true ? "center" : AlignRight.IsChecked == true ? "right" : "justify"
            };
          
            MainWindow._wpfDocumentHandler.DocumentHandler.UpdateParagraphStyling(paragraphStyle, Paragraph.Id);

            DialogResult=true;  

            Close();
        }
    }
}
