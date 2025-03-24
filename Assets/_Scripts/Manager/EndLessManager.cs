using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLessManager : MonoBehaviour
{
    public static EndLessManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        SaveSystem.Instance.TakeData();
        UpgradeManager.Instance.SetLevel(UpgradeManager.UpgradeType.Damage, SaveSystem.Instance.data.levelDamage);
        UpgradeManager.Instance.SetLevel(UpgradeManager.UpgradeType.Undo, SaveSystem.Instance.data.levelUndo);
        UpgradeManager.Instance.SetLevel(UpgradeManager.UpgradeType.Wizard, SaveSystem.Instance.data.levelWizard);
        UpgradeManager.Instance.SetLevel(UpgradeManager.UpgradeType.Shuffle, SaveSystem.Instance.data.levelShuffle);
        GameManager.Instance.SetCoin(SaveSystem.Instance.data.coin);
        GameManager.Instance.SetAttack(SaveSystem.Instance.data.isAttack);
        GameManager.Instance.SetMap(SaveSystem.Instance.data.map);
    }

    void Update()
    {
        
    }
}
