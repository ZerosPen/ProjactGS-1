using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Roaming : State
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
        enemyMovement.moveToPosX(enemyMovement.targetPosition);
    }
}
