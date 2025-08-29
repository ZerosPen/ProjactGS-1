using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dogde : State
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
        Debug.Log("Try to jump!");
    }
}
