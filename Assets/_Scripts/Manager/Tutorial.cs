using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance { get; private set; }
    [SerializeField] private Transform tut1;
    [SerializeField] private Transform tut2;
    [SerializeField] private Transform tut3;
    [SerializeField] private Transform tut4;
    public bool tut1Done = false;
    public bool tut2Done = false;
    public bool tut3Done = false;
    public bool tut4Done = false;
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (tut1Done == false)
        {
            tut1.gameObject.SetActive(true);
        }
        else if (tut1Done == true && tut2Done == false)
        {
            tut1.gameObject.SetActive(false);
            TripleManager.Instance.SetUndo(1);
            tut2.gameObject.SetActive(true);
        }
        else if (tut2Done == true && tut3Done == false)
        {
            tut2.gameObject.SetActive(false);
            TripleManager.Instance.SetWizard(1);
            tut3.gameObject.SetActive(true);
        }
        else if (tut3Done == true && tut4Done == false)
        {
            tut3.gameObject.SetActive(false);
            TripleManager.Instance.SetShuffle(1);
            tut4.gameObject.SetActive(true);
        }
        else if (tut4Done == true)
        {
            tut4.gameObject.SetActive(false);
        }
    }
}
