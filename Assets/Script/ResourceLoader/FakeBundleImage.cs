using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.ResourceLoader
{
[Serializable]
public struct FakeBundleImage
{
    [field: SerializeField]
    public string BundleName { get; private set; }
    
    [field: SerializeField]
    public GameObject[] Prefabs { get; private set; }

    public GameObject GetPrefab(string prefabName)
    {
        foreach (var prefab in Prefabs)
        {
            if (prefab.name == prefabName)
            {
                return prefab;
            }
        }
        
        Debug.LogError($"Can't find prefab by name {prefabName}; " +
                       $"Need add prefab by name{prefabName} in inspector {nameof(FakeResourceBundleContainer)} " +
                       $"or prefab name was changed");

        return null;
    }
}
}