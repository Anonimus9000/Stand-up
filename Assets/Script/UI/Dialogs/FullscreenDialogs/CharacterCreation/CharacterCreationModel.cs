using System;
using System.Collections.Generic;
using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.UI.Dialogs.FullscreenDialogs.CharacterCreation.Components;
using UnityEngine;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterCreation
{
public class CharacterCreationModel:IModel
{
    private readonly List<Sprite> _characterList;
    private readonly IUISystem _uiSystem;
    private int _currentSpriteIndex;

    public event Action<Sprite> OnSpriteChanged;

    public event Action OnRightButtonDisable; 
    public event Action OnLeftButtonDisable; 
    public event Action OnLeftButtonEnable; 
    public event Action OnRightButtonEnable; 
    public CharacterCreationModel(IUISystem uiSystem, List<Sprite> CharacterList)
    {
        _characterList = CharacterList;
        _uiSystem = uiSystem;
    }

    public void SetStartConditions()
    {
        OnSpriteChanged?.Invoke(_characterList[0]);
        OnLeftButtonDisable?.Invoke();
    }

    public void SetNextSprite()
    {
        for (int i = 0; i < _characterList.Count; i++)
        {
            if (i == _characterList.Count - 1)
            {
                OnRightButtonDisable?.Invoke();
                return;
            }

            if (i == _currentSpriteIndex)
            {
                OnLeftButtonEnable?.Invoke();
                OnSpriteChanged?.Invoke(_characterList[i+1]);
                _currentSpriteIndex = i + 1;
                return;
            }

           
        }
        
    }

    public void SetPreviousSprite()
    {
        for (int i = 0; i < _characterList.Count; i++)
        {
            if (_currentSpriteIndex == 0)
            {
                OnLeftButtonDisable?.Invoke();
                return;
            }
            
            if (i == _currentSpriteIndex)
            {
                OnRightButtonEnable?.Invoke();
                OnSpriteChanged?.Invoke(_characterList[i-1]);
                _currentSpriteIndex = i - 1;
                return;
            }
        }
        
    }
    
}
}