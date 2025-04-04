using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;
    [SerializeField] private TMP_Text coinCounts;
    [SerializeField] private TMP_Text healthCounts;
    [SerializeField] private TMP_Text timeCounts;
    [SerializeField] private Transform attackButton;
    [SerializeField] private Transform upgradeParent;
    [SerializeField] private List<Transform> upgradeItem = new();
    [SerializeField] private List<TMP_Text> upgradeDescriptions = new();
    [SerializeField] private List<UnityEngine.UI.Button> upgradeButtons = new();
    [SerializeField] private List<TMP_Text> upgradeCosts = new();
    [SerializeField] private Transform shopParent;
    [SerializeField] private List<Transform> shopItem = new();
    [SerializeField] private List<UnityEngine.UI.Button> shopButtons = new();
    [SerializeField] private List<TMP_Text> shopCosts = new();
    [SerializeField] private Transform Tut;
    [SerializeField] private UnityEngine.UI.Button deleteButton;
    [SerializeField] private UnityEngine.UI.Button saveButton;
    [SerializeField] private UnityEngine.UI.Button exitButton;
    [SerializeField] private UnityEngine.UI.Button openwebButton;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        LoadItem();
        LoadDescription();
        LoadButton();
        LoadCost();
        AddButtonEvent();
    }
    private void Reset()
    {
        LoadItem();
        LoadButton();
    }
    void Start()
    {
        attackButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (upgradeDescriptions.Count > 0)
        {
            UpdateDescription();
        }
        if (upgradeCosts.Count > 0)
        {
            UpdateCost();
        }
        coinCounts.text = ((int)GameManager.Instance.GetLatestCoin()).ToString();
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        healthCounts.text = HealthManager.Instance.health.ToString();
        timeCounts.text = HealthManager.Instance.time.ToString();
    }
    public void TurnOnTut()
    {
        Tut.gameObject.SetActive(true);
    }
    public void TurnOffTut()
    {
        Tut.gameObject.SetActive(false);
    }
    private void LoadItem()
    {
        foreach(Transform item in upgradeParent)
        {
            upgradeItem.Add(item);
        }
        foreach (Transform item in shopParent)
        {
            shopItem.Add(item);
        }
    }
    private void LoadButton()
    {
        foreach(Transform button in upgradeItem)
        {
            upgradeButtons.Add(button.GetComponentInChildren<UnityEngine.UI.Button>());
        }
        foreach (Transform button in shopItem)
        {
            shopButtons.Add(button.GetComponentInChildren<UnityEngine.UI.Button>());
        }
    }
    private void LoadDescription()
    {
        foreach (Transform des in upgradeItem)
        {
            upgradeDescriptions.Add(des.Find("Description").GetComponent<TMP_Text>());
        }
    }
    private void LoadCost()
    {
        foreach (UnityEngine.UI.Button button in upgradeButtons)
        {
            upgradeCosts.Add(button.GetComponentInChildren<TMP_Text>());
        }
        foreach (UnityEngine.UI.Button button in shopButtons)
        {
            shopCosts.Add(button.GetComponentInChildren<TMP_Text>());
        }
    }
    private void UpdateCost()
    {
        foreach (TMP_Text cost in upgradeCosts)
        {
            cost.text = ""+UpgradeManager.Instance.GetCost((UpgradeManager.UpgradeType) upgradeCosts.IndexOf(cost));
        }
        foreach (TMP_Text cost in shopCosts)
        {
            cost.text = "" + UpgradeManager.Instance.GetCost((UpgradeManager.UpgradeType)shopCosts.IndexOf(cost));
        }
    }
    private void UpdateDescription()
    {
        foreach (TMP_Text des in upgradeDescriptions)
        {
            des.text = UpgradeManager.Instance.GetDescription((UpgradeManager.UpgradeType)upgradeDescriptions.IndexOf(des));
        }
    }
    private void AddButtonEvent()
    {
        foreach (UnityEngine.UI.Button button in upgradeButtons)
        {
            int id = upgradeButtons.IndexOf(button);
            button.onClick.AddListener(() => OnBuyClick(id));
            button.onClick.AddListener(() => SoundManager.Instance.PlayButtonSound());
        }
        deleteButton.onClick.AddListener(() => SaveSystem.Instance.DeleteSave());
        deleteButton.onClick.AddListener(() => SoundManager.Instance.PlayButtonSound());
        saveButton.onClick.AddListener(() => GameManager.Instance.Save());
        saveButton.onClick.AddListener(() => SoundManager.Instance.PlayButtonSound());
        exitButton.onClick.AddListener(() => GameManager.Instance.Exit());
        exitButton.onClick.AddListener(() => SoundManager.Instance.PlayButtonSound());
        openwebButton.onClick.AddListener(() => GameManager.Instance.OpenWeb());
        openwebButton.onClick.AddListener(() => SoundManager.Instance.PlayButtonSound());

    }
    private void OnBuyClick(int id)
    {
        UpgradeManager.Instance.Upgrade((UpgradeManager.UpgradeType)id);
    }
    public void TurnOnAttackButton()
    {
        attackButton.gameObject.SetActive(true);
    }
    public void TurnOffAttackButton()
    {
        attackButton.gameObject.SetActive(false);
    }
    public void Attack(int lv)
    {
        if(HealthManager.Instance.health<=0) return;
        GameManager.Instance.SetCurLvl(lv);
        GameManager.Instance.SetAttack(true);
        GameManager.Instance.SetWin(false);
        GameManager.Instance.SetTileSprite(TileSprite.Weapon);
        if (GameManager.Instance.GetTut())
        {
            GameManager.Instance.SetTut(false);
            GameManager.Instance.Save();
            Invoke(nameof(OpenTut), 0.25f);
        }
        else
        {
            GameManager.Instance.Save();
            Invoke(nameof(OpenTile), 0.25f);
        }
    }
    public void OpenTut()
    {
        GameManager.Instance.isTripple = true;

        SceneManager.LoadScene("TripleTileTut");
    }
    public void OpenTile()
    {
        GameManager.Instance.isTripple = true;

        SceneManager.LoadScene("TripleTile");
    }
    public void PlayButtonSound()
    {
        SoundManager.Instance.PlayButtonSound();
    }
}
