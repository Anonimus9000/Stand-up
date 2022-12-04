using System;
using System.Collections.Generic;
using System.Linq;
using Script.Initializer;
using Script.Libraries.ServiceProvider;
using IServiceProvider = Script.Libraries.ServiceProvider.IServiceProvider;

namespace Script.DataServices
{
public class DataServiceProvider : IServiceProvider, IInitializable
{
    private readonly List<IService> _services;

    public DataServiceProvider(params IService[] services)
    {
        _services = new List<IService>(services);
    }
    
    public T GetService<T>() where T : IService
    {
        foreach (var service in _services.OfType<T>())
        {
            return service;
        }

        throw new Exception($"Can't find {typeof(T)}; Need add this service");
    }
}
}