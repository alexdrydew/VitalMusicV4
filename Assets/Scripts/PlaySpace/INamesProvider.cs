using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INamesProvider<T> {
    List<T> CurrentNames { get; }
}
