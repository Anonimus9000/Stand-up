using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.FullscreenDialogs;
using Script.UI.Tests;
using Script.UI.UIInstantiater;
using Script.UI.UiWindowsLoader;
using UnityEngine;

namespace Script.Initializer.Initializables.StartApplicationInitializables
{
    public class UIManagerInitializer : MonoBehaviour
    {
        [SerializeField] private Transform _parentToCreate;
        [SerializeField] private string _pathToDialogs;

        public void Initialize()
        {
            var uiWindows = InitializeWindowsLoader();

            IInstantiater instantiater = new UnityInstantiater(_parentToCreate);
            var uiManager = new UIManager(instantiater, uiWindows);
            
            OpenApplicationEnterDotWindow(uiManager);
        }

        private List<IUIWindow> InitializeWindowsLoader()
        {
            var loaderObject = new GameObject("WindowsLoader");
            loaderObject.transform.SetParent(transform);
            var unityUIWindowsLoader = loaderObject.AddComponent<UnityUIWindowsLoader>();
            
            unityUIWindowsLoader.LoadDialogs(_pathToDialogs);

            return unityUIWindowsLoader.UIWindows;
        }

        private void OpenApplicationEnterDotWindow(UIManager uiManager)
        {
            uiManager.Show<TestScreen1>();
        }
    }
}