using System.IO;
using System.Windows;
using System.Windows.Controls;
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

    public readonly PathsPage _pathsPage;
    public readonly MenuPage _menuPage;

    public MainWindow()
    {
        InitializeComponent();
        PathsManager.LoadPathsFromFile();
        _pathsPage = new PathsPage(this);
        _menuPage = new MenuPage(this);
        MainFrame.Navigate(_menuPage);
    }

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    private void MinimizeButton(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    public void ExitButton(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _menuPage.SaveStates();
        _pathsPage.SavePaths();
        PathsManager.SavePathsToFile();
    }

    public void NavigateToPage(Page page)
    {
        MainFrame.Navigate(page);
    }
}