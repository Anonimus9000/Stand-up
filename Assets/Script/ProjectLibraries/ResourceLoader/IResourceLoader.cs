using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Script.ProjectLibraries.ResourceLoader
{
public interface IResourceLoader : IDisposable
{
    CancellationToken ResourceLoadToken { get; }
    void LoadResource(string bundleName, Action<GameObject[]> onResourceLoaded);
    Task<GameObject[]> LoadResourceAsync(string bundleName,  CancellationToken cancellationToken);
    void LoadResource(string prefabName, string bundleName, Action<GameObject> onResourceLoaded);
    Task<GameObject> LoadResourceAsync(string prefabName, string bundleName, CancellationToken cancellationToken);
    void LoadResource(ResourceImage resourceImage, Action<GameObject> onResourceLoaded);
    Task<GameObject> LoadResourceAsync(ResourceImage resourceImage, CancellationToken cancellationToken);
}
}