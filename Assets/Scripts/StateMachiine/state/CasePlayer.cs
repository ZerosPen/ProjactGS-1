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
        enemyMovement.isRoaming = enemyMovement.isRoaming = false;
        enemyMovement.isChasing = enemyMovement.isChasing = true;
        enemyMovement.OnChasePlayer(playerPos.transform.position);
    }
}
