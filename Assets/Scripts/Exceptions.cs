using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataException<T> : System.ApplicationException {
    public DataException() : base($"No data assigned to {typeof(T)}") {
    }
}

public class InstantiationException<T> : System.ApplicationException {
    public InstantiationException() : base($"Wrong method used to instantiate {typeof(T)}") {
    }
}

