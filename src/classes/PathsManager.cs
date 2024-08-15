namespace FSSY_v3.classes;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls.Primitives;


public class PathsDto
{
    private List<string> _batchPaths;
    private string _gameExePath = "";
    private string _cloudDrivePath = "";
    private string _ffsExePath = "";
    private string _savegameDirectoryPath = "";

    public PathsDto( List<string> batchPaths, string gameExePath, string cloudDrivePath, string ffsExePath, string savegameDirectoryPath)
    {
        _batchPaths = batchPaths;
        _gameExePath = gameExePath;
        _cloudDrivePath = cloudDrivePath;
        _ffsExePath = ffsExePath;
        _savegameDirectoryPath = savegameDirectoryPath;
    }

    public List<string> BatchPaths
    {
        get => _batchPaths;
        set => _batchPaths = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string GameExePath
    {
        get => _gameExePath;
        set => _gameExePath = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string CloudDrivePath
    {
        get => _cloudDrivePath;
        set => _cloudDrivePath = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string FfsExePath
    {
        get => _ffsExePath;
        set => _ffsExePath = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string SavegameDirectoryPath
    {
        get => _savegameDirectoryPath;
        set => _savegameDirectoryPath = value ?? throw new ArgumentNullException(nameof(value));
    }
}

public static class PathsManager
{
    private static List<string> _batchPaths = [];
    private static string _gameExePath = "";
    private static string _cloudDrivePath = "";
    private static string _ffsExePath = "";
    private static string _savegameDirectoryPath = "";
    private static readonly string _folderPath = MainWindow.FolderPath;
    private static readonly string _filePath  = Path.Combine(_folderPath, "paths.json");

    public static void SaveBatchPathsToFile(UniformGrid uniformGrid)
    {
        if (!Directory.Exists(_folderPath))
        {
            Directory.CreateDirectory(_folderPath);
        }

        _batchPaths.Clear();
        foreach (PathGridItem pathGridItem in uniformGrid.Children) {
            _batchPaths.Add(pathGridItem.PathText);
        }

        var pathsDto = new PathsDto(_batchPaths, _gameExePath, _cloudDrivePath, _ffsExePath, _savegameDirectoryPath);
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

    private static void ChangeEveryPath(PathsDto pathsDto)
    {
        _batchPaths = pathsDto.BatchPaths;
        _gameExePath = pathsDto.GameExePath;
        _cloudDrivePath = pathsDto.CloudDrivePath;
        _ffsExePath = pathsDto.FfsExePath;
        _savegameDirectoryPath = pathsDto.SavegameDirectoryPath;
    }

    public static List<string> BatchPaths
    {
        get => _batchPaths;
        set => _batchPaths = value ?? throw new ArgumentNullException(nameof(value));
    }
}