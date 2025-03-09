using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandNodeTile : MonoBehaviour
{
    [SerializeField] private NodeTileBase tileBase;
    private Image image;
    [SerializeField] private int ID=0;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.sprite = tileBase.sprites[ID];
    }
    public void SetID(int ID)
    {
        this.ID = ID;
        if (ID != 0)
        {
            transform.DOScale(new Vector3(0.25f,0.25f,1f), 0f);
            transform.DOScale(new Vector3(0.9f,0.9f,1f), 0.5f);
        }
    }
    public void SetIDOne(int ID)
    {
        this.ID = ID;
    }
    public void Remove(int ID)
    {
        this.ID = ID;
        {
            transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.1f).OnComplete(() => {
                transform.DOScale(new Vector3(.25f, .25f, 1), 0.1f).OnComplete(() =>
                {
                    transform.DOScale(new Vector3(0.9f, 0.9f, 1f), 0.3f);
                });
            });
        }
        Transform t = SpawnManager.Instance.Spawn("Score",transform.position.x, transform.position.y, Quaternion.identity);
        t.GetComponent<TMP_Text>().text = "+"+TripleManager.Instance.GetMulti();
    }
    public int GetID()
    {
        return ID;
    }
}
