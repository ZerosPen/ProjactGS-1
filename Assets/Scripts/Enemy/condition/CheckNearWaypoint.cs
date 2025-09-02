using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNearWaypoint : Condition
{
    public float nearWaypoint;
    private EnemyMovement _enemyMovement;
    // Start is called before the first frame update
    void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    public override bool checkCondition()
    {
        if (_enemyMovement.distance < nearWaypoint)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
