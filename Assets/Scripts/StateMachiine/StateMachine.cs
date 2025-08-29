using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public List<State> states;
    public List<Condition> conditions;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i].checkCondition())
            {
                states[i].enabled = true;
            }
            else
            {
                states[i].enabled = false;
            }
        }
    }

}
