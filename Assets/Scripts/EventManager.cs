using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static  EventManager instance;
    private Dictionary<Type, List<Action<object>>> _eventListeners = new Dictionary<Type, List<Action<object>>>();
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddListener<T>(Action<object> callback) where T : IEvent
    {
        Type eventType = typeof(T);
        if (!_eventListeners.ContainsKey(eventType))
        {
            _eventListeners[eventType] = new List<Action<object>>();
        }
        _eventListeners[eventType].Add(callback);
    }

    public void RemoveListener<T>(Action<object> callback) where T : IEvent
    {
        Type eventType = typeof(T);
        if (_eventListeners.ContainsKey(eventType))
        {
            _eventListeners[eventType].Remove(callback);
        }
    }

    public void TriggerEvent<T>(T eventData) where T : IEvent
    {
        Type eventType = typeof(T);
        if (_eventListeners.ContainsKey(eventType))
        {
            foreach (Action<object> callback in _eventListeners[eventType])
            {
                callback(eventData);
            }
        }
        
    }
}
