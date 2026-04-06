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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResumeHandlerGUI.Views
{
    /// <summary>
    /// Interaction logic for TechnicalSkillsRibbon.xaml
    /// </summary>
    public partial class TechnicalSkillsRibbon : UserControl
    {
        private List<TechnicalSkill> languages = new();
        private List<TechnicalSkill> frameworks = new();
        private List<TechnicalSkill> tools = new();

        public TechnicalSkillsRibbon()
        {
            InitializeComponent();
            UpdateFields();
        }

        public void UpdateFields()
        {
            UpdateLanguages();
            UpdateFrameworks();
            UpdateTools();
        }

        private void UpdateLanguages()
        {
            LanguagesListBox.Items.Clear();
            languages = MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.AllTechnicalSkills.
                TechnicalSkills.Where(t => t.Type == "language").ToList();

            foreach (var language in languages)
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var nameBlock = new TextBlock
                {
                    Text = language.Text,
                    VerticalAlignment = VerticalAlignment.Center
                };

                var button = new Button
                {
                    Content = "⚙",
                    Width = 20,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0, 0, 0, 0)
                };

                button.Click += (s, e) =>
                {
                    MainWindow._windowManager.EditSocialMediaLink(language.Id);
                    MainWindow._wpfDocumentHandler.UpdateResume();
                };

                Grid.SetColumn(nameBlock, 0);
                Grid.SetColumn(button, 1);
                grid.Children.Add(nameBlock);
                grid.Children.Add(button);

                var item = new ListBoxItem
                {
                    Content = grid,
                    IsSelected = language.Active,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch
                };

                LanguagesListBox.Items.Add(item);
            }

        }
        private void UpdateFrameworks()
        {
            FrameworksListBox.Items.Clear();
            frameworks = MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.AllTechnicalSkills.
                TechnicalSkills.Where(t => t.Type == "framework").ToList();

            foreach (var framework in frameworks)
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var nameBlock = new TextBlock
                {
                    Text = framework.Text,
                    VerticalAlignment = VerticalAlignment.Center
                };

                var button = new Button
                {
                    Content = "⚙",
                    Width = 20,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0, 0, 0, 0)
                };

                button.Click += (s, e) =>
                {
                    MainWindow._windowManager.EditSocialMediaLink(framework.Id);
                    MainWindow._wpfDocumentHandler.UpdateResume();
                };

                Grid.SetColumn(nameBlock, 0);
                Grid.SetColumn(button, 1);
                grid.Children.Add(nameBlock);
                grid.Children.Add(button);

                var item = new ListBoxItem
                {
                    Content = grid,
                    IsSelected = framework.Active,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch
                };

                FrameworksListBox.Items.Add(item);
            }

        }

        private void UpdateTools()
        {
            ToolsListBox.Items.Clear();
            tools = MainWindow._wpfDocumentHandler.DocumentHandler.CurrentResume.AllTechnicalSkills.
                TechnicalSkills.Where(t => t.Type == "tool").ToList();

            foreach (var tool in tools)
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var nameBlock = new TextBlock
                {
                    Text = tool.Text,
                    VerticalAlignment = VerticalAlignment.Center
                };

                var button = new Button
                {
                    Content = "⚙",
                    Width = 20,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0, 0, 0, 0)
                };

                button.Click += (s, e) =>
                {
                    MainWindow._windowManager.EditSocialMediaLink(tool.Id);
                    MainWindow._wpfDocumentHandler.UpdateResume();
                };

                Grid.SetColumn(nameBlock, 0);
                Grid.SetColumn(button, 1);
                grid.Children.Add(nameBlock);
                grid.Children.Add(button);

                var item = new ListBoxItem
                {
                    Content = grid,
                    IsSelected = tool.Active,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch
                };

                ToolsListBox.Items.Add(item);
            }
        }



        private void LanguagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                var listBoxItem = listBox.Items[i] as ListBoxItem;
                var grid = listBoxItem.Content as Grid;

                var linkNameBlock = grid.Children[0] as TextBlock;
                string socialMediaLink = linkNameBlock.Text.ToString();
                var isActive = listBoxItem.IsSelected;
                MainWindow._wpfDocumentHandler.DocumentHandler.SetTechnicalSkillActive(languages[i].Id, isActive);
            }

            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.UpdateResume();
            }
        }

        private void FrameworksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                var listBoxItem = listBox.Items[i] as ListBoxItem;
                var grid = listBoxItem.Content as Grid;

                var linkNameBlock = grid.Children[0] as TextBlock;
                string socialMediaLink = linkNameBlock.Text.ToString();
                var isActive = listBoxItem.IsSelected;
                MainWindow._wpfDocumentHandler.DocumentHandler.SetTechnicalSkillActive(frameworks[i].Id, isActive);
            }

            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.UpdateResume();
            }
        }

        private void ToolsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                var listBoxItem = listBox.Items[i] as ListBoxItem;
                var grid = listBoxItem.Content as Grid;

                var linkNameBlock = grid.Children[0] as TextBlock;
                string socialMediaLink = linkNameBlock.Text.ToString();
                var isActive = listBoxItem.IsSelected;
                MainWindow._wpfDocumentHandler.DocumentHandler.SetTechnicalSkillActive(tools[i].Id, isActive);
            }

            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.UpdateResume();
            }
        }

        private void AddFrameworkButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._windowManager.AddTechnicalSkill("framework");
            MainWindow._wpfDocumentHandler.UpdateResume();
        }

        private void AddToolButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._windowManager.AddTechnicalSkill("tool");
            MainWindow._wpfDocumentHandler.UpdateResume();
        }

        private void AddLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._windowManager.AddTechnicalSkill("language");
            MainWindow._wpfDocumentHandler.UpdateResume();
        }
    }
}
