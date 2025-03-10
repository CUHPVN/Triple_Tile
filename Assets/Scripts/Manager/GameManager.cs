using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int score = 0;
    [SerializeField] private int multi = 1;
    [SerializeField] private int undoCount = 0;
    [SerializeField] private int wizardCount = 0;
    [SerializeField] private int shuffleCount = 0;
    [SerializeField] private int currentLevel = 0;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public int GetScore()
    {
        return score;
    }
    public void SetScore(int newScore)
    {
        score = newScore;
    }
    public void AddScore(int amount)
    {
        score += amount * multi;
    }
    public int GetCurLvl()
    {
        return currentLevel;
    }
    public void SetCurLvl(int newLvl)
    {
       currentLevel = newLvl;
    }
    public int GetMulti()
    {
        return multi;
    }
    public void SetMulti(int newMulti)
    {
        multi = newMulti;
    }
    public int GetUndoCount()
    {
        return undoCount;
    }
    public void SetUndoCount(int newUndoCount)
    {
        undoCount = newUndoCount;
    }
    public void AddUndoCount(int amount)
    {
        undoCount += amount;
    }
    public int GetWizardCount()
    {
        return wizardCount;
    }
    public void SetWizardCount(int newWizardCount)
    {
        wizardCount = newWizardCount;
    }
    public void AddWizardCount(int amount)
    {
        wizardCount += amount;
    }
    public int GetShuffleCount()
    {
        return shuffleCount;
    }
    public void SetShuffleCount(int newShuffleCount)
    {
        shuffleCount = newShuffleCount;
    }
    public void AddShuffleCount(int amount)
    {
        shuffleCount += amount;
    }
}