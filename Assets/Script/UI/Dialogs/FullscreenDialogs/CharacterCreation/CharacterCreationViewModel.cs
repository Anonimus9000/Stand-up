using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Container.Scenes;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Dialogs.FullscreenDialogs.CharacterCreation.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterCreation
{
public class CharacterCreationViewModel : IUIViewModel
{
    private readonly CharacterCreationModel _model;
    private CharacterCreationView _view;
    private readonly List<GameObject> _characterCreationData;
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly IUIService _fullScreenUIService;
    private CharacterSelector _characterSelector;
    
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
    
    public void ShowView(IUIView view)
    {
        _view = view as CharacterCreationView;
        _view!.Show();
        _sceneSwitcher.SwitchTo<MainMenuScene>();
        
        _view.SetRendererCharacterTexture(_characterSelector.RenderTexture);
        
        SubscribeOnViewEvent(_view);
        SubscribeOnModelEvent(_model);
        SetStartSprite();
    }

    public void ShowHiddenView()
    {
        _view.Show();
        _sceneSwitcher.SwitchTo<MainMenuScene>();
        SubscribeOnViewEvent(_view);
        SubscribeOnModelEvent(_model);
        SetStartSprite();
    }
    

    public void HideView()
    {
        _view.Hide();
        UnsubscribeOnViewEvent(_view);
        UnsubscribeOnModelEvent(_model);
    }

    private void SubscribeOnModelEvent(CharacterCreationModel model)
    {
        model.OnCharacterChanged += CharacterChanged;
        model.OnRightButtonDisabled += RightButtonDisabled;
        model.OnLeftButtonDisabled += LeftButtonDisabled;
        model.OnRightButtonEnabled += RightButtonEnabled;
        model.OnLeftButtonEnabled += LeftButtonEnabled;
    }
    
    private void SubscribeOnViewEvent(CharacterCreationView view)
    {
        view.OnLeftPressed += OnLeftButtonPressed;
        view.OnRightPressed += OnRightButtonPressed;
    }
    
    private void UnsubscribeOnModelEvent(CharacterCreationModel model)
    {
        model.OnCharacterChanged -= CharacterChanged;
        model.OnRightButtonDisabled -= RightButtonDisabled;
        model.OnLeftButtonDisabled -= LeftButtonDisabled;
        model.OnRightButtonEnabled -= RightButtonEnabled;
        model.OnLeftButtonEnabled -= LeftButtonEnabled;
    }
    
    private void UnsubscribeOnViewEvent(CharacterCreationView view)
    {
        view.OnLeftPressed -= OnLeftButtonPressed;
        view.OnRightPressed -= OnRightButtonPressed;
    }

    private void LeftButtonEnabled()
    {
        _view.EnableLeftButton();
    }

    private void RightButtonEnabled()
    {
        _view.EnableRightButton();
    }

    private void LeftButtonDisabled()
    {
        _view.DisableLeftButton();
    }

    private void SetStartSprite()
    {
        _model.SetStartConditions();
    }

    public IInstantiatable GetInstantiatable()
    {
        return _view;
    }

    private void OnRightButtonPressed()
    {
        _model.SetNextSprite();
    }

    private void RightButtonDisabled()
    {
        _view.DisableRightButton();
    }

    private void CharacterChanged(GameObject look)
    {
        _characterSelector.SetCharacter(look);
    }


    private void OnLeftButtonPressed()
    {
        _model.SetPreviousSprite();
    }
    
    
}
}