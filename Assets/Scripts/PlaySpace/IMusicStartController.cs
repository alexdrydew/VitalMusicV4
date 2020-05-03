using UnityEngine.Events;

public interface IMusicStartController
{
    UnityEvent<int> StartBlockChanged { get; set; }
}
