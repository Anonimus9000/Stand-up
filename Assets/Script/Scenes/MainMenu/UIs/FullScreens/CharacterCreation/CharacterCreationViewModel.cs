using System;
using System.Collections.Generic;
using Script.ProjectLibraries.ResourceLoader;
using Script.ProjectLibraries.SceneSwitcherSystem;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;
using Script.Scenes.Home.UIs.Popups.ActionsPopup;
using Script.Scenes.MainMenu.UIs.FullScreens.CharacterCreation.Components;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script.Scenes.MainMenu.UIs.FullScreens.CharacterCreation
{
public class CharacterCreationViewModel : UIViewModel
{
    public override event Action<IUIViewModel> ViewShown;
    public override event Action<IUIViewModel> ViewHidden;
    
    private readonly CharacterCreationModel _model;
    private CharacterCreationView _view;
    private readonly List<GameObject> _characterCreationData;
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly IUIService _fullScreenUIService;
    private IAnimatorService _animatorService;
    private readonly IResourceLoader _resourceLoader;
    private readonly ResourceImage _characterSelectorResourceImage = new("CharacterSelector", "CommonPrefabs");
    private CharacterSelector _characterSelector;

    public CharacterCreationViewModel(
        IResourceLoader resourceLoader,
        ISceneSwitcher sceneSwitcher, 
        IUIService fullScreenUIService,
        List<GameObject> characterCreationData)
    {
        _resourceLoader = resourceLoader;
        _sceneSwitcher = sceneSwitcher;
        _characterCreationData = characterCreationData;
        _model = new CharacterCreationModel(characterCreationData);
        _fullScreenUIService = fullScreenUIService;
    }

    public override void Init(IUIView view, IAnimatorService animatorService)
    {
        if (view is not CharacterCreationView actionsUIView)
        {
            throw new Exception($"Incorrect type {view.GetType()}; Need {typeof(ActionsUIView)}");
        }
        
        _view = AddDisposable(actionsUIView);
        _animatorService = animatorService;
        
        _sceneSwitcher.SwitchTo<MainMenuScene>();

        _resourceLoader.LoadResource(_characterSelectorResourceImage, OnResourceLoaded);
    }

    private void OnResourceLoaded(GameObject prefab)
    {
        _characterSelector =
            AddDisposable(Object.Instantiate(prefab).GetComponent<CharacterSelector>());

        _view.SetRendererCharacterTexture(_characterSelector.RenderTexture);
        SetInitialCharacter();
        
        SubscribeOnModelEvents(_model);
        SubscribeOnViewEvents(_view);
        SubscribeOnAnimatorEvents(_animatorService);
    }

    public override void Deinit()
    {
        UnsubscribeOnAnimatorEvents(_animatorService);
        UnsubscribeOnModelEvent(_model);
        UnsubscribeOnViewEvent(_view);
        Dispose();
    }

    public override void ShowView()
    {
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
        view.CloseButtonPressed += OnCloseButtonPressed;
    }

    private void UnsubscribeOnViewEvent(CharacterCreationView view)
    {
        view.OnLeftPressed -= OnLeftButtonPressed;
        view.OnRightPressed -= OnRightButtonPressed;
        view.OnRightPressed -= OnCloseButtonPressed;
    }

    private void OnCloseButtonPressed()
    {
        _fullScreenUIService.CloseCurrentView();
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