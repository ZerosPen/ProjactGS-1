using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHealtPoint : Condition
{
    public float healtPointTrigger;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    public override bool checkCondition()
    {
        if (_enemy.healthPoint <= healtPointTrigger)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
