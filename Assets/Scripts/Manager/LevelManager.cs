using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

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
            i.transform.position = i.localPos + new Vector3(0, 20, 0);
        }
    }
    private IEnumerator DropDown(int i)
    {
        if(i < tiles.Count)
        {
            tiles[i].transform.DOMove(tiles[i].localPos, 0.25f).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(0.01f);
            yield return StartCoroutine(DropDown(i + 1));

        }
        else
        {
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
            TripleTileUIManager.Instance.OpenWinPanel();
            Debug.Log("You Win");
        }
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
