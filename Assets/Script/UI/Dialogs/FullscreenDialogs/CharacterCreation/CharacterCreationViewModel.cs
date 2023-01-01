using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Container.Scenes;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Dialogs.FullscreenDialogs.CharacterCreation.Components;
using Script.UI.Dialogs.PopupDialogs.ActionsPopup;
using UnityEngine;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterCreation
{
public class CharacterCreationViewModel : IUIViewModel
{
    private readonly CharacterCreationModel _model;
    private CharacterCreationView _view;
    private readonly List<GameObject> _characterCreationData;
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly IUIService _fullScreenUIService;
    private readonly CharacterSelector _characterSelector;
    private IAnimatorService _animatorService;

    public event Action<IUIViewModel> ViewShown;
    public event Action<IUIViewModel> ViewHidden;

    public CharacterCreationViewModel(
        ISceneSwitcher sceneSwitcher, 
        IUIService fullScreenUIService,
        List<GameObject> characterCreationData,
        CharacterSelector characterSelector)
    {
        _sceneSwitcher = sceneSwitcher;
        _characterCreationData = characterCreationData;
        _model = new CharacterCreationModel(characterCreationData);
        _fullScreenUIService = fullScreenUIService;
        _characterSelector = characterSelector;
    }

    public void Init(IUIView view, IAnimatorService animatorService)
    {
        if (view is not CharacterCreationView actionsUIView)
        {
            throw new Exception($"Incorrect type {view.GetType()}; Need {typeof(ActionsUIView)}");
        }
        
        _view = actionsUIView;
        _animatorService = animatorService;
        
        _sceneSwitcher.SwitchTo<MainMenuScene>();
        
        _view.SetRendererCharacterTexture(_characterSelector.RenderTexture);
        SetInitialCharacter();
        
        SubscribeOnModelEvents(_model);
        SubscribeOnViewEvents(_view);
        SubscribeOnAnimatorEvents(_animatorService);
    }

    public void Deinit()
    {
        UnsubscribeOnAnimatorEvents(_animatorService);
        UnsubscribeOnModelEvent(_model);
        UnsubscribeOnViewEvent(_view);
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
    
    private void SetInitialCharacter()
    {
        _model.ShowFirstCharacter();
    }

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
    

    #region ModelEvents

    private void SubscribeOnModelEvents(CharacterCreationModel model)
    {
        model.OnCharacterChanged += OnCharacterChanged;
        model.OnRightButtonDisabled += OnRightButtonDisabled;
        model.OnLeftButtonDisabled += OnLeftButtonDisabled;
        model.OnRightButtonEnabled += OnRightButtonEnabled;
        model.OnLeftButtonEnabled += OnLeftButtonEnabled;
    }
    
    private void UnsubscribeOnModelEvent(CharacterCreationModel model)
    {
        model.OnCharacterChanged -= OnCharacterChanged;
        model.OnRightButtonDisabled -= OnRightButtonDisabled;
        model.OnLeftButtonDisabled -= OnLeftButtonDisabled;
        model.OnRightButtonEnabled -= OnRightButtonEnabled;
        model.OnLeftButtonEnabled -= OnLeftButtonEnabled;
    }
    
    private void OnRightButtonDisabled()
    {
        _view.DisableRightButton();
    }

    private void OnCharacterChanged(GameObject look)
    {
        _characterSelector.SetCharacter(look);
    }
    
    private void OnLeftButtonEnabled()
    {
        _view.EnableLeftButton();
    }

    private void OnRightButtonEnabled()
    {
        _view.EnableRightButton();
    }

    private void OnLeftButtonDisabled()
    {
        _view.DisableLeftButton();
    }


    #endregion

    #region ViewEvents

    private void SubscribeOnViewEvents(CharacterCreationView view)
    {
        view.OnLeftPressed += OnLeftButtonPressed;
        view.OnRightPressed += OnRightButtonPressed;
    }

    private void UnsubscribeOnViewEvent(CharacterCreationView view)
    {
        view.OnLeftPressed -= OnLeftButtonPressed;
        view.OnRightPressed -= OnRightButtonPressed;
    }
    
    private void OnRightButtonPressed()
    {
        _model.ShowNextCharacter();
    }

    private void OnLeftButtonPressed()
    {
        _model.ShowPreviousCharacter();
    }

    #endregion
}
}