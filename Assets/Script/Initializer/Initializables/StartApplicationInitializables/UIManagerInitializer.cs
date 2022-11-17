using System.Collections.Generic;
using Script.Libraries.InGameEventSystem;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Tests;
using Script.UI.UIInstantiater;
using Script.UI.UiWindowsLoader;
using UnityEngine;

namespace Script.Initializer.Initializables.StartApplicationInitializables
{
public class UIManagerInitializer : MonoBehaviour, IInitializable
{
    [SerializeField] private Transform _parentToCreate;
    [SerializeField] private string _pathToDialogs;

    public void Initialize(InGameEventsObserver inGameEventsObserver)
    {
        var uiManager = InitializeUIManager();

        OpenApplicationEnterDotWindow(uiManager);
    }

    private UIManager InitializeUIManager()
    {
        var uiWindows = InitializeWindowsLoader();

        IInstantiater instantiater = new UnityInstantiater(_parentToCreate);
        
        return new UIManager(instantiater, uiWindows);
    }
    

    private List<IUIWindow> InitializeWindowsLoader()
    {
        var loaderObject = new GameObject("WindowsLoader");
        loaderObject.transform.SetParent(transform);
        var unityUIWindowsLoader = loaderObject.AddComponent<UnityUIWindowsLoader>();

        unityUIWindowsLoader.LoadDialogs(_pathToDialogs);

        return unityUIWindowsLoader.UIWindows;
    }

    private void OpenApplicationEnterDotWindow(IUIManager uiManager)
    {
        uiManager.Show<TestScreen1>();
    }
}
}