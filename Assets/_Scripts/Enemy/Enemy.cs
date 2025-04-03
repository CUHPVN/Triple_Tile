using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float hp;
    [SerializeField] protected float latestHp;
    [SerializeField] protected float maxHp;
    protected float coin;
    [SerializeField] protected Transform player;
    [SerializeField] protected Transform hpBar;
    [SerializeField] protected Slider hpSlider;
    [SerializeField] protected TMP_Text hpText;


    private void OnEnable()
    {
        LoadComponent();
    }
    private void Start()
    {
    }
    protected virtual void LoadComponent()
    {
        int value = UpgradeManager.Instance.GetLevel(UpgradeManager.UpgradeType.Damage);
        int value2 = UpgradeManager.Instance.GetLevel(UpgradeManager.UpgradeType.Undo)+ UpgradeManager.Instance.GetLevel(UpgradeManager.UpgradeType.Wizard)+ UpgradeManager.Instance.GetLevel(UpgradeManager.UpgradeType.Shuffle);
        hp = (value)*50+value2*10;
        latestHp = hp;
        maxHp = (value) * 50 + value2 * 10;
        coin = hp*0.1f+value+value2;
        player = GameObject.FindWithTag("Player").transform;
    }
    public float GetHP()
    {
        return hp;
    }
    public float GetMaxHP()
    {
        return maxHp;
    }
    public float GetCoin()
    {
        return coin;
    }
    public void SetHP(float hp)
    {
        this.hp = hp;
        if (this.hp <= 0)
        {
            this.hp = 0;
            Invoke(nameof(Death), 1f);
        }
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
            if (GameManager.Instance.GetTut())
            {
                GameUIManager.Instance.TurnOnTut();
            }
            else
            {
                GameUIManager.Instance.TurnOffTut();
            }
            if (hp-GameManager.Instance.GetScore()>0)
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
    public void Death()
    {
        GameManager.Instance.AddCoin((int)coin);
        gameObject.SetActive(false);
        MapManager.Instance.Continues();
    }
    protected virtual void UpdateHP()
    {
        if(hpSlider != null)
        {
            if(latestHp != hp)
            {
                Anim();
            }
            hpText.text = (int)hp + "/" + (int)maxHp;
        }
        else
        {
            hpBar = transform.Find("HP");
            hpSlider = hpBar.GetComponentInChildren<Slider>();
            hpText = hpBar.GetComponentInChildren<TMP_Text>();
        }
    }
    protected virtual void Anim()
    {
        latestHp = Mathf.Lerp(latestHp, hp, 0.05f);
        hpSlider.value = (float)latestHp / maxHp;
    }
}
