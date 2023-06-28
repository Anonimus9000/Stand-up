using System;

namespace Script.ProjectLibraries.Observer.ObservableValue
{
public struct Subscriber<T>
{
    public Action<T> OnNotify { get; }
    public bool IsSingleNotify { get; }

    public Subscriber(Action<T> onNotify, bool isSingleNotify)
    {
        OnNotify = onNotify;
        IsSingleNotify = isSingleNotify;
    }
}
}