namespace FSSY_v3.classes;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls.Primitives;

public class PathsManager
{
    public List<string> Paths { get; set; } = new List<string>();
    private readonly string _folderPath;
    private readonly string _filePath;

    public PathsManager()
    {
        _folderPath = MainWindow.FolderPath;
        _filePath = Path.Combine(_folderPath, "paths.json");
        Directory.CreateDirectory(_folderPath);
    }


    public void SavePathsToFile(UniformGrid uniformGrid)
    {
        var pathsManager = new PathsManager();

        foreach (PathGridItem pathGridItem in uniformGrid.Children) {
            pathsManager.Paths.Add(pathGridItem.PathText);
        }

        var json = JsonConvert.SerializeObject(pathsManager, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    public void LoadPathsFromFile(UniformGrid uniformGrid)
    {
        if (!File.Exists(_filePath)) return;
        var json = File.ReadAllText(_filePath);
        var pathsManager = JsonConvert.DeserializeObject<PathsManager>(json);
        this.Paths = pathsManager.Paths;

        for (var i = 0; i < pathsManager.Paths.Count && i < uniformGrid.Children.Count; i++)
        {
            if (uniformGrid.Children[i] is PathGridItem pathGridItem)
            {
                pathGridItem.PathText = pathsManager.Paths[i];
            }
        }
    }

    public List<string> getPaths()
    {
        return Paths;
    }
}