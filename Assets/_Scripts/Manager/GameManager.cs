using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private int coin = 0;
    [SerializeField] private float latestCoin = 0;
    [SerializeField] private int score = 0;
    [SerializeField] private int multi = 1;
    [SerializeField] private int undoCount = 0;
    [SerializeField] private int wizardCount = 0;
    [SerializeField] private int shuffleCount = 0;
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private SaveData.Map map;
    [SerializeField] public Enemy enemy;
    [SerializeField] private bool isAttack = false;
    [SerializeField] private bool isWin = true;
    [SerializeField] private bool[] isUnlock = new bool[Enum.GetNames(typeof(TileSprite)).Length];
    public bool isTripple = false;
    [SerializeField] private bool isTut = true;
    [SerializeField] NodeTileBase nodeTileBase;
    [SerializeField] private TileSprite tileSprite;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;
        DontDestroyOnLoad(this.gameObject);
        LoadComponent();
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        SoundManager.Instance.PlayMusic();
    }
    private void Update()
    {
        CoinAnim();
    }
    public void LoadComponent()
    {
        coin = 0;
        latestCoin = 0;
        score = 0;
        multi = 1;
        undoCount = 0;
        wizardCount = 0;
        shuffleCount = 0;
        currentLevel = 0;
        isAttack = false;
        isWin = true;
        isTut = true;
        map = new SaveData.Map();
        isUnlock = new bool[Enum.GetNames(typeof(TileSprite)).Length];
        enemy = null;
    }
    public void DeleteSave()
    {
        coin = 0;
        latestCoin = 0;
        score = 0;
        multi = 1;
        undoCount = 0;
        wizardCount = 0;
        shuffleCount = 0;
        currentLevel = 0;
        isAttack = false;
        isWin = true;
        isTut= true;
        map = new SaveData.Map();
        isUnlock = new bool[Enum.GetNames(typeof(TileSprite)).Length];
        enemy = null;
    }
    protected virtual void CoinAnim()
    {
        if (Mathf.Abs(latestCoin - coin) < 1) latestCoin = coin;
        else
        {
            latestCoin = Mathf.Lerp(latestCoin, coin, 0.1f);
        }
    }
    public void SetIsUnlock(bool[] bools)
    {
        isUnlock = bools;
    }
    public bool GetIsUnlockID(int value)
    {
        return isUnlock[value];
    }
    public void SetIsUnlockID(int value)
    {
        isUnlock[value]=true;
    }
    public void SetCurrentSprite(TileSprite sprite)
    {
        tileSprite = sprite;
    }
    public TileSprite GetCurrentSprite()
    {
        return tileSprite;
    }
    public void SetSprite()
    {
        nodeTileBase.SetTileSprite(tileSprite);
    }
    public float GetLatestCoin()
    {
        return latestCoin;
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
    public bool GetTut()
    {
        return isTut;
    }
    public void SetTut(bool tut)
    {
        isTut = tut;
    }
    public bool GetWin()
    {
        return isWin;
    }
    public void SetWin(bool win)
    {
        isWin = win;
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
    public void AttackEnemy()
    {
        StartCoroutine(Damage(score));
    }
    IEnumerator Damage(int damage)
    {
        yield return new WaitForSeconds(1);
        enemy.SetHP(enemy.GetHP() - (float)damage);
        score = 0;
    }
    public bool CheckMap()
    {
        return map.hp!=0;
    }
    public void SetMap(SaveData.Map newMap)
    {
        map = newMap;
        if(enemy!=null)
        enemy.SetHP(map.hp);
    }
    public void CheckDeath()
    {
        if(map.hp-score <= 0) GameUIManager.Instance.TurnOffAttackButton();
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
        data.isTut = isTut;
        data.currentTime = System.DateTime.Now;
        data.health = HealthManager.Instance.health;
        data.time = HealthManager.Instance.elapsedTimes;
        data.isUnlock = isUnlock;
        data.currentSprite = tileSprite;
        if (enemy != null)
        {
            enemy.AddMap();
            data.map = map;
        }
        else
        {
            data.map = SaveSystem.Instance.data.map;
        }
            SaveSystem.Instance.SaveGame(data);
    }
    private void OnApplicationQuit()
    {
        if (isTripple)
        {
            HealthManager.Instance.DecHealth();
        }
        Save();
    }
    public void OpenWeb()
    {
        Application.OpenURL("https://cuhp-vn.itch.io/");
    }
    public void Exit()
    {
        Application.Quit();
    }
}