using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : State
{
    private EnemyMovement _enemyMovement;

    private void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void FixedUpdate()
    {
        _enemyMovement.OnPatrol();
    }
}
