using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int playerLevel;
    public float playerHealth;
}

public class SaveSystem : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/savefile.json";
    }

    public void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
        Debug.Log("Game saved to " + filePath);
    }

    public SaveData LoadGame()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded!");
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found!");
            return null;
        }
    }
}
