using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisualNovel {
    [CreateAssetMenu(fileName = "Scene", menuName = "VisualNovel/Scene")]
    public class Scene : ScriptableObject {
        [SerializeField]
        private Sprite backgroundImage;
        [SerializeField]
        private List<Replica> replicas;

        public Sprite BackgroundImage => backgroundImage;
        public List<Replica> Replicas => replicas;
    }
}
