using System.IO;
using Newtonsoft.Json;

namespace FSSY_v3.classes;

public class Config(bool isSetupFinished)
{
    public bool IsSetupFinished { get; private set; } = isSetupFinished;
    private static readonly string FolderPath = PathsManager.FolderPath;
    private static readonly string FilePath = Path.Combine(FolderPath, "config.json");

    public void SetSetupFinished()
    {
        IsSetupFinished = true;
        WriteConfigToFile();
    }

    public static Config LoadConfig()
    {
        if (!File.Exists(FilePath))
        {
            return new Config(false);
        }

        ;
        var json = File.ReadAllText(FilePath);
        return JsonConvert.DeserializeObject<Config>(json) ?? new Config(false);
    }

    private void WriteConfigToFile()
    {
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }

        var json = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }
}