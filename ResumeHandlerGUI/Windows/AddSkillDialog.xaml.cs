using System.Windows;
using System.Windows.Controls;

namespace ResumeHandlerGUI
{
    public partial class AddSkillDialog : Window
    {
        public string SkillName { get; private set; } = "";
        public string SkillType { get; private set; } = "";

        public AddSkillDialog()
        {
            InitializeComponent();
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

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
