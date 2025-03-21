using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : MonoBehaviour
{


    protected virtual void DespawnObj()
    {
        SpawnManager.Instance.Despawn(this.transform);
    }

}
