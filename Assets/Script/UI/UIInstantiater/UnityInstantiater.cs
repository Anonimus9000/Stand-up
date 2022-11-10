using System;
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
        
        public IInstantiatble Instantiate(IInstantiatble objectToCreate)
        {
            if (objectToCreate is Object obj)
            {
                return Object.Instantiate(obj, _parentToCreate) as IInstantiatble;
            }

            throw new Exception("Need daughter of Object class");
        }

        public void Destroy(IInstantiatble objectToDestroy)
        {
            if (objectToDestroy is Component obj)
            {
                Object.Destroy(obj.gameObject);
            }
        }
    }
}