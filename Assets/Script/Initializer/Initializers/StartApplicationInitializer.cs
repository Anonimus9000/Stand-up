using Script.Initializer.Initializables.StartApplicationInitializables;
using UnityEngine;

namespace Script.Initializer.Initializers
{
public class StartApplicationInitializer : MonoBehaviour, IInitializer
{
    [SerializeReference] private UIManagerInitializer _uiManagerInitializer;

    private void Awake()
    {
        InitializeElements();
    }

    public void InitializeElements()
    {
        InitializeUISystem();
    }

    private void InitializeUISystem()
    {
        _uiManagerInitializer.Initialize();
    }
}
}