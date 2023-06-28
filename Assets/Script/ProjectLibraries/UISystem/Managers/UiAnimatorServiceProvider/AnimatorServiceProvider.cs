using System;
using System.Collections.Generic;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;

namespace Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider
{
public class AnimatorServiceProvider : IAnimatorServiceProvider
{
    private readonly List<IAnimatorService> _services;

    public AnimatorServiceProvider(params IAnimatorService[] services)
    {
        _services = new List<IAnimatorService>(services);
    }

    public T GetService<T>() where T : IAnimatorService
    {
        if (_services == null)
        {
            throw new Exception("Need initialize provider before use");
        }

        foreach (var uiAnimatorService in _services)
        {
            if (uiAnimatorService is T service)
            {
                return service;
            }
        }

        throw new Exception($"Can't find {typeof(T)} service. Need add this service");
    }
}
}