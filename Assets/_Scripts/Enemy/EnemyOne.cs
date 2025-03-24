using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : Enemy
{
    void Awake()
    {
        LoadComponent();
    }

    void Update()
    {
        CheckStop();
    }
}
