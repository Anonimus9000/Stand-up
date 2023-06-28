using System;
using UnityEngine;

namespace Script.ProjectLibraries.MVVM
{
public abstract class BehaviourDisposableBase : MonoBehaviour, IDisposable
{
    protected bool disposed;
    protected readonly CompositeDisposable compositeDisposable = new();

    public void Dispose()
    {
        compositeDisposable.Dispose();
        OnDispose();
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }

            disposed = true;
        }
    }

    protected virtual void OnDispose() { }

    private void OnDestroy()
    {
        Dispose();
    }
}
}