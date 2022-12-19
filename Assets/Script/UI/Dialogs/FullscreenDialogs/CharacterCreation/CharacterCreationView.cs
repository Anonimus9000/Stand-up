using System;
using System.Collections.Generic;
using Script.UI.Dialogs.BaseDialogs;
using Script.UI.Dialogs.FullscreenDialogs.CharacterCreation.Components;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterCreation
{
public class CharacterCreationView: MonoUiViewFullscreen
{
    
    [SerializeField]
    private TextMeshProUGUI _nameField;

    [SerializeField] private Image _characterLook;

    [SerializeField]
    private Button _rightArrow;

    [SerializeField] private Button _leftArrow;

    public event Action OnRightPressed;

    public event Action OnLeftPressed;
    
    public override void OnShown()
    {
        base.OnShown();

        _leftArrow.onClick.AddListener(LeftArrowButton);
        _rightArrow.onClick.AddListener(RightArrowButton);
    }

    public override void OnHidden()
    {
        base.OnHidden();

        _leftArrow.onClick.RemoveListener(LeftArrowButton);
        _rightArrow.onClick.RemoveListener(RightArrowButton);
    }

    public void SetCharacterLook(Sprite look)
    {
        _characterLook.sprite = look;
    }

    public void DisableRightButton()
    {
        _rightArrow.interactable = false;
    }
    
    public void EnableRightButton()
    {
        _rightArrow.interactable = true;
    }

    public void DisableLeftButton()
    {
        _leftArrow.interactable = false;
    }
    
    public void EnableLeftButton()
    {
        _leftArrow.interactable = true;
    }

    private void RightArrowButton()
    {
        OnRightPressed?.Invoke();
    }

    private void LeftArrowButton()
    {
        OnLeftPressed?.Invoke();
    }
}
}