using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReferenceList : MonoBehaviour
{
    [SerializeField]
    List<UnityEngine.Object> references;
    public List<UnityEngine.Object> References => references;

    public IEnumerable<T> GetReferencesOfType<T>() {
        return References.OfType<T>();
    }

    public T GetReferenceOfType<T>() {
        return GetReferencesOfType<T>().First();
    }
}
