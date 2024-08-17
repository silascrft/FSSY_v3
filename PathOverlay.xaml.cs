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

        public PathOverlay()
        {
            InitializeComponent();
            FillPaths();
            FillToolTips();
        }

        private void FillPaths()
        {
            GameExePath.Text = PathsManager.GameExePath.Equals("") ? DefaultPath : PathsManager.GameExePath;
            CloudDrivePath.Text = PathsManager.CloudDrivePath.Equals("") ? DefaultPath : PathsManager.CloudDrivePath;
            FfsExePath.Text = PathsManager.FfsExePath.Equals("") ? DefaultPath : PathsManager.FfsExePath;
            SavegameDirPath.Text = PathsManager.SavegameDirectoryPath.Equals("") ? DefaultPath : PathsManager.SavegameDirectoryPath;
        }

        private void FillToolTips()
        {
            GameExePath.ToolTip = GameExePath.Text;
            CloudDrivePath.ToolTip = CloudDrivePath.Text;
            FfsExePath.ToolTip = FfsExePath.Text;
            SavegameDirPath.ToolTip = SavegameDirPath.Text;
        }

        private void HandleClose(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.GetMenuPage().GetModalFrame().Navigate(null);
        }

        private void HandleFileSelect(object sender, RoutedEventArgs e)
        {
            var senderButton = sender as Button;
            var parent = senderButton.Parent as Grid;
            var children = parent.Children;
            foreach (UIElement child in children)
            {
                if (child is TextBox textBox)
                {
                    InsertPathFromFileSelect(textBox);
                    return;
                }
            }
        }

        private static void InsertPathFromFileSelect(TextBox targetTextBox)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "FreeFileSync Files (.ffs_batch)|*.ffs_batch;*.ffs_real";

            var result = openFileDialog.ShowDialog();
            if (result != true) return;

            var filePath = openFileDialog.FileName;
            targetTextBox.Text = filePath;
        }

        public void SavePaths()
        {
            PathsManager.SaveOverlayPath(GameExePath.Text, GameExePath);
            PathsManager.SaveOverlayPath(CloudDrivePath.Text, CloudDrivePath);
            PathsManager.SaveOverlayPath(FfsExePath.Text, FfsExePath);
            PathsManager.SaveOverlayPath(SavegameDirPath.Text, SavegameDirPath);
        }
    }
}
