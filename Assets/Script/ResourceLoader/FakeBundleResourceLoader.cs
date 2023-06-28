using System;
using System.Threading;
using System.Threading.Tasks;
using Script.ProjectLibraries.ResourceLoader;
using UnityEngine;

namespace Script.ResourceLoader
{
public class FakeBundleResourceLoader : IResourceLoader
{
    public CancellationToken ResourceLoadToken { get; }

    private readonly FakeResourceBundleContainer _resourceContainer;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public void LoadResource(string bundleName, Action<GameObject[]> onResourceLoaded)
    {
        var bundle = _resourceContainer.GetBundle(bundleName);
        onResourceLoaded?.Invoke(bundle);
    }

    public Task<GameObject[]> LoadResourceAsync(string bundleName, CancellationToken cancellationToken)
    {
        var gameObjects = _resourceContainer.GetBundle(bundleName);
        return Task.FromResult(gameObjects);
    }

    public FakeBundleResourceLoader(FakeResourceBundleContainer resourceContainer)
    {
        _cancellationTokenSource = new CancellationTokenSource();
        ResourceLoadToken = _cancellationTokenSource.Token;
        _resourceContainer = resourceContainer;
    }
    
    public void LoadResource(string prefabName, string bundleName, Action<GameObject> onResourceLoaded)
    {
        var prefab = _resourceContainer.GetPrefab(prefabName, bundleName);
        onResourceLoaded?.Invoke(prefab);
    }

    public Task<GameObject> LoadResourceAsync(
        string prefabName, 
        string bundleName, 
        CancellationToken cancellationToken)
    {
        var prefab = _resourceContainer.GetPrefab(prefabName, bundleName);
        return Task.FromResult(prefab);
    }

    public void LoadResource(ResourceImage resourceImage, Action<GameObject> onResourceLoaded)
    {
        LoadResource(resourceImage.BundleName, resourceImage.PrefabName, onResourceLoaded);
    }

    public async Task<GameObject> LoadResourceAsync(ResourceImage resourceImage, CancellationToken token)
    {
        return await LoadResourceAsync(resourceImage.BundleName, resourceImage.PrefabName, token);
    }

    public void Dispose()
    {
        _cancellationTokenSource?.Cancel();
    }
}
}