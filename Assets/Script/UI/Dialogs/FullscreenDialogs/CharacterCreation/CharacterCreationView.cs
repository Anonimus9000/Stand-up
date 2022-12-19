using System;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.BaseBehaviour;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterCreation
{
public class CharacterCreationView: UiViewBehaviour, IFullScreen
{
    [SerializeField]
    private TextMeshProUGUI _nameField;

    [SerializeField] private Image _characterLook;

    [SerializeField]
    private Button _rightArrow;

    [SerializeField] 
    private Button _leftArrow;
    
    public override event Action ViewShown;
    public override event Action ViewHidden;

    public event Action OnRightPressed;

    public event Action OnLeftPressed;
    
    public override void Show()
    {
        _leftArrow.onClick.AddListener(LeftArrowButton);
        _rightArrow.onClick.AddListener(RightArrowButton);
    }

    public override void Hide()
    {
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