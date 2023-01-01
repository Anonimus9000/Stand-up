using System;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterInfo
{
public class CharacterInfoViewModel : IUIViewModel
{
    public event Action<IUIViewModel> ViewShown;
    public event Action<IUIViewModel> ViewHidden;
    
    private CharacterInfoView _view;
    private readonly CharacterInfoModel _model;
    private readonly IUIService _fullScreenService;
    private IAnimatorService _animatorService;

    public CharacterInfoViewModel(IUIService fullScreensUIService)
    {
        _fullScreenService = fullScreensUIService;
        _model = new CharacterInfoModel();
    }

    public void Init(IUIView view, IAnimatorService animatorService)
    {
        _view = view as CharacterInfoView;
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
    }

    public IInstantiatable GetInstantiatable()
    {
        return _view;
    }

    #region ViewEvents

    private void SubscribeOnViewEvents(CharacterInfoView view)
    {
        view.CloseButtonPressed += OnCloseButtonPressed;
    }

    private void UnsubscribeOnViewEvents(CharacterInfoView view)
    {
        view.CloseButtonPressed -= OnCloseButtonPressed;
    }

    private void OnCloseButtonPressed()
    {
        _fullScreenService.CloseCurrentView();
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