using Script.Initializer.Initializables.StartApplicationInitializables;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcher.Switcher;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Initializer.Initializers
{
public class StartApplicationInitializer : MonoBehaviour
{
    [SerializeField]
    private UIManagerInitializer _uiManagerInitializer;

    [SerializeField]
    private LocationSwitcherInitializer _locationSwitcherInitializer;

    private IUIManager _uiManager;
    private ISceneSwitcher _sceneSwitcher;

    private void Awake()
    {
        InitializeElements();
    }

    public void InitializeElements()
    {
        InitializeUISystem();
        InitializeSceneSwitcher();
    }

    private void InitializeUISystem()
    {
        _uiManager = (UIManager)_uiManagerInitializer.InitializeElements();
    }

    private void InitializeSceneSwitcher()
    {
        _sceneSwitcher = (ISceneSwitcher)_locationSwitcherInitializer.InitializeElements();
    }
}
}