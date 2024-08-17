using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using FSSY_v3.classes;
using Microsoft.Win32;

namespace FSSY_v3
{
    //TODO Implement saving to pathsManager
    public partial class PathOverlay : Page
    {
        private const string DefaultPath = "C:/Your/File/Path";
        private readonly MenuPage _menuPage;

        public PathOverlay(MenuPage menuPage)
        {
            _menuPage = menuPage;
            InitializeComponent();
            FillPathElements();
        }

        public void SavePaths()
        {
            PathsManager.SaveOverlayPath(GameExePath.Text, GameExePath);
            PathsManager.SaveOverlayPath(CloudDrivePath.Text, CloudDrivePath);
            PathsManager.SaveOverlayPath(FfsExePath.Text, FfsExePath);
            PathsManager.SaveOverlayPath(SavegameDirPath.Text, SavegameDirPath);
        }

        private void FillPathElements()
        {
            FillPaths();
            FillToolTips();
        }

        private void FillPaths()
        {
            GameExePath.Text = PathsManager.GameExePath.Equals("") ? DefaultPath : PathsManager.GameExePath;
            CloudDrivePath.Text = PathsManager.CloudDrivePath.Equals("") ? DefaultPath : PathsManager.CloudDrivePath;
            FfsExePath.Text = PathsManager.FfsExePath.Equals("") ? DefaultPath : PathsManager.FfsExePath;
            SavegameDirPath.Text = PathsManager.SavegameDirectoryPath.Equals("")
                ? DefaultPath
                : PathsManager.SavegameDirectoryPath;
        }

        private void FillToolTips()
        {
            GameExePath.ToolTip = GameExePath.Text;
            CloudDrivePath.ToolTip = CloudDrivePath.Text;
            FfsExePath.ToolTip = FfsExePath.Text;
            SavegameDirPath.ToolTip = SavegameDirPath.Text;
        }

        private void HandleClose(object sender, RoutedEventArgs routedEventArgs)
        {
            _menuPage.CloseModal();
        }

        private void HandleFileSelect(object sender, RoutedEventArgs e)
        {
            var file = PromptFileSelect();
            if (file == null) return;
            InsertPath(file, FindAssociatedTextBox(sender));
        }

        private void HandleFolderSelect(object sender, RoutedEventArgs e)
        {
            var folder = PromptFolderSelect();
            if (folder == null) return;
            InsertPath(folder, FindAssociatedTextBox(sender));
        }

        private static string? PromptFolderSelect()
        {
            var openFolderDialog = new OpenFolderDialog();
            var valid = openFolderDialog.ShowDialog();
            if (valid == true)
            {
                return openFolderDialog.FolderName;
            }

            MessageBox.Show("No valid folder selected.", "Path selection", MessageBoxButton.OK, MessageBoxImage.Error);
            return null;
        }

        private static string? PromptFileSelect()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "FreeFileSync Files (.ffs_batch)|*.ffs_batch;*.ffs_real"
            };

            var valid = openFileDialog.ShowDialog();
            if (valid == true)
            {
                return openFileDialog.FileName;
            }

            MessageBox.Show("No valid file selected.", "Path selection", MessageBoxButton.OK, MessageBoxImage.Error);
            return null;
        }

        private static TextBox FindAssociatedTextBox(object sender)
        {
            var senderButton = sender as Button;
            var parent = senderButton?.Parent as Grid;
            var children = parent?.Children;

            if (children == null) throw new TargetException("Button has no associated TextBox");

            foreach (UIElement child in children)
            {
                if (child is TextBox textBox)
                {
                    return textBox;
                }
            }

            throw new TargetException("Button has no associated TextBox");
        }

        private void InsertPath(string folder, TextBox targetTextBox)
        {
            targetTextBox.Text = folder;
            SavePaths();
        }
    }
}