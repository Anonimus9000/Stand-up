using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public abstract class UISystem : IUISystem
{
    private IDialogsManager _popupsManager;
    private IDialogsManager _fullScreensManager;
    private IDialogsManager _mainUIManager;

    private IUIView _currentWindow;

    public UISystem(
        IInstantiater mainUIInstantiater,
        IInstantiater fullScreenUIInstantiater,
        IInstantiater popupsUIInstantiater,
        List<IUIView> windows)
    {
        InitializeManagers(mainUIInstantiater, fullScreenUIInstantiater, popupsUIInstantiater, windows);
    }

    public virtual UIViewModel Show(UIViewModel viewModel)
    {
        viewModel.InitializeUiManagers(_mainUIManager, _popupsManager, _fullScreensManager);
        viewModel.ShowView();

        return viewModel;
    }

    public virtual void Close(UIViewModel viewModel)
    {
        viewModel.CloseView();
    }

    public void CloseWindowsExceptMain()
    {
        _popupsManager.CloseAll();

        _fullScreensManager.CloseAll();
    }

    public void CloseAllPopups()
    {
        _popupsManager.CloseAll();
    }

    private void InitializeManagers(
        IInstantiater mainUIInstantiater,
        IInstantiater fullScreenUIInstantiater,
        IInstantiater popupsUIInstantiater,
        List<IUIView> uiWindows)
    {
        var popupDialogs = new List<IUIView>(uiWindows.Count);
        var fullScreenDialogs = new List<IUIView>(uiWindows.Count);
        var mainUIs = new List<IUIView>(uiWindows.Count);

        foreach (var uiWindow in uiWindows)
        {
            switch (uiWindow)
            {
                case IPopup:
                    popupDialogs.Add(uiWindow);
                    break;
                case IFullScreen:
                    fullScreenDialogs.Add(uiWindow);
                    break;
                case IMainUI:
                    mainUIs.Add(uiWindow);
                    break;
                default:
                    throw new Exception($"Incorrect type window {uiWindow.GetType()}");
            }
        }

        _popupsManager = new PopupsManager(popupsUIInstantiater, popupDialogs, this);
        _fullScreensManager = new FullScreensManager(fullScreenUIInstantiater, fullScreenDialogs, this);
        _mainUIManager = new MainUIManager(mainUIInstantiater, mainUIs, this);
    }
}
}