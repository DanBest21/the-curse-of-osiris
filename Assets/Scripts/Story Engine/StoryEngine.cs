using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class StoryEngine
{
    private static string path = Application.persistentDataPath + "/game.save";

    public static void SaveGame(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = new FileStream(path, FileMode.Create);

        GameState state = new GameState(gameManager);

        formatter.Serialize(stream, state);
        stream.Close();
    }

    public static GameState LoadGame()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            GameState state = formatter.Deserialize(stream) as GameState;

            stream.Close();

            return state;
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
            return null;
        }
    }

    public static void DeleteSave()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static string SavePath()
    {
        return path;
    }
}