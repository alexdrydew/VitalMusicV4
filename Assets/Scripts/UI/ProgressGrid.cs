using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CustomUI {
    public class ProgressGrid<T> : MonoBehaviour {
        [SerializeField]
        protected List<T> realNames;
        protected List<bool> guessedNames;

        protected virtual void Awake() {
            guessedNames = Enumerable.Repeat(false, realNames.Count).ToList();
        }

        public virtual bool TryToRevealName(int index, T guessedName) {
            if (EqualityComparer<T>.Default.Equals(realNames[index], guessedName)) {
                guessedNames[index] = true;
                return true;
            }
            return false;
        }

        public bool CheckIfComplete() {
            for (int i = 0; i < guessedNames.Count; ++i) {
                if (!guessedNames[i]) {
                    return false;
                }
            }
            return true;
        }
    }
}
