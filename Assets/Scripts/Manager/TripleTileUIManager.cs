using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TripleTileUIManager : MonoBehaviour
{
    public static TripleTileUIManager Instance;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text shuffleText;
    [SerializeField] private TMP_Text wizardText;
    [SerializeField] private Transform winPanel;
    Vector3 originalPosition;
    Vector3 originalScale;
    public float shakeDuration = 0.2f; 
    public float scaleIncrease = 1.2f; 
    void Awake()
    {
        Instance = this;
        originalPosition = scoreText.rectTransform.localPosition;
        originalScale = scoreText.rectTransform.localScale;
    }
    void Update()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        scoreText.text = TripleManager.Instance.GetScore().ToString();
        shuffleText.text = TripleManager.Instance.GetShuffle().ToString();
        wizardText.text = TripleManager.Instance.GetWizard().ToString();
    }
    public void Shuffle()
    {
        if (TripleManager.Instance.GetShuffle() <= 0) return;
        TripleManager.Instance.DecShuffle();
        GameObject.FindFirstObjectByType<LevelManager>().ShuffleOne();
    }
    public void Wizard()
    {
        if (TripleManager.Instance.GetWizard() <= 0) return;
        TripleManager.Instance.DecWizard();
        int id = HandManager.Instance.CheckOne();
        if (id != 0)
        {
            int count = HandManager.Instance.CheckCount(id);
            for (int i = 0; i < 3 - count; i++)
            {
                GameObject.FindFirstObjectByType<LevelManager>().FindAndTake(id);
            }
        }
        else
        {
            int nid = GameObject.FindFirstObjectByType<LevelManager>().CheckOne();
            for (int i = 0; i < 3; i++)
            {
                GameObject.FindFirstObjectByType<LevelManager>().FindAndTake(nid);
            }
        }
    }
    public void OpenWinPanel()
    {
        winPanel.gameObject.SetActive(true);
    }
    public void Pause()
    {
        TripleManager.Instance.Pause();
    }
    public void Continue()
    {
        TripleManager.Instance.Continue();
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("TripleTile");
    }
    public void Attack()
    {
        SceneManager.LoadScene("Game");
    }

    public void BounceAndShakeText()
    {
        
            scoreText.rectTransform.DOScale(originalScale * scaleIncrease, shakeDuration).SetEase(Ease.OutBack).OnComplete(() =>
            {
                {
                    scoreText.rectTransform.DOScale(originalScale, shakeDuration);
                    scoreText.rectTransform.DOLocalMove(originalPosition, shakeDuration);
                }
            });
    }
}
