using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    [System.Serializable]
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
    public bool isTut = true;
    public int health = 5;
    public int time = 0;
    public System.DateTime currentTime;
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
        filePath = Application.persistentDataPath + "/savefile.dat";
    }

    public void SaveGame(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }
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
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                SaveData data = (SaveData)formatter.Deserialize(stream);
                Debug.Log("Game loaded!");
                return data;
            }
        }
        else
        {
            Debug.LogWarning("Save file not found!");
            return new SaveData();
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            data = new SaveData();
            Debug.Log("Save file deleted!");
            SceneManager.LoadScene("Game");
        }
        else
        {
            Debug.LogWarning("Save file not found to delete!");
        }
    }
}
