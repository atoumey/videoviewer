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

namespace VideoViewer
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public string AppPath { get; set; }
        private string _appPath;

        public string DefaultViewPath { get; set; }
        private string _defaultViewPath;

        public SolidColorBrush FolderBrush { get; set; }
        private SolidColorBrush _folderBrush;

        public SolidColorBrush FolderTextBrush { get; set; }
        private SolidColorBrush _folderTextBrush;

        public SolidColorBrush FileBrush { get; set; }
        private SolidColorBrush _fileBrush;

        public SolidColorBrush FileTextBrush { get; set; }
        private SolidColorBrush _fileTextBrush;

        public OptionsWindow()
        {
            InitializeComponent();
        }

        public OptionsWindow(string appPath, string defaultViewPath, SolidColorBrush folderBrush, SolidColorBrush folderTextBrush, SolidColorBrush fileBrush, SolidColorBrush fileTextBrush)
        {
            InitializeComponent();

            this.AppPath = appPath;
            this.DefaultViewPath = defaultViewPath;
            this.FolderBrush = folderBrush;
            this.FolderTextBrush = folderTextBrush;
            this.FileBrush = fileBrush;
            this.FileTextBrush = fileTextBrush;

            AppPathTextBox.Text = AppPath;
            DefaultViewPathTextBox.Text = DefaultViewPath;
            FolderButton.Background = FolderBrush;
            FolderTextButton.Background = FolderTextBrush;
            FileButton.Background = fileBrush;
            FileTextButton.Background = FileTextBrush;
        }

        private void OnFolderButtonClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog colorPicker = new System.Windows.Forms.ColorDialog();
            colorPicker.Color = System.Drawing.Color.FromArgb(((SolidColorBrush)FolderButton.Background).Color.R, ((SolidColorBrush)FolderButton.Background).Color.G, ((SolidColorBrush)FolderButton.Background).Color.B);
            colorPicker.FullOpen = true;
            colorPicker.ShowDialog();

            FolderButton.Background = new SolidColorBrush(Color.FromRgb(colorPicker.Color.R, colorPicker.Color.G, colorPicker.Color.B));
            FolderBrush = (SolidColorBrush)FolderButton.Background;
        }

        private void OnFolderTextButtonClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog colorPicker = new System.Windows.Forms.ColorDialog();
            colorPicker.Color = System.Drawing.Color.FromArgb(((SolidColorBrush)FolderButton.Background).Color.R, ((SolidColorBrush)FolderButton.Background).Color.G, ((SolidColorBrush)FolderButton.Background).Color.B);
            colorPicker.FullOpen = true;
            colorPicker.ShowDialog();

            FolderTextButton.Background = new SolidColorBrush(Color.FromRgb(colorPicker.Color.R, colorPicker.Color.G, colorPicker.Color.B));
            FolderTextBrush = (SolidColorBrush)FolderTextButton.Background;
        }

        private void OnFileButtonClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog colorPicker = new System.Windows.Forms.ColorDialog();
            colorPicker.Color = System.Drawing.Color.FromArgb(((SolidColorBrush)FolderButton.Background).Color.R, ((SolidColorBrush)FolderButton.Background).Color.G, ((SolidColorBrush)FolderButton.Background).Color.B);
            colorPicker.FullOpen = true;
            colorPicker.ShowDialog();

            FileButton.Background = new SolidColorBrush(Color.FromRgb(colorPicker.Color.R, colorPicker.Color.G, colorPicker.Color.B));
            FileBrush = (SolidColorBrush)FileButton.Background;
        }

        private void OnFileTextButtonClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog colorPicker = new System.Windows.Forms.ColorDialog();
            colorPicker.Color = System.Drawing.Color.FromArgb(((SolidColorBrush)FolderButton.Background).Color.R, ((SolidColorBrush)FolderButton.Background).Color.G, ((SolidColorBrush)FolderButton.Background).Color.B);
            colorPicker.FullOpen = true;
            colorPicker.ShowDialog();

            FileTextButton.Background = new SolidColorBrush(Color.FromRgb(colorPicker.Color.R, colorPicker.Color.G, colorPicker.Color.B));
            FileTextBrush = (SolidColorBrush)FileTextButton.Background;
        }

        protected void OnOKClick(object sender, RoutedEventArgs args)
        {
            Close();
        }

        protected void OnDefaultFolderButtonClick(object sender, RoutedEventArgs args)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.ShowDialog();
            DefaultViewPath = dlg.SelectedPath;
            DefaultViewPathTextBox.Text = DefaultViewPath;
        }

        protected void OnAppPathClick(object sender, RoutedEventArgs args)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.ShowDialog();
            AppPath = dlg.FileName;
            AppPathTextBox.Text = AppPath;
        }
    }
}