using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using FSSY_v3.classes;
using Microsoft.Win32;

namespace FSSY_v3;

public partial class PathsPage : Page
{

    public PathsPage()
    {
        InitializeComponent();
        FillPaths();
    }

    private void FillPaths()
    {
        var uniformGrid = PathsGrid;
        var batchPaths = PathsManager.BatchPaths;
        
        for (var i = 0; i < batchPaths.Count && i < uniformGrid.Children.Count; i++)
        {
            if (uniformGrid.Children[i] is PathGridItem pathGridItem)
            {
                pathGridItem.PathText = batchPaths[i];
            }
        }
    }

    private void OpenFileButton_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Textdateien (*.txt)|*.txt|Alle Dateien (*.*)|*.*|FreeFileSynch Batch (.ffs_batch)|*.ffs_batch|FreeFileSynch RealTime (.ffs_real)|*.ffs_real";

        var result = openFileDialog.ShowDialog();

        if (result == true)
        {
            var filePath = openFileDialog.FileName;
            MessageBox.Show($"Datei ausgewählt: {filePath}");
        }
    }
    
    private void NavigateBack(object sender, RoutedEventArgs e)
    {
        SavePaths();
        var mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.NavigateToMenuPage();
    }
    
    public void SaveOnClose(object sender, CancelEventArgs e)
    {
        SavePaths();
    }

    private void SavePaths()
    {
        PathsManager.SaveBatchPathsToFile(PathsGrid);
    }
}