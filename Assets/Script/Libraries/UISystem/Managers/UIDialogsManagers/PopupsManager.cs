using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
    public class PopupsManager : IDialogsManager
    {
        private IPopupDialog _currentPopup;
        private List<IUIWindow> _popupPrefabs;
        private IInstantiater _instantiater;

        public void Initialize(IInstantiater instantiater, List<IUIWindow> windows)
        {
            _instantiater = instantiater;

            _popupPrefabs = windows;
        }

        public IUIWindow Show<T>() where T : IUIWindow
        {
            var popupToShow = GetPrefab<T>();

            TryCloseCurrentPopup();

            var popupDialog = _instantiater.Instantiate(popupToShow) as IPopupDialog;
            
            popupDialog!.OnShown();

            return popupDialog;
        }

        public IUIWindow Hide<T>() where T : IUIWindow
        {
            throw new NotImplementedException();
        }

        public void Close<T>() where T : IUIWindow
        {
            var popupDialog = GetPrefab<T>();
            _instantiater.Destroy(popupDialog);
        }

        private bool TryCloseCurrentPopup()
        {
            if (_currentPopup == null) return false;
            
            ClosePrefab(_currentPopup);

            return true;
        }

        private IPopupDialog GetPrefab<T>() where T : IUIWindow
        {
            foreach (var popupPrefab in _popupPrefabs)
            {
                if (popupPrefab is T)
                {
                    return popupPrefab as IPopupDialog;
                }
            }

            throw new Exception($"Can't find prefab {typeof(T)}");
        }

        private void ClosePrefab(IPopupDialog popupToClose)
        {
            popupToClose.OnHidden();
            _instantiater.Destroy(popupToClose);
        }
    }
}