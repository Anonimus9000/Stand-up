using Script.Initializer.Base;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcherSystem.Container;
using Script.SceneSwitcherSystem.Switcher;
using UnityEngine;

namespace Script.Initializer.MainInitializers
{
public class GameEntryPointInitializer : MonoBehaviour, IMainInitializer
{
    #region MonoBehavioursDependencies

    [SerializeField]
    private UIManagerDependenciesInitializer _uiManagerInitializer;

    [SerializeField]
    private SceneContainer _sceneContainer;

    #endregion

    private void OnEnable()
    {
        InitializeElements();
    }

    public void InitializeElements()
    {
        var uiManager = InitializeUISystem();
        var sceneSwitcher = InitializeSceneSwitcher(uiManager);
    }

    private IUIManager InitializeUISystem()
    {
        return (IUIManager)_uiManagerInitializer.Initialize();
    }

    private ISceneSwitcher InitializeSceneSwitcher(IUIManager uiManager)
    {
        var sceneSwitcher = new SceneSwitcherDependenciesInitializer(uiManager, _sceneContainer);
        return (ISceneSwitcher)sceneSwitcher.Initialize();
    }
}
}