﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    private Dictionary<string, UnityEvent> eventDictionary;
    private static EventManager eventManager;

    public static EventManager instance {
        get {
            if (!eventManager) {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager) {
                    Debug.LogError("Missing an active EventManger script on a GameObject in this scene.");
                }
                else {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init() {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener) {
        //if (eventManager == null) return;
        UnityEvent thisEvent = null;

        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        }
        else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
        //Debug.Log(string.Format("Registered Event: {0}", eventName));
    }

    public static void StopListening(string eventName, UnityAction listener) {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
   
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
        //Debug.Log(string.Format("Unregistered Event: {0}", eventName));
    }

    public static void TriggerEvent(string eventName) {
        //if (eventManager == null) return;
        UnityEvent thisEvent = null;

        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke();
        }
        //Debug.Log(string.Format("Tried to trigger Event: {0}", eventName));
    }
}