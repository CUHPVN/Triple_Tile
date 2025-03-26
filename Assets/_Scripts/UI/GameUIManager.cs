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
    [SerializeField] private Transform attackButton;
    [SerializeField] private Transform upgradeParent;
    [SerializeField] private List<Transform> upgradeItem = new();
    [SerializeField] private List<TMP_Text> upgradeDescriptions = new();
    [SerializeField] private List<UnityEngine.UI.Button> upgradeButtons = new();
    [SerializeField] private List<TMP_Text> upgradeCosts = new();
    [SerializeField] private UnityEngine.UI.Button deleteButton;
    [SerializeField] private UnityEngine.UI.Button saveButton;


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
    }
    private void LoadItem()
    {
        foreach(Transform item in upgradeParent)
        {
            upgradeItem.Add(item);
        }
    }
    private void LoadButton()
    {
        foreach(Transform button in upgradeItem)
        {
            upgradeButtons.Add(button.GetComponentInChildren<UnityEngine.UI.Button>());
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
    }
    private void UpdateCost()
    {
        foreach (TMP_Text cost in upgradeCosts)
        {
            cost.text = ""+UpgradeManager.Instance.GetCost((UpgradeManager.UpgradeType) upgradeCosts.IndexOf(cost));
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
        }
        deleteButton.onClick.AddListener(() => SaveSystem.Instance.DeleteSave());
        saveButton.onClick.AddListener(() => GameManager.Instance.Save());

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
        GameManager.Instance.SetCurLvl(lv);
        GameManager.Instance.SetAttack(true);
        GameManager.Instance.Save();
        SceneManager.LoadScene("TripleTile");
    }
}
