using System;
using Script.Initializer;
using Script.Libraries.UISystem.Managers.Instantiater;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script.UI.UIInstantiater
{
public class UnityInstantiater : IInstantiater
{
    private readonly Transform _parentToCreate;

    public UnityInstantiater(Transform parentToCreate)
    {
        _parentToCreate = parentToCreate;
    }

    public void Initialize()
    {
    }

    public IInstantiatable Instantiate(IInstantiatable objectToCreate)
    {
        if (objectToCreate is Object obj)
        {
            return Object.Instantiate(obj, _parentToCreate) as IInstantiatable;
        }

        throw new Exception("Need daughter of Object class");
    }

    public void Destroy(IInstantiatable objectToDestroy)
    {
        if (objectToDestroy is Component obj)
        {
            Object.Destroy(obj.gameObject);
        }
    }

    public void SetActive(IInstantiatable objectToHide, bool isActive)
    {
        if (isActive)
        {
            Activate(objectToHide);
        }
        else
        {
            Deactivate(objectToHide);
        }
    }

    private void Activate(IInstantiatable objectToHide)
    {
        if (objectToHide is Component obj)
        {
            obj.gameObject.SetActive(true);
        }
    }

    private void Deactivate(IInstantiatable objectToHide)
    {
        if (objectToHide is Component obj)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
}