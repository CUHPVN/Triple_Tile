using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScoreOnEnable : MonoBehaviour
{
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = TripleManager.Instance.GetScore().ToString();
        }
    }
}
