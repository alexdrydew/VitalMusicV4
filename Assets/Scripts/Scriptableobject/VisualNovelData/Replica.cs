using UnityEngine;

namespace VisualNovel {
    [CreateAssetMenu(fileName = "Replica", menuName = "VisualNovel/Replica")]
    public class Replica : ScriptableObject {
        [SerializeField]
        private Character character;

        [SerializeField] [TextArea(3, 10)]
        private string what = "";

        public string What => what;
        public string Who => character.CharacterName;
    }
}