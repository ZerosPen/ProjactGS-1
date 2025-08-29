using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistanceClose : Condition
{
    [Header("Status & Details")]
    public float distance;
    [SerializeField] private  Enemy enemyScript;

    private void Start()
    {
        enemyScript = GetComponent<Enemy>();
        if (enemyScript == null)
        {
            Debug.LogError("Enemy script not found on " + gameObject.name);
        }
    }

    public override bool checkCondition()
    {
        if (enemyScript.distancePlayer <= distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
