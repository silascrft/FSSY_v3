using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using FSSY_v3.classes;
using Microsoft.Win32;

namespace FSSY_v3;

public partial class Paths : Page
{
    private readonly PathsManager _pathsManager;

    private void OpenFileButton_Click(object sender, RoutedEventArgs e)
    {
        // Erstelle eine Instanz von OpenFileDialog
        OpenFileDialog openFileDialog = new OpenFileDialog();

        // Optional: Setze Filter für die Dateitypen
        openFileDialog.Filter = "Textdateien (*.txt)|*.txt|Alle Dateien (*.*)|*.*|FreeFileSynch Batch (.ffs_batch)|*.ffs_batch|FreeFileSynch RealTime (.ffs_real)|*.ffs_real";

        // Öffne den Dialog
        bool? result = openFileDialog.ShowDialog();

        if (result == true)
        {
            // Hole den ausgewählten Dateipfad
            string filePath = openFileDialog.FileName;

            // Hier kannst du machen, was du mit der Datei tun möchtest
            MessageBox.Show($"Datei ausgewählt: {filePath}");
        }
    }

    public Paths()
    {
        InitializeComponent();
        _pathsManager = new PathsManager();
        LoadPathsFromFile();
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
}