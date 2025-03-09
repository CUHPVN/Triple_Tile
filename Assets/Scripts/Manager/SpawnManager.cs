using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SpawnManager : MonoBehaviour
{


    private static SpawnManager instance;
    public static SpawnManager Instance { get { return instance; } }

    [SerializeField] private GameObject holder;
    [SerializeField] private List<Transform> prefabs = new();
    [SerializeField] private List<Transform> poolObjs = new();

    private void Awake()
    {
        instance = this;
        holder = GameObject.Find("Holder");
        this.LoadPrefabs();
    }
    void Start()
    {


    }


    public Transform Spawn(string prefabName, float x, float y, Quaternion rotate)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.LogWarning("Prefab not found: " + prefabName);
            return null;
        }
        Transform ObjSpawn = GetObjectFromPool(prefab);
        ObjSpawn.position = new Vector2(x, y);
        ObjSpawn.rotation = rotate;
        ObjSpawn.SetParent(holder.transform);
        ObjSpawn.gameObject.SetActive(true);
        return ObjSpawn;
    }

    private Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform poolObj in this.poolObjs)
        {
            if (poolObj.name == prefab.name)
            {
                this.poolObjs.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }

    private void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        Transform prefabObj = GameObject.Find("Prefabs").transform;
        foreach (Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }
        this.HidePrefabs();
        Debug.Log(transform.name + ": Load Prefab", gameObject);

    }
    private void HidePrefabs()
    {
        foreach (Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    private Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == prefabName) return prefab;

        }
        return null;
    }

    public void Despawn(Transform obj)
    {
        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
    }


    void Update()
    {

    }
}
