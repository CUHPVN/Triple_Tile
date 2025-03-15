using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float hp;
    protected float maxHp;
    protected float coin;
    [SerializeField] protected Transform player;

    private void Awake()
    {
        LoadComponent();
    }
    protected virtual void LoadComponent()
    {
        hp = 100;
        maxHp = 100;
        coin = 10;
        player = GameObject.FindWithTag("Player").transform;
    }
    protected virtual void CheckStop()
    { 
        if (Vector2.Distance(player.position, transform.position)<=2){
            MapManager.Instance.Stop();
        }
    }
}
