using System;
using Script.ProjectLibraries.UISystem.UIWindow;
using Script.UI.CommonUIs.BaseBehaviour;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Scenes.MainMenu.UIs.FullScreens.CharacterCreation
{
public class CharacterCreationView: UiViewBehaviour, IFullScreen
{
    [SerializeField]
    private TextMeshProUGUI _nameField;

    [SerializeField] 
    private RawImage _characterLook;

    [SerializeField]
    private Button _rightArrow;

    [SerializeField] 
    private Button _leftArrow;

    [SerializeField] 
    private Button _closeButton;

    public event Action OnRightPressed;
    public event Action OnLeftPressed;
    public event Action CloseButtonPressed;
    
    
    public override void OnShown()
    {
        _leftArrow.onClick.AddListener(LeftArrowButton);
        _rightArrow.onClick.AddListener(RightArrowButton);
        _closeButton.onClick.AddListener(OnCloseButtonPressed);
    }

    public override void OnHidden()
    {
        _leftArrow.onClick.RemoveListener(LeftArrowButton);
        _rightArrow.onClick.RemoveListener(RightArrowButton);
    }

    public void SetRendererCharacterTexture(Texture texture)
    {
        _characterLook.texture = texture;
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

    private void OnCloseButtonPressed()
    {
        CloseButtonPressed?.Invoke();
    }
}
}