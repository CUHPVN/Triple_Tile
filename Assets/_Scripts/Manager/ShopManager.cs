using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    public List<int> costList = new List<int>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    
        LoadComponent();
    }
    private void LoadComponent()
    {
       
    }
    private void Update()
    {
        
    }
    public int GetCost(TileSprite shopType)
    {
        switch (shopType)
        {
            case TileSprite.Weapon:
                GameManager.Instance.SetIsUnlockID((int)shopType);
                return costList[(int)shopType];
            case TileSprite.Equipment:
                return costList[(int)shopType];
            case TileSprite.Fruit:
                return costList[(int)shopType];
            default:
                Debug.LogError("Invalid shop type");
                break;
        }
        return 0;
    }
    public void Buy(TileSprite shopType)
    {
        if (CanBuy(shopType))
        {
            GameManager.Instance.SetCoin(GameManager.Instance.GetCoin() - GetCost(shopType));
            GameManager.Instance.SetIsUnlockID((int)shopType);
            GameManager.Instance.SetCurrentSprite(shopType);
            GameManager.Instance.Save();
        }
        else if(GameManager.Instance.GetIsUnlockID((int)shopType))
        {
            GameManager.Instance.SetCurrentSprite(shopType);
        }
    }
    bool CanBuy(TileSprite shopType)
    {
        if(GameManager.Instance.GetIsUnlockID((int)shopType)) return false;
        switch (shopType)
        {
            case TileSprite.Weapon:
                if (GameManager.Instance.GetCoin() >= GetCost(shopType))
                {
                    return true;
                }
                break;
            case TileSprite.Equipment:
                if (GameManager.Instance.GetCoin() >= GetCost(shopType))
                {
                    return true;
                }
                break;
            case TileSprite.Fruit:
                if (GameManager.Instance.GetCoin() >= GetCost(shopType))
                {
                    return true;
                }
                break;
        }
        return false;
    }
}
