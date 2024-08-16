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
    
    private void NavigateBack(object sender, RoutedEventArgs e)
    {
        var mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.NavigateToMenuPage();
    }
    
    public void SavePaths()
    {
        PathsManager.SaveBatchPaths(PathsGrid);
    }
}