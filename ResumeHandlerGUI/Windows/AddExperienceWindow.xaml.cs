using DocumentHandler.DTO;
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
                CompanyName = new Element(CompanyNameTextBox.Text),
                JobTitle = new Element(JobTitleTextBox.Text),
                Location = new Element(JobLocationTextBox.Text),
                CompanyWebsiteLink = new Element(CompanyWebsiteTextBox.Text),
                StartDate = StartDatePicker.SelectedDate ?? DateTime.MinValue,
                EndDate = EndDatePicker.SelectedDate ?? DateTime.MinValue,
            };

            var bulletPoints = BulletPoints.ToList();
            foreach (var bullet in bulletPoints)
            {
                experience.BulletPoints.Add(new Element(bullet));
            }

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
