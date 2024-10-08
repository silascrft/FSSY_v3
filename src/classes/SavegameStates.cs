﻿namespace FSSY_v3.classes;

using System.IO;
using Newtonsoft.Json;

public class SavegameStates
{
    private readonly string _folderPath;
    private readonly string _filePath;

    public SavegameStates()
    {
        _folderPath = MainWindow.FolderPath;
        _filePath = Path.Combine(_folderPath, "checkboxStates.json");
        Directory.CreateDirectory(_folderPath);
    }

    public void Save(CheckBoxState state)
    {
        var json = JsonConvert.SerializeObject(state);
        File.WriteAllText(_filePath, json);
    }

    public CheckBoxState Load()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<CheckBoxState>(json);
        }
        return new CheckBoxState();
    }
}