using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public struct UndoTile
{
    public int id;
    public NodeTile node;
}
public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }
    [SerializeField] private Transform hand;
    [SerializeField] private Transform bonusSlot;
    [SerializeField] private BonusHandNodeTile bonusHandNodeTile;
    [SerializeField] private List<HandNodeTile> tiles = new();
    [SerializeField] private Queue<int> queue = new();
    [SerializeField] public List<UndoTile> undos = new();
    private int maxHandSize = 7;
    private List<int> tileCount = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadHand();
    }

    public void AddTile(int id)
    {
        if (tiles.Count(tile => tile.GetID() != 0) >= maxHandSize)
        {
            queue.Enqueue(id);
        }
        else
        {
            bool isExist = false;
            for (int j = 0; j < maxHandSize - 1; j++)
            {
                if (tiles[j].GetID() == id && tiles[j + 1].GetID() != id && tiles[j + 1].GetID() != 0)
                {
                    for (int k = maxHandSize - 1; k > j; k--)
                    {
                        tiles[k].SetIDOne(tiles[k - 1].GetID());
                    }
                    tiles[j].SetID(id);
                    tileCount[id]++;
                    isExist = true;
                    break;
                }
            }
            if (!isExist)
            {
                var emptyTile = tiles.FirstOrDefault(tile => tile.GetID() == 0);
                if (emptyTile != null)
                {
                    emptyTile.SetID(id);
                    tileCount[id]++;
                    isExist = true;
                }
            }
        }
        Sort();
        CheckTriple();
        CheckEnd();
    }
    public Vector3 AddTilePos(int id)
    {
        if (tiles.Count(tile => tile.GetID() != 0) >= maxHandSize)
        {
            queue.Enqueue(id);
            Sort();
            CheckTriple();
            CheckEnd();
            bonusHandNodeTile.SetID(id);
            return bonusSlot.transform.position;
        }
        else
        {
            bool isExist = false;
            for (int j = 0; j < maxHandSize - 1; j++)
            {
                if (tiles[j].GetID() == id && tiles[j + 1].GetID() != id && tiles[j + 1].GetID() != 0)
                {
                    for (int k = maxHandSize - 1; k > j; k--)
                    {
                        tiles[k].SetIDOne(tiles[k - 1].GetID());
                    }
                    tiles[j+1].SetID(id);
                    tileCount[id]++;
                    isExist = true;
                    Sort();
                    CheckTriple();
                    CheckEnd();
                    return tiles[j].transform.position;
                }
            }
            if (!isExist)
            {
                var emptyTile = tiles.FirstOrDefault(tile => tile.GetID() == 0);
                if (emptyTile != null)
                {
                    emptyTile.SetID(id);
                    tileCount[id]++;
                    isExist = true;
                    Sort();
                    CheckTriple();
                    CheckEnd();
                    return emptyTile.transform.position;
                }
            }
        }
        return bonusSlot.transform.position;
    }
    public Vector3 AddTilePosWOE(int id)
    {
        if (tiles.Count(tile => tile.GetID() != 0) >= maxHandSize)
        {
            if (tiles[maxHandSize - 1].GetID() != 0)
            {
                queue.Enqueue(tiles[maxHandSize - 1].GetID());
                tileCount[tiles[maxHandSize - 1].GetID()]--;
                tiles[maxHandSize - 1].SetID(0);
            }
        }
        
        {
            bool isExist = false;
            for (int j = 0; j < maxHandSize - 1; j++)
            {
                if (tiles[j].GetID() == id && tiles[j + 1].GetID() != id && tiles[j + 1].GetID() != 0)
                {
                    for (int k = maxHandSize - 1; k > j; k--)
                    {
                        tiles[k].SetIDOne(tiles[k - 1].GetID());
                    }
                    tiles[j + 1].SetID(id);
                    tileCount[id]++;
                    isExist = true;
                    Sort();
                    CheckTriple();
                    return tiles[j].transform.position;
                }
            }
            if (!isExist)
            {
                var emptyTile = tiles.FirstOrDefault(tile => tile.GetID() == 0);
                if (emptyTile != null)
                {
                    emptyTile.SetID(id);
                    tileCount[id]++;
                    isExist = true;
                    Sort();
                    CheckTriple();
                    return emptyTile.transform.position;
                }
            }
        }
        return bonusSlot.transform.position;
    }
    public void Sort()
    {
        for (int i = 0; i < maxHandSize; i++)
        {
            for (int j = i + 1; j < maxHandSize; j++)
            {
                if (tiles[i].GetID() == 0)
                {
                    int temp = tiles[i].GetID();
                    tiles[i].SetID(tiles[j].GetID());
                    tiles[j].SetID(temp);
                }
            }
        }
    }
    public void AddUndo(int id, NodeTile node)
    {
        undos.Add(new UndoTile { id = id, node = node });
    }
    public void RemoveUndo()
    {
        if (undos.Count == 0) return;
        UndoTile undo = undos[undos.Count - 1];
        GameObject.FindFirstObjectByType<LevelManager>().Add(undo.node);
        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            if (tiles[i].GetID() == undo.id)
            {
                tiles[i].SetID(0);
                tileCount[undo.id]--;
                undo.node.posToMove = tiles[i].transform.position;
                break;
            }
        }
        undo.node.MoveUp();
        
        undos.RemoveAt(undos.Count - 1);
    }
    public int GetUndoCount()
    {
        return undos.Count;
    }
    public void PrintUndo()
    {
        foreach (var undo in undos)
        {
            Debug.Log(undo.id);
        }
    }
    public void CheckTriple()
    {
        for (int i = 0; i < 20; i++)
        {
            if (tileCount[i] >= 3)
            {
                tileCount[i] -= 3;
                //RemoveTile(i);
                StartCoroutine(RemoveTileWithDelay(i));
            }
        }
    }
    private void RemoveTile(int id)
    {
        foreach (var tile in tiles.Where(tile => tile.GetID() == id))
        {
            tile.Remove(0);
        }
    }
    private IEnumerator RemoveTileWithDelay(int id)
    {
        yield return new WaitForSeconds(0.5f);
        int tmp = 0;
        for (int i = 0; i < maxHandSize; i++)
        {
            if (tiles[i].GetID() == id)
            {
                tiles[i].Remove(0);
                undos.RemoveAll(undo => undo.id == id);
                tmp++;
                TripleManager.Instance.AddScore(1);
                if (tmp == 3)
                {
                    break;
                }
            }
        }
        Sort();
        TryAddTileFromQueue();
    }
    private void TryAddTileFromQueue()
    {
        while (tiles.Count(tile => tile.GetID() == 0) > 0 && queue.Count > 0)
        {
            int id = queue.Dequeue();
            bonusHandNodeTile.Remove(id,AddTilePos(id));
        }
    }

    public void CheckEnd()
    {
        int count = tileCount.Sum();
        if (count >= maxHandSize)
        {
            if (!TripleManager.Instance.GetLose())
            {
                TripleManager.Instance.SetLose(true);
                TripleManager.Instance.Pause();
                TripleTileUIManager.Instance.OpenWinPanel();
                Debug.Log("You Lose");
            }
        }
        Sort();
    }
    public int CheckOne()
    {
        for (int i = 0; i < maxHandSize; i++)
        {
            if (tiles[i].GetID() != 0)
            {
                return tiles[i].GetID();
            }
        }
        return 0;
    }
    public int CheckCount(int id)
    {
        return tileCount[id];
    }
    public int GetQueueCount()
    {
        return queue.Count;
    }
    public int GetQueueFirst()
    {
        return queue.Peek();
    }
    private void LoadHand()
    {
        tiles = hand.GetComponentsInChildren<HandNodeTile>().ToList();
        tileCount = Enumerable.Repeat(0, 20).ToList();
    }
}
