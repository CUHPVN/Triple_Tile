using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class TripleManager : MonoBehaviour
{
    public static TripleManager Instance;
    [SerializeField] private PopOnEnable pop;
    [SerializeField] private int score=0;
    [SerializeField] private int multi=1;
    [SerializeField] private int wizardCount = 0;
    [SerializeField] private int shuffleCount = 0;
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private List<GameObject> levels;
    [SerializeField] private Transform levelParent;
    [SerializeField] private bool isWin = false,isLose=false;
    bool isPause = false;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        LoadData();
        Instantiate(levels[currentLevel], Vector3.zero, Quaternion.identity, levelParent);
        Debug.Log("Level " + currentLevel);
    }
    private void PauseUpdate()
    {
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    private void LoadData()
    {
        score = 0;
        multi = GameManager.Instance.GetMulti();
        wizardCount = GameManager.Instance.GetWizardCount();
        shuffleCount = GameManager.Instance.GetShuffleCount();
        currentLevel = GameManager.Instance.GetCurLvl();
    }
    public void Pause()
    {
        isPause = true;
    }
    public void Continue()
    {
        isPause = false;
        pop.Disable();
    }
    public bool GetWin()
    {
        return isWin;
    }
    public void SetWin(bool val)
    {
        isWin = val;
    }
    public bool GetLose()
    {
        return isLose;
    }
    public void SetLose(bool val)
    {
        isLose = val;
    }
    public bool GetPause()
    {
        return isPause;
    }
    public int GetScore()
    {
        return score;
    }
    public int GetMulti()
    {
        return multi;
    }
    public int GetWizard()
    {
        return wizardCount;
    }
    public int GetShuffle()
    {
        return shuffleCount;
    }
    public void DecWizard()
    {
        wizardCount--;
    }
    public void DecShuffle()
    {
        shuffleCount--;
    }
    public void AddScore(int val)
    {
        score += val*multi;
        TripleTileUIManager.Instance.BounceAndShakeText();
    }
    
}
