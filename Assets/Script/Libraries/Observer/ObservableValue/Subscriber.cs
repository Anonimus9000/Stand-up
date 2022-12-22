using System;

namespace Script.Libraries.Observer.ObservableValue
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