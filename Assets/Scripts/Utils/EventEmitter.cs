using System;
using System.Collections.Generic;
using UnityEngine;

public class EventEmitter
{
    private readonly Dictionary<string, List<Action<object[]>>> events = new Dictionary<string, List<Action<object[]>>>();
    private readonly Dictionary<string, List<object>> hosts = new Dictionary<string, List<object>>();

    public void On(string eventName, Action<object[]> listener, object host = null)
    {
        if (!events.ContainsKey(eventName))
        {
            events[eventName] = new List<Action<object[]>>();
            hosts[eventName] = new List<object>();
        }

        events[eventName].Add(listener);
        hosts[eventName].Add(host);
    }

    public void RemoveListener(string eventName, Action<object[]> listener, object host = null)
    {
        if (events.ContainsKey(eventName) && hosts.ContainsKey(eventName))
        {
            int index = events[eventName].IndexOf(listener);
            if (index > -1)
            {
                events[eventName].RemoveAt(index);
                hosts[eventName].RemoveAt(index);
            }
        }
    }

    public void Emit(string eventName, params object[] args)
    {
        if (events.ContainsKey(eventName))
        {
            for (int i = 0; i < events[eventName].Count; i++)
            {
                events[eventName][i]?.Invoke(args);
            }
        }
    }

    public void Once(string eventName, Action<object[]> listener, object host = null)
    {
        Action<object[]> wrappedListener = null; // Declare outside so it can reference itself
        wrappedListener = (args) =>
        {
            RemoveListener(eventName, wrappedListener, host);
            listener(args);
        };

        On(eventName, wrappedListener, host);
    }
}

public static class EVENT_TYPES
{
    public const string VIBRATE_EFFECT = "VIBRATE_EFFECT";
}


public class EventEmitterClass
{
    public EventEmitter VibrateEventEmitter = new EventEmitter();

    public static EventEmitterClass instance = null;

    public static EventEmitterClass GetInstance()
    {
        if(EventEmitterClass.instance == null)
            EventEmitterClass.instance = new EventEmitterClass();
        return EventEmitterClass.instance;
    }

    void Start()
    {
       /* // Example usage
        Action<object[]> listener = (args) => { Debug.Log($"Event received with message: {args[0]}"); };
        VibrateEventEmitter.On("testEvent", listener);

        // Emitting event
        VibrateEventEmitter.Emit("testEvent", "Hello World!");

        // Once example
        VibrateEventEmitter.Once("onceEvent", (args) => { Debug.Log($"Once event received with message: {args[0]}"); });
        VibrateEventEmitter.Emit("onceEvent", "This will be received.");
        VibrateEventEmitter.Emit("onceEvent", "This will not be received.");

        // Removing listener
        VibrateEventEmitter.RemoveListener("testEvent", listener);
        VibrateEventEmitter.Emit("testEvent", "This message won't be logged.");*/
    }
}