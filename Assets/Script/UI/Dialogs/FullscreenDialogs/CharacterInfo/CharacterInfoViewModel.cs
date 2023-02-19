using System;
using System.Collections.Generic;
using Script.DataServices.Base;
using Script.DataServices.Services.PlayerDataService;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.FullscreenDialogs.CharacterInfo.Characteristics;
using UnityEngine;

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
    private List<CharacteristicElementViewModel> _characteristics;
    private readonly IDataService _playerDataService;
    private CharacteristicsListViewModel _characteristicsListViewModel;

    public CharacterInfoViewModel(IUIService fullScreensUIService, IDataService playerDataService)
    {
        _playerDataService = playerDataService;
        _fullScreenService = fullScreensUIService;
        _model = new CharacterInfoModel(playerDataService as PlayerDataService);
    }

    public void Init(IUIView view, IAnimatorService animatorService)
    {
        _view = view as CharacterInfoView;
        _animatorService = animatorService;
        _characteristicsListViewModel = new CharacteristicsListViewModel(_view!.CharacteristicsListView, _playerDataService);

        SubscribeOnModelEvents();
        SubscribeOnViewEvents(_view);
        SubscribeOnAnimatorEvents(_animatorService);
        
        InitCharacterView();
    }

    public void Deinit()
    {
        UnsubscribeOnModelEvents();
        UnsubscribeOnViewEvents(_view);
        UnsubscribeOnAnimatorEvents(_animatorService);
        _characteristicsListViewModel.Deinit();
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

    #region ModelEvents

    private void InitCharacterView()
    {
        _view.Avatar.texture = _model.PlayerAvatar;
        _view.Name.text = _model.PlayerName;
    }
    
    private void SubscribeOnModelEvents()
    {
        _model.AvatarChanged += OnAvatarChanged;
        _model.PlayerNameChanged += OnNameChanged;
    }

    private void UnsubscribeOnModelEvents()
    {
        _model.AvatarChanged -= OnAvatarChanged;
        _model.PlayerNameChanged -= OnNameChanged;
    }

    private void OnNameChanged(string plaName)
    {
        _view.Name.text = plaName;
    }

    private void OnAvatarChanged(Texture avatarTexture)
    {
        _view.Avatar.texture = avatarTexture;
    }

    #endregion

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