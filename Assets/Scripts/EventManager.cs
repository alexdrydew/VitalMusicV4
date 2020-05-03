using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class PlaybackStartedEvent : UnityEvent { }
public class ChordPlacedEvent : UnityEvent { }
public class PointerMovedEvent : UnityEvent { }
public class ChordLibraryOpenedEvent : UnityEvent { }

public static class GlobalEventsManager
{
    public static PlaybackStartedEvent PlaybackStarted { get; } = new PlaybackStartedEvent();
    public static ChordPlacedEvent ChordPlaced { get; } = new ChordPlacedEvent();
    public static PointerMovedEvent PointerMoved { get; } = new PointerMovedEvent();
    public static ChordLibraryOpenedEvent ChordLibraryOpened { get; } = new ChordLibraryOpenedEvent();
}
