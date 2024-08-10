using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using FSSY_v3.classes;
using Microsoft.Win32;

namespace FSSY_v3;

public partial class PathsPage : Page
{
    private readonly PathsManager _pathsManager;

    public PathsPage()
    {
        InitializeComponent();
        _pathsManager = new PathsManager();
        LoadPathsFromFile();
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

    //Back Button Events
    private void NavigateBack(object sender, RoutedEventArgs e)
    {
        SavePaths();
        var mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.NavigateToMenuPage();
    }


    //Quit Button Event
    public void SaveOnClose(object sender, CancelEventArgs e)
    {
        SavePaths();
    }

    private void SavePaths()
    {
        _pathsManager.SavePathsToFile(PathsGrid);
    }

    private void LoadPathsFromFile()
    {
        _pathsManager.LoadPathsFromFile(PathsGrid);
    }

    public PathsManager getPathsManager()
    {
        return _pathsManager;
    }
}