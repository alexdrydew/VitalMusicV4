using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;


public interface IGlobalEvent { }

public class GlobalEvent : IGlobalEvent {
    public event Action<object> Event;

    public void Invoke(object sender) {
        Event?.Invoke(sender);
    }
}

public class GlobalEvent<T> : IGlobalEvent {
    public event System.EventHandler<T> Event;

    public void Invoke(object sender, T args) {
        Event(sender, args);
    }
}

public enum GlobalEventType {
    PlaybackStartedEvent,
    ChordPlacedEvent,
    PointerMovedEvent,
    ChordLibraryOpenedEvent,
    MessageBoxPressed
}

public static class GlobalEventsManager {
    private static Dictionary<GlobalEventType, Dictionary<System.Type, IGlobalEvent>> genericEventDictionary;
    private static Dictionary<GlobalEventType, GlobalEvent> eventDictionary;

    static GlobalEventsManager() {
        genericEventDictionary = new Dictionary<GlobalEventType, Dictionary<Type, IGlobalEvent>>();
        eventDictionary = new Dictionary<GlobalEventType, GlobalEvent>();
    }

    public static void AddListener<T>(GlobalEventType eventType, System.EventHandler<T> action) {
        if (!genericEventDictionary.ContainsKey(eventType)) {
            genericEventDictionary.Add(eventType, new Dictionary<Type, IGlobalEvent>());
        }
        var typeDictionary = genericEventDictionary[eventType];
        if (!typeDictionary.ContainsKey(typeof(T))) {
            typeDictionary.Add(typeof(T), new GlobalEvent<T>());
        }
        (typeDictionary[typeof(T)] as GlobalEvent<T>).Event += action;
    }

    public static void RemoveListener<T>(GlobalEventType eventType, System.EventHandler<T> action) {
        (genericEventDictionary[eventType][typeof(T)] as GlobalEvent<T>).Event -= action;
    }

    public static void AddListener(GlobalEventType eventType, Action<object> action) {
        if (!eventDictionary.ContainsKey(eventType)) {
            eventDictionary.Add(eventType, new GlobalEvent());
        }
        eventDictionary[eventType].Event += action;
    }

    public static void RemoveListener(GlobalEventType eventType, Action<object> action) {
        eventDictionary[eventType].Event -= action;
    }

    public static void Invoke<T>(GlobalEventType eventType, object sender, T args) {
        if (!genericEventDictionary.ContainsKey(eventType)) {
            return;
        }
        var typeDictionary = genericEventDictionary[eventType];
        if (!typeDictionary.ContainsKey(typeof(T))) {
            return;
        }
        (typeDictionary[typeof(T)] as GlobalEvent<T>).Invoke(sender, args);
    }

    public static void Invoke(GlobalEventType eventType, object sender) {
        if (!eventDictionary.ContainsKey(eventType)) {
            return;
        }
        eventDictionary[eventType].Invoke(sender);
    }
}
