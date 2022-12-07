using System;
using System.Collections.Generic;
using System.Linq;
using Script.Initializer.Base;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using UnityEngine;
using ILogger = Script.Libraries.Logger.LoggerBase.ILogger;
using IServiceProvider = Script.Libraries.ServiceProvider.IServiceProvider;

namespace Script.Initializer.MonoDependencyContainers
{
[CreateAssetMenu(fileName = "DependencyProvider", menuName = "ScriptableObjects/MonoDependencyProvider", order = 1)]
public class MonoDependencyProvider : ScriptableObject, IDependencyProvider
{
    private readonly List<object> _dependencyes = new();

    public void InitializeDependencies(IServiceProvider dataService, IUIManager uiManager, ILogger logger)
    {
        _dependencyes.Add(dataService);
        _dependencyes.Add(uiManager);
        _dependencyes.Add(logger);
    }

    public void AddDependency(object dependency)
    {
        if (_dependencyes.Contains(dependency))
        {
            throw new Exception($"Dependency {dependency.GetType()} is already contained");
        }
        
        _dependencyes.Add(dependency);
    }

    public T GetDependency<T>()
    {
        foreach (var initializable in _dependencyes.OfType<T>())
        {
            return initializable;
        }

        throw new Exception($"Can't find {typeof(T)}");
    }
}
}