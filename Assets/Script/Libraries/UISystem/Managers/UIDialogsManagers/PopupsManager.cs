using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
    public class PopupsManager : IDialogsManager
    {
        private PopupDialog _currentPopup;
        private List<PopupDialog> _popupPrefabs;
        private string _popupPrefabsPath;
        private IInstantiater _instantiater;

        public void Initialize(string popupPrefabsPath, IInstantiater instantiater)
        {
            _instantiater = instantiater;
            _popupPrefabsPath = popupPrefabsPath;
            
            InitializePrefabs(_popupPrefabsPath);
        }

        public BaseUIWindow Show<T>() where T : BaseUIWindow
        {
            var popupToShow = GetPrefab<T>();

            TryCloseCurrentPopup();

            var popupDialog = _instantiater.Instantiate(popupToShow) as PopupDialog;
            
            popupDialog!.OnShown();

            return popupDialog;
        }

        public BaseUIWindow Hide<T>() where T : BaseUIWindow
        {
            throw new NotImplementedException();
        }

        public void Close<T>() where T : BaseUIWindow
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

        private PopupDialog GetPrefab<T>() where T : BaseUIWindow
        {
            foreach (var popupPrefab in _popupPrefabs)
            {
                if (popupPrefab is T)
                {
                    return popupPrefab;
                }
            }

            throw new Exception($"Can't find prefab {typeof(T)}. Need add prefab in {_popupPrefabsPath}");
        }

        private void ClosePrefab(PopupDialog popupToClose)
        {
            popupToClose.OnHidden();
            _instantiater.Destroy(popupToClose);
        }

        private void InitializePrefabs(string prefabsPath)
        {
            var popupDialogs = Resources.LoadAll<PopupDialog>(prefabsPath);
            _popupPrefabs = new List<PopupDialog>(popupDialogs);
        }
    }
}