using System;
using System.Collections.Generic;

namespace Script.ProjectLibraries.MVVM
{
public class CompositeDisposable : IDisposable
{
    public readonly List<IDisposable> disposables = new(10);

    public void AddDisposable(IDisposable disposable)
    {
        disposables.Add(disposable);
    }

    public void Dispose()
    {
        foreach (var disposable in disposables)
        {
            disposable.Dispose();
        }
        
        GC.SuppressFinalize(this);
    }
}
}