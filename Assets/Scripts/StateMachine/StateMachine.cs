using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[System.Serializable]
public class StateEntry
{
    public State state;
    public Condition condition;
    public int priority;
}

public class StateMachine : MonoBehaviour
{
    public List<StateEntry> entries;
    private State currstate;

    private void Update()
    {
        for (int i = 0; i < entries.Count; i++)
        {
            if (entries[i].condition.checkCondition())
            {
                entries[i].state.enabled = true;
            }
            else
            {
                entries[i].state.enabled = false;
            }
        }
        /*StateEntry bestEntry = null;

        foreach (var entry in entries)
        {
            if (entry.condition.checkCondition())
            {
                if (bestEntry == null || entry.priority > bestEntry.priority)
                {
                    bestEntry = entry;
                }
            }
        }

        if (bestEntry != null)
        {
            SwitchState(bestEntry.state);
        }*/
    }

    /*private void SwitchState(State newstate)
    {
       if (currstate == newstate) return;

       if (currstate != null)
            currstate.enabled = false;

       newstate.enabled = true;
       currstate = newstate;
    }*/
}
