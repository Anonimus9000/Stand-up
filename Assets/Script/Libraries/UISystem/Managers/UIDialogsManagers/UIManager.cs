using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine.SocialPlatforms.Impl;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public class UIManager : IUIManager
{
    private readonly IDialogsManager _popupsManager = new PopupsManager();
    private readonly IDialogsManager _fullScreensManager = new FullScreensManager();
    private readonly IDialogsManager _mainUIManager = new MainUIManager();

    private IUIWindow _currentWindow;

    public UIManager(IInstantiater instantiater, List<IUIWindow> windows)
    {
        InitializeManagers(instantiater, windows);
    }

    public IUIWindow Show<T>() where T : IUIWindow, new()
    {
        // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
        switch (new T())
        {
            case IPopup _:
                return _popupsManager.Show<T>();
            case IFullScreen _:
                return _fullScreensManager.Show<T>();
            case IMainUI _:
                return _mainUIManager.Show<T>();
            default:
                throw new Exception("Incorrect type: " + typeof(T));
        }
    }

    public void Close<T>() where T : IUIWindow, new()
    {
        // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
        switch (new T())
        {
            case IPopup _:
                _popupsManager.Close<T>();
                break;
            case IFullScreen _:
                _fullScreensManager.Close<T>();
                break;
            case IMainUI _:
                _mainUIManager.Close<T>();
                break;
            default:
                throw new Exception("Incorrect type: " + typeof(T));
        }
    }

    public void CloseWindowsExceptMain()
    {
        _popupsManager.Close<IPopup>();
        _fullScreensManager.Close<IFullScreen>();
    }

    private void InitializeManagers(IInstantiater instantiater, List<IUIWindow> uiWindows)
    {
        var popupDialogs = new List<IUIWindow>(uiWindows.Count);
        var fullScreenDialogs = new List<IUIWindow>(uiWindows.Count);
        var mainUIs = new List<IUIWindow>(uiWindows.Count);

        foreach (var uiWindow in uiWindows)
        {
            switch (uiWindow)
            {
                case IPopup popupDialog:
                    popupDialogs.Add(popupDialog);
                    break;
                case IFullScreen fullScreenDialog:
                    fullScreenDialogs.Add(fullScreenDialog);
                    break;
                case IMainUI mainUI:
                    mainUIs.Add(mainUI);
                    break;
                default:
                    throw new Exception($"Incorrect type window {uiWindow.GetType()}");
            }
        }

        _popupsManager.Initialize(instantiater, popupDialogs, this);
        _fullScreensManager.Initialize(instantiater, fullScreenDialogs, this);
        _mainUIManager.Initialize(instantiater, mainUIs, this );
    }
}
}