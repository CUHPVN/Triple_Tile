using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float hp;
    [SerializeField] protected float maxHp;
    protected float coin;
    [SerializeField] protected Transform player;
    [SerializeField] protected Transform hpBar;
    [SerializeField] protected Slider hpSlider;
    [SerializeField] protected TMP_Text hpText;


    private void Awake()
    {
        LoadComponent();
    }
    protected virtual void LoadComponent()
    {
        hp = 100;
        maxHp = 100;
        coin = 10;
        player = GameObject.FindWithTag("Player").transform;
    }
    public void SetHP(float hp)
    {
        this.hp = hp;
    }
    public void SetMaxHP(float maxHp)
    {
        this.maxHp = maxHp;
    }
    public void SetCoin(float coin)
    {
        this.coin = coin;
    }
    protected virtual void CheckStop()
    { 
        if (Vector2.Distance(player.position, transform.position)<=2&&MapManager.Instance.GetMove()){
            MapManager.Instance.Stop();
            GameManager.Instance.enemy = this;
            GameUIManager.Instance.TurnOnAttackButton();
        }
    }
    public virtual void AddMap()
    {
        SaveData.Map map;
        map.name = transform.parent.name;
        map.hp = hp;
        map.maxHp = maxHp;
        map.coin = coin;
        GameManager.Instance.AddMap(map);
    }
    protected virtual void UpdateHP()
    {
        if(hpSlider != null)
        {
            hpSlider.value = (float)hp / maxHp;
            hpText.text = (int)hp + "/" + (int)maxHp;
        }
        else
        {
            hpBar = transform.Find("HP");
            hpSlider = hpBar.GetComponentInChildren<Slider>();
            hpText = hpBar.GetComponentInChildren<TMP_Text>();
        }
    }
}
