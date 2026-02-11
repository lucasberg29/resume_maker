using DocumentHandler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AddExperienceWindow : Window
    {
        private ObservableCollection<BulletPoint> _bulletPoints = new();
        public IReadOnlyList<string> BulletPoints =>
        _bulletPoints
        .Where(b => !string.IsNullOrWhiteSpace(b.Text))
        .Select(b => b.Text.Trim())
        .ToList();


        public AddExperienceWindow()
        {
            InitializeComponent();

            _bulletPoints.Add(new BulletPoint());
            BulletPointsItemsControl.ItemsSource = _bulletPoints;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Experience added!");

            Experience experience = new Experience()
            {
                CompanyName = CompanyNameTextBox.Text,
                JobTitle = JobTitleTextBox.Text,
                Location = JobLocationTextBox.Text,
                CompanyWebsiteLink = CompanyWebsiteTextBox.Text,
                StartDate = StartDatePicker.SelectedDate ?? DateTime.MinValue,
                EndDate = EndDatePicker.SelectedDate ?? DateTime.MinValue,
                BulletPoints = BulletPoints.ToList()
            };
            
            MainWindow._documentHandler.AddExperience(experience);

            DialogResult = true;
            Close();
        }

        private void AddBullet_Click(object sender, RoutedEventArgs e)
        {
            _bulletPoints.Add(new BulletPoint()
            {
                Text = ""
            });
        }

        private void RemoveBullet_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is BulletPoint bullet)
            {
                _bulletPoints.Remove(bullet);
            }
        }
    }

    public class BulletPoint
    {
        public string Text { get; set; } = "";
    }
}
