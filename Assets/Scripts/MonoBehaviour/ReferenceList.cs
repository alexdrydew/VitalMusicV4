using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReferenceList : MonoBehaviour {
    [SerializeField]
    private List<Object> references;

    public List<Object> References => references;

    public IEnumerable<T> GetReferencesOfType<T>() {
        return References.OfType<T>();
    }

    public T GetReferenceOfType<T>() {
        return GetReferencesOfType<T>().First();
    }
}