using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisualNovel {
    [CreateAssetMenu(fileName = "Character", menuName = "VisualNovel/Character")]
    public class Character : ScriptableObject {
        [SerializeField]
        private string characterName;
        public string CharacterName => characterName;
    }
}
