﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace Script.ProjectLibraries.MVVM
{
public class DisposableBase : IDisposable
{
    public bool Disposed { get; private set; }
    protected readonly CompositeDisposable compositeDisposable = new();
    protected CancellationToken _cancellationToken;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly List<IDisposable> _disposables = new(10);

    protected DisposableBase()
    {
        AddDisposable(compositeDisposable);
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected TDisposable AddDisposable<TDisposable>(TDisposable disposable) where TDisposable : IDisposable
    {
        _disposables.Add(disposable);
        return disposable;
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!Disposed)
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            Disposed = true;
            
            _cancellationTokenSource?.Cancel();
        }
    }

    ~DisposableBase()
    {
        Dispose(false);
    }
}
}