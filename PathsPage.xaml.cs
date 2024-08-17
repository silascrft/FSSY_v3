using System.Windows;
using System.Windows.Controls;
using FSSY_v3.classes;

namespace FSSY_v3;

public partial class PathsPage : Page
{

    private readonly MainWindow _mainWindow;

    public PathsPage(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        InitializeComponent();
        FillPaths();
    }

    public void SavePaths()
    {
        PathsManager.SaveBatchPaths(PathsGrid);
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

    private void HandleBack(object sender, RoutedEventArgs e)
    {
        _mainWindow.NavigateToPage(_mainWindow._menuPage);
        SavePaths();
    }
}