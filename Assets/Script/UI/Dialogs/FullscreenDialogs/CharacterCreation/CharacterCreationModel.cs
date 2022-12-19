using System;
using System.Collections.Generic;
using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using UnityEngine;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterCreation
{
public class CharacterCreationModel:IModel
{
    private readonly List<Sprite> _characterList;
    private int _currentSpriteIndex;

    public event Action<Sprite> OnSpriteChanged;

    public event Action OnRightButtonDisabled; 
    public event Action OnLeftButtonDisabled; 
    public event Action OnLeftButtonEnabled; 
    public event Action OnRightButtonEnabled; 
    public CharacterCreationModel(List<Sprite> CharacterList)
    {
        _characterList = CharacterList;
    }

    public void SetStartConditions()
    {
        OnSpriteChanged?.Invoke(_characterList[0]);
        OnLeftButtonDisabled?.Invoke();
    }

    public void SetNextSprite()
    {
        for (int i = 0; i < _characterList.Count; i++)
        {
            if (i == _characterList.Count - 1)
            {
                OnRightButtonDisabled?.Invoke();
                return;
            }

            if (i == _currentSpriteIndex)
            {
                OnLeftButtonEnabled?.Invoke();
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
                OnLeftButtonDisabled?.Invoke();
                return;
            }
            
            if (i == _currentSpriteIndex)
            {
                OnRightButtonEnabled?.Invoke();
                OnSpriteChanged?.Invoke(_characterList[i-1]);
                _currentSpriteIndex = i - 1;
                return;
            }
        }
    }
}
}