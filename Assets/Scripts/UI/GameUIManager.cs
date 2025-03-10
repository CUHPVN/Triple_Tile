using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
