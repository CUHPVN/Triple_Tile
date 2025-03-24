using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private int coin = 0;
    [SerializeField] private int score = 0;
    [SerializeField] private int multi = 1;
    [SerializeField] private int undoCount = 0;
    [SerializeField] private int wizardCount = 0;
    [SerializeField] private int shuffleCount = 0;
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private SaveData.Map map;
    [SerializeField] public Enemy enemy;
    [SerializeField] bool isAttack = false;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public int GetScore()
    {
        return score;
    }
    public void SetScore(int newScore)
    {
        score = newScore;
    }
    public int GetCoin()
    {
        return coin;
    }
    public void SetCoin(int newCoin)
    {
        coin = newCoin;
    }
    public void AddCoin(int amount)
    {
        coin += amount;
    }
    public bool GetAttack()
    {
        return isAttack;
    }
    public void SetAttack(bool newIsAttack)
    {
        isAttack = newIsAttack;
    }
    public int GetCurLvl()
    {
        return currentLevel;
    }
    public void SetCurLvl(int newLvl)
    {
       currentLevel = newLvl;
    }
    public int GetMulti()
    {
        return multi;
    }
    public void SetMulti(int newMulti)
    {
        multi = newMulti;
    }
    public int GetUndoCount()
    {
        return undoCount;
    }
    public void SetUndoCount(int newUndoCount)
    {
        undoCount = newUndoCount;
    }
    public void AddUndoCount(int amount)
    {
        undoCount += amount;
    }
    public int GetWizardCount()
    {
        return wizardCount;
    }
    public void SetWizardCount(int newWizardCount)
    {
        wizardCount = newWizardCount;
    }
    public void AddWizardCount(int amount)
    {
        wizardCount += amount;
    }
    public int GetShuffleCount()
    {
        return shuffleCount;
    }
    public void SetShuffleCount(int newShuffleCount)
    {
        shuffleCount = newShuffleCount;
    }
    public void AddShuffleCount(int amount)
    {
        shuffleCount += amount;
    }
    public void AddMap(SaveData.Map map)
    {
        this.map = map;
    }
    public SaveData.Map GetMap()
    {
        return map;
    }
    public bool CheckMap()
    {
        return map.name!=null;
    }
    public void SetMap(SaveData.Map newMap)
    {
        map = newMap;
    }
    public void Save()
    {
        SaveData data = new SaveData();
        data.levelDamage = UpgradeManager.Instance.GetLevel(UpgradeManager.UpgradeType.Damage);
        data.levelUndo = UpgradeManager.Instance.GetLevel(UpgradeManager.UpgradeType.Undo);
        data.levelWizard = UpgradeManager.Instance.GetLevel(UpgradeManager.UpgradeType.Wizard);
        data.levelShuffle = UpgradeManager.Instance.GetLevel(UpgradeManager.UpgradeType.Shuffle);
        data.coin = coin;
        data.isAttack = isAttack;
        enemy.AddMap();
        data.map = map;
        SaveSystem.Instance.SaveGame(data);
    }
}