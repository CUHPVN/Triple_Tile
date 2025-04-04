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
                return costList[(int)shopType];
        }
        return 0;
    }
    public void Buy(TileSprite shopType)
    {
        if (CanBuy(shopType))
        {
            GameManager.Instance.SetCoin(GameManager.Instance.GetCoin() - GetCost(shopType));
            switch (shopType)
            {
                case TileSprite.Weapon:
                    
                    GameManager.Instance.Save();
                    break;
            }
        }
    }
    bool CanBuy(TileSprite shopType)
    {
        switch (shopType)
        {
        }
        return false;
    }
}
