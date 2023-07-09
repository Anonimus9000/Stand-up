using System;
using System.Collections.Generic;
using Script.ProjectLibraries.Logger.LoggerBase;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;

namespace Script.ProjectLibraries.UISystem.Managers.UiServiceProvider
{
public class UIServiceProvider : IUIServiceProvider
{
    protected readonly List<IUIService> services = new();
    private readonly ILogger _logger;

    public UIServiceProvider(
        IInstantiater mainUIInstantiater,
        IInstantiater fullScreenUIInstantiater,
        IInstantiater popupsUIInstantiater,
        List<IUIView> windows, 
        IAnimatorServiceProvider animatorServiceProvider,
        ILogger logger)
    {
        _logger = logger;
        
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

    public void Show<T>(IUIViewModel viewModel) where T : IUIView
    {
        var popupService = GetService<PopupsUIService>();
        var fullscreenService = GetService<FullScreensUIService>();
        var mainUIService = GetService<MainUIService>();
        
        switch (viewModel.UIType)
        {
            case UIType.None:
                _logger.LogError($"Need set ui type in {nameof(viewModel)}");
                break;
            case UIType.Popup:
                popupService.Show<T>(viewModel);
                break;
            case UIType.Fullscreen:
                fullscreenService.Show<T>(viewModel);
                break;
            case UIType.Main:
                mainUIService.Show<T>(viewModel);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void CloseCurrentView(UIType uiType)
    {
        var popupService = GetService<PopupsUIService>();
        var fullscreenService = GetService<FullScreensUIService>();
        var mainUIService = GetService<MainUIService>();
        
        switch (uiType)
        {
            case UIType.None:
                _logger.LogError($"Can't close ui, because uiType is {UIType.None}. Need initialize type in uiViewModel");
                break;
            case UIType.Popup:
                popupService.CloseCurrentView();
                break;
            case UIType.Fullscreen:
                fullscreenService.CloseCurrentView();
                break;
            case UIType.Main:
                mainUIService.CloseCurrentView();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(uiType), uiType, null);
        }
    }

    public void CloseAll(UIType uiType)
    {
        var popupService = GetService<PopupsUIService>();
        var fullscreenService = GetService<FullScreensUIService>();
        var mainUIService = GetService<MainUIService>();
     
        switch (uiType)
        {
            case UIType.None:
                _logger.LogError($"Can't close ui, because uiType is {UIType.None}. Need initialize type in uiViewModel");
                break;
            case UIType.Popup:
                popupService.CloseAll();
                break;
            case UIType.Fullscreen:
                fullscreenService.CloseAll();
                break;
            case UIType.Main:
                mainUIService.CloseAll();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(uiType), uiType, null);
        }
    }

    public IUIViewModel GetCurrentUI(UIType uiType)
    {
        var popupService = GetService<PopupsUIService>();
        var fullscreenService = GetService<FullScreensUIService>();
        var mainUIService = GetService<MainUIService>();
        
        switch (uiType)
        {
            case UIType.None:
                _logger.LogError($"Incorrect type: {UIType.None}");
                break;
            case UIType.Popup:
                return popupService.CurrentUI;
            case UIType.Fullscreen:
                return fullscreenService.CurrentUI;
            case UIType.Main:
                return mainUIService.CurrentUI;
            default:
                throw new ArgumentOutOfRangeException(nameof(uiType), uiType, null);
        }

        return null;
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