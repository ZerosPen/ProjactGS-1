using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasePlayer : State
{
    private EnemyMovement enemyMovement;
    private Enemy enemyScript;
    public GameObject playerPos;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyScript = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyScript.isRoaming = enemyMovement.isRoaming = false;
        enemyScript.isChasing = enemyMovement.isChasing = true;
        enemyMovement.moveToPosX(playerPos.transform.position);
    }
}
