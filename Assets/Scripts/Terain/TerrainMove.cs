using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMove : MonoBehaviour
{
    [SerializeField] private float speed=0.5f;
    [SerializeField] private bool spawnnext = false;
    void OnEnable()
    {
        spawnnext = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Spawn();
        Despawn();
    }
    private void Move()
    {
        if(!MapManager.Instance.GetMove())
        {
            return;
        }
        speed = MapManager.Instance.GetSpeed();
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    void Spawn()
    {
        if (Vector2.Distance(transform.position, Camera.main.transform.position) <= 1&&!spawnnext)
        {
            spawnnext = true;
            MapManager.Instance.SpawnTile(new Vector2(transform.position.x+14, transform.position.y));
        }
    }
    void Despawn()
    {
        if (transform.position.x < Camera.main.transform.position.x - 20)
        {
            SpawnManager.Instance.Despawn(transform);
        }
    }
}
