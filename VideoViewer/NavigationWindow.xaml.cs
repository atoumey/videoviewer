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
using Microsoft.Win32;
using System.IO;
using System.IO.IsolatedStorage;

namespace VideoViewer
{
    /// <summary>
    /// Interaction logic for NavigationWindow.xaml
    /// </summary>
    public partial class NavigationWindow : Window
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

        public DirectoryInfo CurrentDir { get; set; }
        private DirectoryInfo _currentDir;

        public NavigationWindow()
        {
            InitializeComponent();

            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
            string[] fileNames = storage.GetFileNames("config.txt");
            if (fileNames.Length != 0)
            {
                byte r, g, b;
                StreamReader reader = new StreamReader(new IsolatedStorageFileStream("config.txt", FileMode.Open, storage));
                AppPath = reader.ReadLine();
                DefaultViewPath = reader.ReadLine();
                reader.ReadLine();
                byte.TryParse(reader.ReadLine(), out r);
                byte.TryParse(reader.ReadLine(), out g);
                byte.TryParse(reader.ReadLine(), out b);
                FolderBrush = new SolidColorBrush(Color.FromRgb(r, g, b));
                reader.ReadLine();
                byte.TryParse(reader.ReadLine(), out r);
                byte.TryParse(reader.ReadLine(), out g);
                byte.TryParse(reader.ReadLine(), out b);
                FolderTextBrush = new SolidColorBrush(Color.FromRgb(r, g, b));
                reader.ReadLine();
                byte.TryParse(reader.ReadLine(), out r);
                byte.TryParse(reader.ReadLine(), out g);
                byte.TryParse(reader.ReadLine(), out b);
                FileBrush = new SolidColorBrush(Color.FromRgb(r, g, b));
                reader.ReadLine();
                byte.TryParse(reader.ReadLine(), out r);
                byte.TryParse(reader.ReadLine(), out g);
                byte.TryParse(reader.ReadLine(), out b);
                FileTextBrush = new SolidColorBrush(Color.FromRgb(r, g, b));
            }
            else
            {
                AppPath = "C:\\Program Files\\VideoLAN\\VLC\\vlc.exe";
                DefaultViewPath = "";
                FolderBrush = new SolidColorBrush(Colors.Aquamarine);
                FolderTextBrush = new SolidColorBrush(Colors.Black);
                FileBrush = new SolidColorBrush(Colors.White);
                FileTextBrush = new SolidColorBrush(Colors.Black);                
            }

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.CanResize;
            this.WindowState = WindowState.Maximized;

            if (DefaultViewPath == "")
            {
                System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
                dlg.ShowDialog();
                if (dlg.SelectedPath == "") Close();
                activateDirectory(dlg.SelectedPath);

            }
            else
                activateDirectory(DefaultViewPath);            
        }

        public void activateDirectory(string path)
        {
            try
            {
                DirectoryInfo NewDir = new DirectoryInfo(path);
                DirectoryInfo[] directories = NewDir.GetDirectories();
                FileInfo[] files = NewDir.GetFiles();
                navigationPanel.Children.Clear();
                Button buttonToAdd;

                foreach (DirectoryInfo dinfo in directories)
                {
                    buttonToAdd = new Button();
                    buttonToAdd.Content = dinfo.Name;
                    buttonToAdd.HorizontalContentAlignment = HorizontalAlignment.Left;
                    buttonToAdd.FontWeight = FontWeights.Heavy;
                    buttonToAdd.FontSize = 50.0;
                    buttonToAdd.Background = FolderBrush;
                    buttonToAdd.Foreground = FolderTextBrush;
                    buttonToAdd.Click += OnButtonClick;
                    navigationPanel.Children.Add(buttonToAdd);
                }

                foreach (FileInfo finfo in files)
                {
                    buttonToAdd = new Button();
                    buttonToAdd.Content = finfo.Name;
                    buttonToAdd.HorizontalContentAlignment = HorizontalAlignment.Left;
                    buttonToAdd.FontSize = 50.0;
                    buttonToAdd.Background = FileBrush;
                    buttonToAdd.Foreground = FileTextBrush;
                    buttonToAdd.Click += OnButtonClick;
                    navigationPanel.Children.Add(buttonToAdd);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show("You can't access that directory", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show("The directory can't be found", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            CurrentDir = new DirectoryInfo(path);
            currentPathBar.Content = CurrentDir.FullName;
        }

        protected void OnButtonClick(object sender, RoutedEventArgs args)
        {
            if (((Button)sender).FontWeight == FontWeights.Heavy)
                activateDirectory(CurrentDir.FullName + "\\" + ((Button)sender).Content);
            else
            {
                System.Diagnostics.Process.Start(AppPath, "\"" + CurrentDir + "\\" + ((Button)sender).Content + "\"");
            }
        }

        protected void OnUpButtonClick(object sender, RoutedEventArgs args)
        {
            if (CurrentDir.Parent != null)
                activateDirectory(CurrentDir.Parent.FullName);
        }

        protected void OnClosed(object sender, EventArgs args)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
            StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream("config.txt", FileMode.Create, storage));
            writer.WriteLine(AppPath);
            writer.WriteLine(DefaultViewPath);
            writer.WriteLine("Folder Background (RGB)");
            writer.WriteLine(FolderBrush.Color.R);
            writer.WriteLine(FolderBrush.Color.G);
            writer.WriteLine(FolderBrush.Color.B);
            writer.WriteLine("Folder Text (RGB)");
            writer.WriteLine(FolderTextBrush.Color.R);
            writer.WriteLine(FolderTextBrush.Color.G);
            writer.WriteLine(FolderTextBrush.Color.B);
            writer.WriteLine("File Background (RGB)");
            writer.WriteLine(FileBrush.Color.R);
            writer.WriteLine(FileBrush.Color.G);
            writer.WriteLine(FileBrush.Color.B);
            writer.WriteLine("File Text (RGB)");
            writer.WriteLine(FileTextBrush.Color.R);
            writer.WriteLine(FileTextBrush.Color.G);
            writer.WriteLine(FileTextBrush.Color.B);
            writer.Close();
        }

        private void OnOptionsButtonClick(object sender, RoutedEventArgs e)
        {
            OptionsWindow options = new OptionsWindow(AppPath, DefaultViewPath, FolderBrush, FolderTextBrush, FileBrush, FileTextBrush);
            options.ShowDialog();

            AppPath = options.AppPath;
            DefaultViewPath = options.DefaultViewPath;
            FolderBrush = options.FolderBrush;
            FolderTextBrush = options.FolderTextBrush;
            FileBrush = options.FileBrush;
            FileTextBrush = options.FileTextBrush;

            activateDirectory(CurrentDir.FullName);
        }
    }
}
