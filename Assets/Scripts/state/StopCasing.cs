using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCasing : State
{
    private EnemyMovement enemyMovement;
    private Enemy enemyScript;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyScript = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyScript.isRoaming = enemyMovement.isRoaming = true;
        enemyScript.isChasing = enemyMovement.isChasing = false;
        enemyMovement.StopMove();
        enemyMovement.SetNewTargetPos();
    }
}
