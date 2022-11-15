﻿using System;
using System.Collections.Generic;
using System.Linq;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
    public class PopupsManager : IDialogsManager
    {
        private List<IUIWindow> _popupPrefabs;
        private IInstantiater _instantiater;
        private IPopupDialog _currentPopup;

        private readonly List<IPopupDialog> _queueHiddenPopups = new List<IPopupDialog>();
        private UIManager _uiManager;

        public void Initialize(IInstantiater instantiater, List<IUIWindow> windows, UIManager uiManager)
        {
            _uiManager = uiManager;
            
            _instantiater = instantiater;

            _popupPrefabs = windows;
        }

        public IUIWindow Show<T>() where T : IUIWindow
        {
            TryHideCurrentPopup();

            var popupToShow = GetPrefab<T>();

            var popupDialog = _instantiater.Instantiate(popupToShow) as IPopupDialog;
            
            popupDialog!.OnShown();
            popupDialog!.Initialize(_uiManager);
            _queueHiddenPopups.Add(popupDialog);

            return popupDialog;
        }

        private bool TryHideCurrentPopup()
        {
            if (_currentPopup is null)
            {
                return false;
            }
            
            _instantiater.SetActive(_currentPopup, false);
            
            return false;
        }

        public void Close<T>() where T : IUIWindow
        {
            var popupDialog = _queueHiddenPopups.Last();
            _queueHiddenPopups.Remove(popupDialog);
            _instantiater.Destroy(popupDialog);

            TryShowLastHiddenPopup();
        }

        private bool TryShowLastHiddenPopup()
        {
            if (_queueHiddenPopups.Count > 0)
            {
                var popupDialog = _queueHiddenPopups.Last();
                _instantiater.SetActive(popupDialog, true);
                
                return true;
            }

            return false;
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
    }
}