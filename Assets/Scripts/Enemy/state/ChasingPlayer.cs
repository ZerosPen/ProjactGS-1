using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingPlayer : State
{
    private Enemy _enemy;
    private EnemyMovement _enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (_enemy.PlayerPos.transform.position - transform.position).normalized;
        _enemyMovement.OnChasePlayer(direction);
    }
}
