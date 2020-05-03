using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerBlockEvent : UnityEvent<int> { }
public class StartedEvent : UnityEvent { }
public class StoppedEvent : UnityEvent { }
public class EditorPointerPosChanged : UnityEvent<int> { }

namespace CustomUI {
    public class ButtonPressedEvent : UnityEvent { }
    public class SelectableButtonPressedEvent : UnityEvent<bool> { }
}