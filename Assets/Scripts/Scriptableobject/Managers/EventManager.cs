using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public abstract class EventManager : ScriptableObject {
    [SerializeField]
    private MusicSystem musicSystem;

    protected List<MusicEvent> pendingEvents = new List<MusicEvent>();

    protected List<MusicEvent> registeredEvents = new List<MusicEvent>();
    public MusicSystem MusicSystem => musicSystem;

    public void Init() {
        registeredEvents.Clear();
        pendingEvents.Clear();
    }

    public void ResetEvents() {
        foreach (MusicEvent ev in registeredEvents) MusicSystem.RemoveEvent(ev);
    }

    public void UpdateEvents() {
        foreach (MusicEvent ev in pendingEvents) {
            if (!MusicSystem.AddEvent(ev)) throw new ApplicationException("Failed adding chord");
            registeredEvents.Add(ev);
        }

        pendingEvents.Clear();
    }
}