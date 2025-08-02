using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistanceFar : Condition
{
    [Header("Status & Details")]
    public float distance;
    private Enemy enemyScript;

    // Update is called once per frame
    void Update()
    {
        enemyScript = GetComponent<Enemy>();
    }

    public override bool checkCondition()
    {
        if (Mathf.Abs(enemyScript.distanceTarget) >= distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
