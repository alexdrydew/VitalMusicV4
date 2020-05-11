using System;

public class DataException<T> : ApplicationException {
    public DataException() : base($"No data assigned to {typeof(T)}") { }
}

public class InstantiationException<T> : ApplicationException {
    public InstantiationException() : base($"Wrong method used to instantiate {typeof(T)}") { }
}