using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Transform attackButton;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        attackButton.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
    public void TurnOnAttackButton()
    {
        attackButton.gameObject.SetActive(true);
        //attackButton.GetComponent<PopOnEnable>().Pop();
    }
    public void LoadTripleTile()
    {
        if(inputField.text != "")
        {
            GameManager.Instance.SetCurLvl(int.Parse(inputField.text));
        }
        else
        {
            GameManager.Instance.SetCurLvl(0);
        }
        SceneManager.LoadScene("TripleTile");
    }
    public void Attack(int lv)
    {
        GameManager.Instance.SetCurLvl(lv);
        SceneManager.LoadScene("TripleTile");
    }
}
