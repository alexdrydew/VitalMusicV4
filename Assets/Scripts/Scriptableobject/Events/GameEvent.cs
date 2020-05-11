using UnityEngine;
using UnityEngine.Events;

namespace GameEvent {
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Events/GameEvent")]
    public class GameEvent : ScriptableObject {
        private UnityEvent unityEvent;

        public void Invoke() {
            if (unityEvent == null) unityEvent = new UnityEvent();
            unityEvent.Invoke();
        }

        public void AddListener(UnityAction action) {
            unityEvent.AddListener(action);
        }

        public void RemoveListener(UnityAction action) {
            unityEvent.RemoveListener(action);
        }
    }
}