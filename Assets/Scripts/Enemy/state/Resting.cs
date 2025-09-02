using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resting : State
{
    private bool isResting;
    private EnemyMovement _enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        _enemyMovement =  GetComponent<EnemyMovement>();
    }

    void OnEnable()
    {
        // Start resting when we enter this state
        StartCoroutine(_enemyMovement.RestRoaming());
    }

    // Update is called once per frame
    void Update()
    {
        // allow state machine to switch again
        if (!_enemyMovement.IsResting)
        {
            enabled = false; // exit this state
        }
    }
}
