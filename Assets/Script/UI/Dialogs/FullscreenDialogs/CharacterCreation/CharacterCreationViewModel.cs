using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcherSystem.Container.Scenes;
using Script.UI.Dialogs.FullscreenDialogs.ApplicationEnter;
using Script.UI.Dialogs.FullscreenDialogs.CharacterCreation.Components;
using Script.UI.Dialogs.FullscreenDialogs.StartGameMenu;
using Script.UI.Dialogs.MainUI.MainHome;
using Script.UI.System;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterCreation
{
public class CharacterCreationViewModel:UiViewModelBehaviour
{
    private CharacterCreationModel _model;
    private CharacterCreationView _view;
    private readonly IUISystem _uiSystem;
    private readonly List<Sprite> _characterCreationData;
    


    public CharacterCreationViewModel(IUISystem uiSystem, List<Sprite> characterCreationData)
    {
        _characterCreationData = characterCreationData;
        _uiSystem = uiSystem;
        _model = new CharacterCreationModel(uiSystem, characterCreationData);
    }

    public override void ShowView()
    {
        _view = fullScreensUiManager.Show<CharacterCreationView>(this);
        sceneSwitcher.SwitchTo<MainMenuScene>();
        SubscribeOnViewEvent(_view);
        SubscribeOnModelEvent(_model);
        SetStartSprite();
    }

    private void SubscribeOnModelEvent(CharacterCreationModel model)
    {
        model.OnSpriteChanged += SpriteChanged;
        model.OnRightButtonDisable += RightButtonDisabled;
        model.OnLeftButtonDisable += LeftButtonDisabled;
        model.OnRightButtonEnable += RightButtonEnabled;
        model.OnLeftButtonEnable += LeftButtonEnabled;
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

    public override void CloseView()
    {
        fullScreensUiManager.TryCloseCurrent();
    }

    public override IInstantiatable GetInstantiatable()
    {
        return _view;
    }

    private void SubscribeOnViewEvent(CharacterCreationView view)
    {
        view.OnLeftPressed+= OnLeftButtonPressed;
        view.OnRightPressed += OnRightButtonPressed;
    }

    private void OnRightButtonPressed()
    {
        _model.SetNextSprite();
    }

    private void RightButtonDisabled()
    {
        _view.DisableRightButton();
    }

    private void SpriteChanged(Sprite look)
    {
        _view.SetCharacterLook(look);
    }


    private void OnLeftButtonPressed()
    {
        _model.SetPreviousSprite();
    }
    
    
}
}