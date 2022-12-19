using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public abstract class UIServiceProvider : IUIServiceProvider
{
    protected readonly List<IUIService> services = new();

    public UIServiceProvider(
        IInstantiater mainUIInstantiater,
        IInstantiater fullScreenUIInstantiater,
        IInstantiater popupsUIInstantiater,
        List<IUIView> windows)
    {
        InitializeManagers(mainUIInstantiater, fullScreenUIInstantiater, popupsUIInstantiater, windows);
    }

    public T GetService<T>() where T : IUIService
    {
        foreach (var dialogsManager in services)
        {
            if (dialogsManager is T manager)
            {
                return manager;
            }
        }

        throw new Exception($"Can't contains {typeof(T)} service");
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

        var popupsUIService = new PopupsUIService(popupsUIInstantiater, popupDialogs);
        var fullScreensUIService = new FullScreensUIService(fullScreenUIInstantiater, fullScreenDialogs, popupsUIService);
        var mainUIService = new MainUIService(mainUIInstantiater, mainUIs, popupsUIService, fullScreensUIService);

        services.Add(popupsUIService);
        services.Add(fullScreensUIService);
        services.Add(mainUIService);
    }
}
}