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
        //GameManager.Instance.SetAttack(SaveSystem.Instance.data.isAttack);
        GameManager.Instance.SetMap(SaveSystem.Instance.data.map);
        GameManager.Instance.SetTut(SaveSystem.Instance.data.isTut);
        GameManager.Instance.SetCurrentSprite(SaveSystem.Instance.data.currentSprite);
        GameManager.Instance.SetIsUnlock(SaveSystem.Instance.data.isUnlock);
        HealthManager.Instance.health = SaveSystem.Instance.data.health;
        HealthManager.Instance.elapsedTimes = SaveSystem.Instance.data.time;
        HealthManager.Instance.Calculate(SaveSystem.Instance.data.currentTime);
        if (!GameManager.Instance.GetWin()) HealthManager.Instance.DecHealth();
        Attack();
        GameManager.Instance.Save();
    }
    void Attack()
    {
        if (GameManager.Instance.GetAttack())
        {
            GameManager.Instance.AttackEnemy();
            if (GameManager.Instance.GetScore() > 0)
            {
                Player.Instance.Attack();
            }
            //GameManager.Instance.SetAttack(false);
        }
    }

    void Update()
    {
        
    }
}
