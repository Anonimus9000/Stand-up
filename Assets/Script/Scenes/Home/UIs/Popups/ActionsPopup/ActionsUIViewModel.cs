using System;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.LocationActionsData;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;
using Script.Scenes.Home.ActionProgressSystem.Handler;
using Script.Scenes.Home.UIs.MainUIs.MainHome;
using Script.Scenes.Home.UIs.Popups.ActionsPopup.Components;
using UnityEngine;

namespace Script.Scenes.Home.UIs.Popups.ActionsPopup
{
public class ActionsUIViewModel : UIViewModel
{
    public override event Action<IUIViewModel> ViewShown;
    public override event Action<IUIViewModel> ViewHidden;
    
    private ActionsUIView _view;
    private readonly ActionsModel _model;
    private readonly IUIService _popupService;
    private readonly IUIService _mainUiService;
    private readonly LocationActionData _locationActionData;
    private ActionFieldItemView _actionFieldItemView;
    private HomeUIViewModel _homeUIViewModel;
    private readonly HomeActionProgressHandler _homeActionProgressHandler;
    private readonly Vector3 _progressBarPosition;
    private IAnimatorService _animatorService;

    public ActionsUIViewModel(
        IUIServiceLocator serviceLocator,
        LocationActionData locationActionData,
        HomeActionProgressHandler homeActionProgressHandler,
        Vector3 progressBarPosition)
    {
        _popupService = serviceLocator.GetService<PopupsUIService>();
        _mainUiService = serviceLocator.GetService<MainUIService>();
        _locationActionData = locationActionData;
        _model = AddDisposable(new ActionsModel());
        _homeActionProgressHandler = homeActionProgressHandler;
        _progressBarPosition = progressBarPosition;
    }

    public override void Init(IUIView view, IAnimatorService animatorService)
    {
        if (view is not ActionsUIView actionsUIView)
        {
            throw new Exception($"Incorrect type {view.GetType()}; Need {typeof(ActionsUIView)}");
        }

        _view = actionsUIView;
        _animatorService = animatorService;
        
        SubscribeOnViewEvents(_view);
        SubscribeOnAnimatorEvents(_animatorService);
    }

    public override void Deinit()
    {
        UnsubscribeInViewEvents(_view);
        UnsubscribeOnAnimatorEvents(_animatorService);
    }

    public override void ShowView()
    {
        _view.Init(_locationActionData.ActionFields, _mainUiService, _popupService, _homeActionProgressHandler, _progressBarPosition);
        
        _animatorService.StartShowAnimation(_view);
    }

    public override void ShowHiddenView()
    {
        _animatorService.StartShowAnimation(_view);
    }

    public override void HideView()
    {
        _animatorService.StartHideAnimation(_view);
    }

    public override IInstantiatable GetInstantiatable()
    {
        return _view;
    }

    #region ViewEvents

    private void SubscribeOnViewEvents(ActionsUIView view)
    {
        view.ClosePressed += CloseButtonPressed;
    }

    private void UnsubscribeInViewEvents(ActionsUIView view)
    {
        view.ClosePressed -= CloseButtonPressed;
    }
    
    private void CloseButtonPressed()
    {
        _popupService.CloseCurrentView();
    }

    #endregion

    #region AnimatorEvents

    private void SubscribeOnAnimatorEvents(IAnimatorService animatorService)
    {
        animatorService.ShowCompleted += OnShowAnimationCompleted;
        animatorService.HideCompleted += OnHideAnimationCompleted;
    }

    private void UnsubscribeOnAnimatorEvents(IAnimatorService animatorService)
    {
        animatorService.ShowCompleted -= OnShowAnimationCompleted;
        animatorService.HideCompleted -= OnHideAnimationCompleted;
    }

    private void OnShowAnimationCompleted()
    {
        _view.OnShown();
        ViewShown?.Invoke(this);
    }

    private void OnHideAnimationCompleted()
    {
        _view.OnHidden();
        ViewHidden?.Invoke(this);
    }

    #endregion

}
}