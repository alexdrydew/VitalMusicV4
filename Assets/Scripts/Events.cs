using UnityEngine.Events;

namespace ChordEditor {
    public class ChordAttachedToSlotEvent : UnityEvent { }
}

namespace NoteEditor {
    public class NoteSelectedEvent : UnityEvent<int> { }

    public class NoteSelectorUpdatedEvent : UnityEvent<int> { }

    public class NoteEditorUpdated : UnityEvent { }
}

public class PerBlockEvent : UnityEvent<int> { }

public class StartedEvent : UnityEvent { }

public class StoppedEvent : UnityEvent { }

public class EditorPointerPosChanged : UnityEvent<int> { }

namespace CustomUI {
    public class ButtonPressedEvent : UnityEvent { }

    public class SelectableButtonPressedEvent : UnityEvent<bool> { }
}