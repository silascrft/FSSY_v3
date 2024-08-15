using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace FSSY_v3;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static readonly string FolderPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FSSY");

    private readonly PathsPage _pathsPage = new PathsPage();
    private readonly MenuPage _menuPage = new MenuPage();

    public MainWindow()
    {
        InitializeComponent();
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
        _menuPage.SaveOnClose(sender, e);
        _pathsPage.SaveOnClose(sender, e);
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