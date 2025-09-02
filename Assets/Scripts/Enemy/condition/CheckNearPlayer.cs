using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNearPlayer : Condition
{
    public float viewRange;
    public bool seePlayer;
    private Enemy _enemy;
    private EnemyMovement _enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    public override bool checkCondition()
    {
        if (_enemy.distancePlayer <= viewRange)
        {
            seePlayer = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
