using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : State
{
    private EnemyMovement enemyMovement;
    public GameObject Object;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyMovement.moveToPosX(Object.transform.position);
    }
}
