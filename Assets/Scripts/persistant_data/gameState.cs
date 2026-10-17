using System;
using System.IO;
using UnityEngine;

[Serializable]
public class GameSave
{
    public int infectedBlocks;
    public float spores;
    public int sporesPerSecond;
    public int infectionPerClick;
}

public static class SaveSystem
{
    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, "save.json");

    public static void Save(GameSave save)
    {
        string json = JsonUtility.ToJson(save, true);
        File.WriteAllText(SavePath, json);
    }

    public static GameSave Load()
    {
        if (!File.Exists(SavePath))
            return null;

        try
        {
            return JsonUtility.FromJson<GameSave>(
                File.ReadAllText(SavePath)
            );
        }
        catch (Exception exception)
        {
            Debug.LogError($"Could not load save: {exception.Message}");
            return null;
        }
    }

    // Get a list of saves
    public static GameSave[] GetSaves()
    {
        var files = Directory.GetFiles(Application.persistentDataPath, "save.json");
        return Array.ConvertAll(files, file => {
            var json = File.ReadAllText(file);
            return JsonUtility.FromJson<GameSave>(json);
        });
    }

    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
            File.Delete(SavePath);
    }
}
