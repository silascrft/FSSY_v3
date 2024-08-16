using System.Windows;
using System.Windows.Controls;

namespace FSSY_v3.classes;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls.Primitives;


public class PathsDto
{
    public List<string> BatchPaths { get; set; }
    public string GameExePath { get; set; }
    public string CloudDrivePath { get; set; }
    public string FfsExePath { get; set; }
    public string SavegameDirectoryPath { get; set; }

    public PathsDto(List<string> batchPaths, string gameExePath, string cloudDrivePath, string ffsExePath, string savegameDirectoryPath)
    {
        BatchPaths = batchPaths;
        GameExePath = gameExePath;
        CloudDrivePath = cloudDrivePath;
        FfsExePath = ffsExePath;
        SavegameDirectoryPath = savegameDirectoryPath;
    }
}

public static class PathsManager
{
    private static List<string> _batchPaths = [];
    private static readonly string _folderPath = MainWindow.FolderPath;
    private static readonly string _filePath  = Path.Combine(_folderPath, "paths.json");
    public static string GameExePath { get; private set; } = "";
    public static string CloudDrivePath { get; private set; } = "";
    public static string FfsExePath { get; private set; } = "";
    public static string SavegameDirectoryPath { get; private set; } = "";

    public static void SaveBatchPaths(UniformGrid uniformGrid)
    {
        if (!Directory.Exists(_folderPath))
        {
            Directory.CreateDirectory(_folderPath);
        }

        _batchPaths.Clear();
        foreach (PathGridItem pathGridItem in uniformGrid.Children) {
            _batchPaths.Add(pathGridItem.PathText);
        }
    }

    public static void SavePathsToFile()
    {
        var pathsDto = new PathsDto(_batchPaths, GameExePath, CloudDrivePath, FfsExePath, SavegameDirectoryPath);
        var json = JsonConvert.SerializeObject(pathsDto, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    public static void LoadPathsFromFile()
    {
        if (!File.Exists(_filePath)) return;
        var json = File.ReadAllText(_filePath);
        var pathsDto = JsonConvert.DeserializeObject<PathsDto>(json);
        ChangeEveryPath(pathsDto);
    }

    /// <summary>
    /// Changes every attribute of the PathsManager to the value of the matching attribute inside the PathsDto object.
    /// If the attribute of the PathsDto is null, the PathsManager attribute is set to a default value.
    /// </summary>
    /// <param name="pathsDto">The given Data Transfer Object of the PathsManager which matches in the attributes</param>
    private static void ChangeEveryPath(PathsDto pathsDto)
    {
        _batchPaths = pathsDto.BatchPaths ?? new List<string>();
        GameExePath = pathsDto.GameExePath ?? "";
        CloudDrivePath = pathsDto.CloudDrivePath ?? "";
        FfsExePath = pathsDto.FfsExePath ?? "";
        SavegameDirectoryPath = pathsDto.SavegameDirectoryPath ?? "";
    }

    public static void SaveOverlayPath(string filePath, TextBox element)
    {
        var xName = element.Name;
        switch (xName)
        {
            case "GameExePath":
                GameExePath = filePath;
                break;
            case "CloudDrivePath":
                CloudDrivePath = filePath;
                break;
            case "FfsExePath":
                FfsExePath = filePath;
                break;
            case "SavegameDirPath":
                SavegameDirectoryPath = filePath;
                break;
            default:
                MessageBox.Show(
                    "An error occurred - Failed to save path:\n" + filePath,
                    "Failed to save path.",
                    MessageBoxButton.OK);
                break;
        }
    }

    public static List<string> BatchPaths
    {
        get => _batchPaths;
        set => _batchPaths = value ?? throw new ArgumentNullException(nameof(value));
    }



}