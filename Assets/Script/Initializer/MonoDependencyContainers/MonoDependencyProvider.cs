using System;
using System.Collections.Generic;
using System.Linq;
using Script.Initializer.Base;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using UnityEngine;
using IServiceProvider = Script.Libraries.ServiceProvider.IServiceProvider;

namespace Script.Initializer.MonoDependencyContainers
{
[CreateAssetMenu(fileName = "DependencyProvider", menuName = "ScriptableObjects/MonoDependencyProvider", order = 1)]
public class MonoDependencyProvider : ScriptableObject, IDependencyProvider
{
    private readonly List<object> _initializables = new();

    public void InitializeDependencies(IServiceProvider dataService, IUIManager uiManager)
    {
        _initializables.Add(dataService);
        _initializables.Add(uiManager);
    }

    public T GetInitializable<T>()
    {
        foreach (var initializable in _initializables.OfType<T>())
        {
            return initializable;
        }

        throw new Exception($"Can't find {typeof(T)}");
    }
}
}