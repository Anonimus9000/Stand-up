using System;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;

namespace Script.UI.CommonUIs.PopupDialogs.InGameEvent
{
public class InGameEventViewModel : IUIViewModel
{
    public event Action<IUIViewModel> ViewShown;
    public event Action<IUIViewModel> ViewHidden;

    public event Action EventCompleted;
    
    private InGameEventView _view;
    private IAnimatorService _animatorService;
    private readonly IUIService _popupsUIService;

    public InGameEventViewModel(IUIService popupsUIService)
    {
        _popupsUIService = popupsUIService;
    }

    public void Init(IUIView view, IAnimatorService animatorService)
    {
        _view = (InGameEventView)view;
        _animatorService = animatorService;
        
        SubscribeOnViewEvents(_view);
        SubscribeOnAnimatorEvents(_animatorService);
    }

    public void Deinit()
    {
        UnsubscribeOnViewEvents(_view);
        UnsubscribeOnAnimatorEvents(_animatorService);
    }

    public void ShowView()
    {
        _animatorService.StartShowAnimation(_view);
    }

    public void ShowHiddenView()
    {
        _animatorService.StartShowAnimation(_view);
    }

    public void HideView()
    {
        _animatorService.StartHideAnimation(_view);
        
        EventCompleted?.Invoke();
    }

    public IInstantiatable GetInstantiatable()
    {
        return _view;
    }
    
        
    #region ViewEvents

    private void SubscribeOnViewEvents(InGameEventView view)
    {
        view.AcceptButton.onClick.AddListener(OnAcceptButtonClicked);
        view.RejectButton.onClick.AddListener(OnButtonRejectClicked);
        view.CloseButton.onClick.AddListener(OnButtonCloseClicked);
    }

    private void UnsubscribeOnViewEvents(InGameEventView view)
    {
        view.AcceptButton.onClick.RemoveListener(OnAcceptButtonClicked);
        view.RejectButton.onClick.RemoveListener(OnButtonRejectClicked);
        view.CloseButton.onClick.RemoveListener(OnButtonCloseClicked);
    }

    private void OnButtonCloseClicked()
    {
        _popupsUIService.CloseCurrentView();
    }

    private void OnButtonRejectClicked()
    {
        _popupsUIService.CloseCurrentView();
    }

    private void OnAcceptButtonClicked()
    {
        _popupsUIService.CloseCurrentView();
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