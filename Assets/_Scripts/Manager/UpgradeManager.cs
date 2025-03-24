using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    private int levelDamage = 1;
    private int levelUndo = 0;
    private int levelWizard = 0;
    private int levelShuffle = 0;
    private int costDamage = 10;
    private int costUndo = 10;
    private int costWizard = 10;
    private int costShuffle = 10;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        UpdateCost();
    }
    private void Update()
    {
        UpdateUpgrade();
    }
    public int GetLevel(UpgradeType upgradeType)
    {
        UpdateCost();
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                return levelDamage;
            case UpgradeType.Undo:
                return levelUndo;
            case UpgradeType.Wizard:
                return levelWizard;
            case UpgradeType.Shuffle:
                return levelShuffle;
        }
        return 0;
    }
    public void SetLevel(UpgradeType upgradeType,int value)
    {
        UpdateCost();
        switch(upgradeType)
        {
            case UpgradeType.Damage:
                levelDamage = value;
                break;
            case UpgradeType.Undo:
                levelUndo = value;
                break;
            case UpgradeType.Wizard:
                levelWizard = value;
                break;
            case UpgradeType.Shuffle:
                levelShuffle = value;
                break;
        }
    }
    public int GetCost(UpgradeType upgradeType)
    {
        UpdateCost();
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                return costDamage;
            case UpgradeType.Undo:
                return costUndo;
            case UpgradeType.Wizard:
                return costWizard;
            case UpgradeType.Shuffle:
                return costShuffle;
        }
        return 0;
    }
    public string GetDescription(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                return "Lvl "+levelDamage+"\n"+ levelDamage+" -> "+(int)(levelDamage + 1);
            case UpgradeType.Undo:
                return "Lvl " + levelUndo + "\n" + levelUndo + " -> " + (int)(levelUndo + 1);
            case UpgradeType.Wizard:
                return "Lvl " + levelWizard + "\n" + levelWizard + " -> " + (int)(levelWizard + 1);
            case UpgradeType.Shuffle:
                return "Lvl " + levelShuffle + "\n" + levelShuffle + " -> " + (int)(levelShuffle + 1);
        }
        return "";
    }
    public void Upgrade(UpgradeType upgradeType)
    {
        UpdateCost();
        if (CanUpgrade(upgradeType))
        {
            GameManager.Instance.SetCoin(GameManager.Instance.GetCoin() - GetCost(upgradeType));
            switch (upgradeType)
            {
                case UpgradeType.Damage:
                    levelDamage++;
                    break;
                case UpgradeType.Undo:
                    levelUndo++;
                    break;
                case UpgradeType.Wizard:
                    levelWizard++;
                    break;
                case UpgradeType.Shuffle:
                    levelShuffle++;
                    break;
            }
        }
    }
    private void UpdateUpgrade()
    {
        GameManager.Instance.SetMulti(levelDamage);
        GameManager.Instance.SetUndoCount(levelUndo);
        GameManager.Instance.SetWizardCount(levelWizard);
        GameManager.Instance.SetShuffleCount(levelShuffle);
    }
    bool CanUpgrade(UpgradeType upgradeType)
    {
        UpdateCost();
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                return GameManager.Instance.GetCoin() >= costDamage;
            case UpgradeType.Undo:
                return GameManager.Instance.GetCoin() >= costUndo;
            case UpgradeType.Wizard:
                return GameManager.Instance.GetCoin() >= costWizard;
            case UpgradeType.Shuffle:
                return GameManager.Instance.GetCoin() >= costShuffle;
        }
        return false;
    }
    private void UpdateCost()
    {
        costDamage = 10 * (int)Mathf.Pow(1.8f, levelDamage);
        costUndo = (int)(10f * Mathf.Pow(1.5f,levelUndo));
        costWizard = (int)(10f * Mathf.Pow(1.7f, levelWizard));
        costShuffle = 10 * (int)Mathf.Pow(1.6f, levelShuffle);
    }
    public enum UpgradeType
    {
        Damage,
        Undo,
        Wizard,
        Shuffle
    }
}
