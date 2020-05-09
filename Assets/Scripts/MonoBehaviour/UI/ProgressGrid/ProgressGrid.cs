using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ProgressGrid {
    public class ProgressGrid : MonoBehaviour {
        [SerializeField]
        protected ProgressGridData data;
        protected List<bool> guessedNames;

        protected virtual void Awake() {
            guessedNames = Enumerable.Repeat(false, data.GetNamesCount()).ToList();
        }

        protected bool TryToRevealName(int index, System.IComparable guessedName) {
            if (data.CheckForEquality(index, guessedName)) {
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
