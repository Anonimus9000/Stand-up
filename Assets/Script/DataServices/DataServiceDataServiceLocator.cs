using System;
using System.Collections.Generic;
using System.Linq;
using Script.ProjectLibraries.ServiceLocators;

namespace Script.DataServices
{
//TODO: replace data service on config
public class DataServiceDataServiceLocator : IDataServiceLocator
{
    private readonly List<IService> _services;

    public DataServiceDataServiceLocator(params IService[] services)
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