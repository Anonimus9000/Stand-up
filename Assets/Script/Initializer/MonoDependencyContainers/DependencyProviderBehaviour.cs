using System;
using System.Collections.Generic;
using System.Linq;
using Script.Initializer.Base;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using UnityEngine;
using ILogger = Script.Libraries.Logger.LoggerBase.ILogger;
using IServiceProvider = Script.Libraries.ServiceProvider.IServiceProvider;

namespace Script.Initializer.MonoDependencyContainers
{
[CreateAssetMenu(fileName = "DependencyProvider", menuName = "ScriptableObjects/MonoDependencyProvider", order = 1)]
public class DependencyProviderBehaviour : ScriptableObject, IDependencyProvider
{
    private readonly List<object> _dependencies = new();
    
    public void AddDependency(object dependency)
    {
        if (_dependencies.Contains(dependency))
        {
            throw new Exception($"Dependency {dependency.GetType()} is already contained");
        }
        
        _dependencies.Add(dependency);
    }

    public T GetDependency<T>()
    {
        foreach (var initializable in _dependencies.OfType<T>())
        {
            return initializable;
        }

        throw new Exception($"Can't find {typeof(T)}");
    }
}
}