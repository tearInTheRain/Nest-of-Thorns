using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public int score;
    public float health;
    public string name;
}
public class GameSaveSystem : MonoBehaviour
{
    private const string SaveKey = "GameSaveData";

    public void SaveGame(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, jsonData);
        PlayerPrefs.Save();
        Debug.Log("Game saved successfully");
    }

    public GameData LoadGame()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string jsonData = PlayerPrefs.GetString(SaveKey);
            GameData loadedData = JsonUtility.FromJson<GameData>(jsonData);
            Debug.Log("Game loaded successfully");
            return loadedData;
        }
        else
        {
            Debug.LogWarning("No save data found");
            return null;
        }
    }

    public void DeleteSaveData()
    {
        PlayerPrefs.DeleteKey(SaveKey);
        Debug.Log("Save data deleted");
    }
}
