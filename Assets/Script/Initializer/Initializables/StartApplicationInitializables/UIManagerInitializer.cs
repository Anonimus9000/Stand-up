using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.UI.Dialogs.FullscreenDialogs;
using UnityEngine;

namespace Script.Initializer.Initializables.StartApplicationInitializables
{
    public class UIManagerInitializer : MonoBehaviour
    {
        [SerializeField] private Transform _parentToCreate;
        [SerializeField] private string _pathToDialogs;

        public void Initialize()
        {
            IInstantiater instantiater = new UnityInstantiater(_parentToCreate);

            var uiManager = new UIManager(_pathToDialogs, instantiater);
            
            OpenApplicationEnterDotWindow(uiManager);
        }

        private void OpenApplicationEnterDotWindow(UIManager uiManager)
        {
            uiManager.Show<ApplicationEnterWindow>();
        }
    }
}