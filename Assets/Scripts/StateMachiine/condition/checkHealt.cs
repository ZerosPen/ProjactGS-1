using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkHealt : Condition
{
    public float healthTrigger;
    private Enemy enemyScript;

    private void Start()
    {
        enemyScript = GetComponent<Enemy>();
    }

    public override bool checkCondition()
    {
        if(enemyScript.healthPoint <= healthTrigger)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
