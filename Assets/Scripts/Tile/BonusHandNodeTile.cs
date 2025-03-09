using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class BonusHandNodeTile : MonoBehaviour
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
        if(HandManager.Instance.GetQueueCount() > 0)
        {
            SetID(HandManager.Instance.GetQueueFirst());
        }
        image.sprite = tileBase.sprites[ID];
    }
    public void SetID(int ID)
    {
        this.ID = ID;
        if (ID != 0)
        {
            transform.DOMove(transform.parent.position, 0f).OnComplete(() => image.enabled = true);
        }
    }
    public void SetIDOne(int ID)
    {
        this.ID = ID;
    }
    public void Remove(int ID,Vector3 pos)
    {
        this.ID = ID;
        {
            transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.1f).OnComplete(() => {
                transform.DOScale(new Vector3(.9f, .9f, 1), 0.1f).OnComplete(() =>
                {
                    transform.DOScale(Vector3.zero, 0.3f);
                    transform.DOMove(pos, 0.3f).OnComplete(()=> image.enabled = false);
                });
            });
        }
    }
    public int GetID()
    {
        return ID;
    }
}
