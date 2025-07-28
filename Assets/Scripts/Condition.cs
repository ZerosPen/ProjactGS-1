using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : StateMachine
{
    public virtual bool checkCondition()
    {
        return false;
    }
}
