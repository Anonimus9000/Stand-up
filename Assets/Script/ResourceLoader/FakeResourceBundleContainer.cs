using System.Collections.Generic;
using Script.ProjectLibraries.ResourceLoader;
using UnityEngine;

namespace Script.ResourceLoader
{
[CreateAssetMenu(fileName = "FakeResourceLoader", menuName = "ScriptableObjects/FakeResourceLoader",
    order = 5)]
public class FakeResourceBundleContainer : ScriptableObject
{
    [SerializeField]
    private List<FakeBundleImage> _bundles;

    public GameObject[] GetBundle(string bundleName)
    {
        foreach (var fakeBundleImage in _bundles)
        {
            if (fakeBundleImage.BundleName == bundleName)
            {
                return fakeBundleImage.Prefabs;
            }
        }
        
        Debug.LogError($"Can't find bundle name: {bundleName}; " +
                       $"Need add bundle by name {bundleName} in inspector {nameof(FakeResourceBundleContainer)} " +
                       $"or bundle name was changed");

        return null;
    }
    
    public GameObject GetPrefab(ResourceImage resourceImage)
    {
        return GetPrefab(resourceImage.BundleName, resourceImage.PrefabName);
    }
    
    public GameObject GetPrefab(string bundleName, string prefabName)
    {
        foreach (var fakeBundleImage in _bundles)
        {
            if (fakeBundleImage.BundleName == bundleName)
            {
                return fakeBundleImage.GetPrefab(prefabName);
            }
        }
        
        Debug.LogError($"Can't find bundle name: {bundleName}; " +
                       $"Need add bundle by name {bundleName} in inspector {nameof(FakeResourceBundleContainer)} " +
                       $"or bundle name was changed");

        return null;
    }
}
}