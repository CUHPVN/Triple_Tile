using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : Despawn
{
    float time = 0;
    [SerializeField] public float maxTime = 1f;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > maxTime)
        {
            time = 0;
            SpawnManager.Instance.Despawn(this.transform);
        }
    }

}
