using DocumentHandler.DTO;
using System.Windows;
using System.Windows.Controls;

namespace ResumeHandlerGUI
{
    public partial class AddTechnicalSkillWindow : Window
    {
        public string SkillName { get; private set; } = "";
        public string SkillType { get; private set; } = "";

        public AddTechnicalSkillWindow(string technicalSkill)
        {
            InitializeComponent();

            SkillType = technicalSkill;

            TypeComboBox.SelectedIndex = technicalSkill switch
            {
                "language" => 0,
                "framework" => 1,
                "tool" => 2,
                _ => -1
            };


        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            SkillName = NameTextBox.Text.Trim();
            SkillType = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(SkillName))
            {
                MessageBox.Show("Please enter a skill name.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TechnicalSkill technicalSkill = new TechnicalSkill()
            {
                Text = SkillName,
                Tag = SkillType.ToLower(),  
            };

            MainWindow._wpfDocumentHandler.DocumentHandler.AddTechnicalSkill(technicalSkill);
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
