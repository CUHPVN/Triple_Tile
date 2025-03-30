using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static SoundManager;
using static Unity.Burst.Intrinsics.X86.Avx;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<NodeTile> tiles = new List<NodeTile>();
    private int tileCount = 0;

    void Start()
    {
        LoadMap();
    }
    void LoadMap()
    {
        foreach (Transform t in transform)
        {
            foreach (Transform t2 in t)
            {
                t2.GetComponent<SpriteRenderer>().sortingOrder = t.GetComponent<TilemapRenderer>().sortingOrder;
                if(!tiles.Contains(t2.GetComponent<NodeTile>()))
                {
                    tiles.Add(t2.GetComponent<NodeTile>());
                    tileCount++;
                }
            }
        }
        SetTile();
        MoveUp();
        Shuffle(tiles);
        StartCoroutine(DropDown(0));
    }
    private void Reset()
    {
        LoadTile();
    }
    void LoadTile()
    {
        foreach (Transform t in transform)
        {
            foreach (Transform t2 in t)
            {
                t2.GetComponent<SpriteRenderer>().sortingOrder = t.GetComponent<TilemapRenderer>().sortingOrder;
                tiles.Add(t2.GetComponent<NodeTile>());
                tileCount++;
            }
        }
    }
    void Update()
    {
        CheckWin();
    }
    public static void Shuffle(List<NodeTile> list)
    {
        for(int i=0;i< list.Count; i++)
        {
            int id = list[i].GetID();
            int r = Random.Range(i, list.Count);
            list[i].SetID(list[r].GetID());
            list[r].SetID(id);
        }
    }
    public void ShuffleOne()
    {
        MoveUp();
        Shuffle(tiles);
        StartCoroutine(DropDown(0));
    }
    public void Remove(NodeTile tile)
    {
        tiles.Remove(tile);
    }
    public void Add(NodeTile tile)
    {
        tiles.Add(tile);
    }
    public void FindAndTake(int id)
    {
        foreach(NodeTile tile in tiles)
        {
            if (tile.GetID()==id && !tile.GetIsClick())
            {
                Remove(tile);
                tile.posToMove = HandManager.Instance.AddTilePosWOE(tile.GetID());
                tile.Move();
                break;
            }
        }
        
    }
    public void MoveUp()
    {
        foreach(var i in tiles)
        {
            i.transform.position = i.localPos;
            i.transform.localScale = new Vector3(0.0f,0.0f,1f);
        }
    }
    private IEnumerator DropDown(int i)
    {
        if(i < tiles.Count)
        {
            tiles[i].transform.DOMove(tiles[i].localPos, 0.15f).SetEase(Ease.InOutQuad);
            tiles[i].transform.DOScale(new Vector3(1,1,1), 0.25f).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(0.01f);
            yield return StartCoroutine(DropDown(i + 1));
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
            foreach (var tile in tiles)
            {
                tile.transform.localScale=new Vector3(1.0f,1.0f,1.0f);
            }
            yield break;
        }
    }
    public int CheckOne()
    {
        return tiles[0].GetID();
    }
    public void CheckWin()
    {
        if(tiles.Count == 0&&!TripleManager.Instance.GetWin())
        {
            TripleManager.Instance.SetWin(true);
            TripleManager.Instance.Pause();
            Invoke(nameof(OpenWin), 0.5f);
            Debug.Log("You Win");
        }
    }
    public void OpenWin()
    {
        SoundManager.Instance.PlaySFX(SFX.Win);
        Transform win = TripleTileUIManager.Instance.GetWinText();
        Transform t = SpawnManager.Instance.Spawn("Multi", win.position.x, win.position.y+1.5f, Quaternion.identity);
        float tmp=0;
        switch (transform.name)
        {
            case("Level-0(Clone)"):
                tmp = 1.5f;
                break;
            case ("Level-1(Clone)"):
                tmp = 2.0f;
                break;
            case ("Level-2(Clone)"):
                tmp = 2.5f;
                break;
            case ("Level-3(Clone)"):
                tmp = 3.0f;
                break;
            case ("Level-4(Clone)"):
                tmp = 3.5f;
                break;
            
        }
        Debug.Log(transform.name + tmp);
        t.GetComponent<TMP_Text>().text = "x " + tmp;
        StartCoroutine(MulScore((float)tmp));
        TripleTileUIManager.Instance.OpenWinPanel();

    }
    IEnumerator MulScore(float val)
    {
        yield return new WaitForSeconds(0.45f);
        TripleManager.Instance.MultiScrore(val);
    }
    public void SetTile()
    {
        int tmp = 0,id=Random.Range(1,19);
        for(int i=0;i<tiles.Count;i++)
        {
            if (tmp == 3)
            {
                tmp = 0;
                id = Random.Range(1, 19);
            }
            tiles[i].SetID(id);
            //Debug.Log("Tile ID: " + id);
            tmp++;
        }
    }
}
