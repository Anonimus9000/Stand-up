using System;
using Script.ConfigData.LocationActionsConfig;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.InteractableObject.ActionProgressSystem;
using Script.InteractableObject.ActionProgressSystem.Handler;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.Libraries.UISystem.Managers.UiServiceProvider;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.MainUI.MainHome;
using Script.UI.Dialogs.PopupDialogs.ActionsPopup.Components;
using UnityEngine;

namespace Script.UI.Dialogs.PopupDialogs.ActionsPopup
{
public class ActionsUIViewModel : IUIViewModel
{
    public event Action<IUIViewModel> ViewShown;
    public event Action<IUIViewModel> ViewHidden;
    
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
        IUIServiceProvider serviceProvider,
        LocationActionData locationActionData,
        HomeActionProgressHandler homeActionProgressHandler,
        Vector3 progressBarPosition)
    {
        _popupService = serviceProvider.GetService<PopupsUIService>();
        _mainUiService = serviceProvider.GetService<MainUIService>();
        _locationActionData = locationActionData;
        _model = new ActionsModel();
        _homeActionProgressHandler = homeActionProgressHandler;
        _progressBarPosition = progressBarPosition;
    }

    public void Init(IUIView view, IAnimatorService animatorService)
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

    public void Deinit()
    {
        UnsubscribeInViewEvents(_view);
        UnsubscribeOnAnimatorEvents(_animatorService);
    }

    public void ShowView()
    {
        _view.Init(_locationActionData.ActionFields, _mainUiService, _popupService, _homeActionProgressHandler, _progressBarPosition);
        
        _animatorService.StartShowAnimation(_view);
    }

    public void ShowHiddenView()
    {
        _animatorService.StartShowAnimation(_view);
    }

    public void HideView()
    {
        _animatorService.StartHideAnimation(_view);
    }

    public IInstantiatable GetInstantiatable()
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