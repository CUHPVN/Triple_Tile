using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance{ get; private set; }
    [SerializeField] private List<Transform> tilePrefab;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private bool canMove=true;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SaveSystem.Instance.TakeData();
        if (SaveSystem.Instance.data.map.name!=null&& SaveSystem.Instance.data.map.hp>0)
        {
            SaveData.Map map = SaveSystem.Instance.data.map;
            Transform spawnObj = SpawnManager.Instance.Spawn(map.name, 0, 0, Quaternion.identity);
            spawnObj.GetComponentInChildren<Enemy>().SetHP(map.hp);
            spawnObj.GetComponentInChildren<Enemy>().SetMaxHP(map.maxHp);
            spawnObj.GetComponentInChildren<Enemy>().SetCoin(map.coin);
        }
        else
        SpawnBaseTile(new Vector2(0,0));
    }
    public void Update()
    {
        
    }
    public void SpawnBaseTile(Vector2 pos)
    {
        SpawnManager.Instance.Spawn("Base Terain", pos.x, pos.y, Quaternion.identity);
    }
    public void SpawnTile(Vector2 pos)
    {
        int ran = Random.Range(0, tilePrefab.Count);
        SpawnManager.Instance.Spawn(tilePrefab[ran].name, pos.x, pos.y, Quaternion.identity);
    }
    public float GetSpeed()
    {
        return speed;
    }
    public bool GetMove()
    {
        return canMove;
    }
    public void Stop()
    {
        canMove = false;
    }
    public void Continues()
    {
        canMove = true;
    }
}
