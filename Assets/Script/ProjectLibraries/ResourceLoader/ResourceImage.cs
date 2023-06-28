namespace Script.ProjectLibraries.ResourceLoader
{
public struct ResourceImage
{
    public string BundleName { get; }
    public string PrefabName { get; }

    public ResourceImage(string prefabName, string bundleName)
    {
        PrefabName = prefabName;
        BundleName = bundleName;
    }
}
}