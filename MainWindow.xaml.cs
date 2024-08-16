using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using FSSY_v3.classes;

namespace FSSY_v3;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static readonly string FolderPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FSSY");

    private readonly PathsPage _pathsPage;
    private readonly MenuPage _menuPage;

    public MainWindow()
    {
        InitializeComponent();
        PathsManager.LoadPathsFromFile();
        _pathsPage = new PathsPage();
        _menuPage = new MenuPage();
        MainFrame.Navigate(_menuPage);
    }

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed)
        {
            this.DragMove();
        }
    }

    private void MinimizeButton(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    public void ExitButton(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _menuPage.SaveStates();
        _pathsPage.SavePaths();
        PathsManager.SavePathsToFile();
    }

    public void NavigateToPathPage()
    {
        MainFrame.Navigate(_pathsPage);
    }

    public void NavigateToMenuPage()
    {
        MainFrame.Navigate(_menuPage);
    }


    public PathsPage GetPathsPage()
    {
        return _pathsPage;
    }

    public MenuPage GetMenuPage()
    {
        return _menuPage;
    }
}