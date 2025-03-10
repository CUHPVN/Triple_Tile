using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
public class NodeTile : MonoBehaviour
{
    [SerializeField] private NodeTileBase tileBase;
    private SpriteRenderer spriteRenderer;
    private int ID;
    private bool canClick = false;
    private bool isClick = false;
    public Vector3 posToMove=Vector3.zero;
    public Vector3 tilePos;
    public Vector3 localPos;

    private void Awake()
    {
    }
    private void LoadComponent()
    {
        canClick = true;
        isClick = false;
    }
    private void OnEnable()
    {
        localPos = transform.position;
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.sprite = tileBase.sprites[ID];
    }
    public void SetID(int ID)
    {
        this.ID = ID;
    }
    public int GetID()
    {
        return ID;
    }
    public void SetCanClick(bool value)
    {
        canClick = value;
    }
    public bool GetCanClick()
    {
        return canClick;
    }
    public bool GetIsClick()
    {
        return isClick;
    }
    public void Move()
    {
        isClick = true;
        tilePos = transform.position;
        transform.DOMove(posToMove, 0.25f).SetEase(Ease.InOutQuad);
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
    public void MoveUp()
    {
        gameObject.SetActive(true);
        LoadComponent();
        transform.position = posToMove;
        transform.DOMove(tilePos, 0.25f);
        transform.DOScale(Vector3.one, 0.5f);
    }
    public void DebugTest()
    {
        Debug.Log(this.ID);
    }
}
