using DG.Tweening.Core.Easing;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public struct Map
    {
        public string name;
        public float hp;
        public float maxHp;
        public float coin;
    }
    public int levelDamage = 1;
    public int levelUndo = 0;
    public int levelWizard = 0;
    public int levelShuffle = 0;
    public int coin = 0;
    public bool isAttack = false;
    public Map map;
}

public class SaveSystem : MonoBehaviour
{
    private string filePath;
    public SaveData data;
    public static SaveSystem Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        filePath = Application.persistentDataPath + "/savefile.json";
    }
    public void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
        Debug.Log("Game saved to " + filePath);
    }
    public void TakeData()
    {
        data = LoadGame();
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
            GameManager.Instance.Save();
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded!");
            return data;
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Save file deleted!");
        }
        else
        {
            Debug.LogWarning("Save file not found to delete!");
        }

    }
}
