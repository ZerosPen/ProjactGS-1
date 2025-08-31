using System;
using System.Collections;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<string, Action<object>>  events = new Dictionary<string, Action<object>>();

    public static void RegisterEvent(string eventName, Action<object> listiner)
    {
        if (!events.ContainsKey(eventName))
        {
            events[eventName] = delegate { };
        }
        events[eventName] += listiner;
    }

    public static void DeRegisterEvent(string eventName, Action<Object> listiner)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] -= listiner;
        }
    }

    public static void OnTriggerEvent(string eventName, object param = null)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName].Invoke(param);
        }
    }
}
