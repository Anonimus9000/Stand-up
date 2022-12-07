﻿using System;
using System.Collections.Generic;
using System.Linq;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine.Android;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public class PopupsManager : IDialogsManager
{
    private List<IUIWindow> _popupPrefabs;
    private IInstantiater _instantiater;
    private IPopup _currentPopup;

    private readonly List<IPopup> _queueHiddenPopups = new List<IPopup>();
    private IUIManager _uiManager;

    public void Initialize(IInstantiater instantiater, List<IUIWindow> windows, IUIManager uiManager)
    {
        _uiManager = uiManager;

        _instantiater = instantiater;

        _popupPrefabs = windows;
    }

    public T Show<T>() where T : IUIWindow
    {
        TryHideCurrentPopup();

        var popupToShow = GetPrefab<T>();

        var popupDialog = _instantiater.Instantiate(popupToShow) as IPopup;

        _currentPopup = popupDialog;

        popupDialog!.OnShown();
        popupDialog!.SetUiManager(_uiManager);
        _queueHiddenPopups.Add(popupDialog);

        return (T) popupDialog;
    }

    private bool TryHideCurrentPopup()
    {
        if (_currentPopup is null)
        {
            return false;
        }

        _instantiater.SetActive(_currentPopup, false);

        return true;
    }

    public void Close<T>() where T : IUIWindow
    {
        if (_queueHiddenPopups.Count == 0)
        {
           return;
        }
        var popupDialog = _queueHiddenPopups.Last();
        _queueHiddenPopups.Remove(popupDialog);
        _instantiater.Destroy(popupDialog);

        if (_queueHiddenPopups.Count == 0)
        {
            _currentPopup = null;
        }

        TryShowLastHiddenPopup();
    }

    private bool TryShowLastHiddenPopup()
    {
       //TODO: govno 
        if (_queueHiddenPopups.Count > 0)
        {
            var popupDialog = _queueHiddenPopups.Last();
            _instantiater.SetActive(popupDialog, true);
            _currentPopup = popupDialog;

            return true;
        }

        return false;
    }

    private IPopup GetPrefab<T>() where T : IUIWindow
    {
        foreach (var popupPrefab in _popupPrefabs)
        {
            if (popupPrefab is T)
            {
                return popupPrefab as IPopup;
            }
        }

        throw new Exception($"Can't find prefab {typeof(T)}");
    }
}
}