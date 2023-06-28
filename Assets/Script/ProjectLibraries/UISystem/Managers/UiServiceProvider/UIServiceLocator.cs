using System;
using System.Collections.Generic;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.ProjectLibraries.UISystem.UIWindow;

namespace Script.ProjectLibraries.UISystem.Managers.UiServiceProvider
{
public abstract class UIServiceLocator : Base.ServiceProvider.IUIServiceLocator
{
    protected readonly List<IUIService> services = new();

    public UIServiceLocator(
        IInstantiater mainUIInstantiater,
        IInstantiater fullScreenUIInstantiater,
        IInstantiater popupsUIInstantiater,
        List<IUIView> windows, 
        IAnimatorServiceProvider animatorServiceProvider)
    {
        InitializeServices(mainUIInstantiater, fullScreenUIInstantiater, popupsUIInstantiater, windows, animatorServiceProvider);
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

    private void InitializeServices(
        IInstantiater mainUIInstantiater,
        IInstantiater fullScreenUIInstantiater,
        IInstantiater popupsUIInstantiater,
        List<IUIView> uiWindows,
        IAnimatorServiceProvider animatorServiceProvider)
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

        var popupAnimatorService = animatorServiceProvider.GetService<IPopupAnimatorService>();
        var fullScreenAnimatorService = animatorServiceProvider.GetService<IFullScreenAnimatorService>();
        var mainUiAnimatorService = animatorServiceProvider.GetService<IMainUiAnimatorService>();

        var popupsUIService = new PopupsUIService(popupsUIInstantiater, popupDialogs, popupAnimatorService);
        
        var fullScreensUIService = new FullScreensUIService(fullScreenUIInstantiater,
            fullScreenDialogs,
            popupsUIService,
            fullScreenAnimatorService);
        
        var mainUIService = new MainUIService(mainUIInstantiater,
            mainUIs,
            popupsUIService,
            fullScreensUIService,
            mainUiAnimatorService);

        services.Add(popupsUIService);
        services.Add(fullScreensUIService);
        services.Add(mainUIService);
    }
}
}