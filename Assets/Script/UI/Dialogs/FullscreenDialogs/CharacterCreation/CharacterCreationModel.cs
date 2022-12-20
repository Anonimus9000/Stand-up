using System;
using System.Collections.Generic;
using Script.Libraries.MVVM;
using UnityEngine;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterCreation
{
public class CharacterCreationModel:IModel
{
    public event Action<GameObject> OnCharacterChanged;
    public event Action OnRightButtonDisabled; 
    public event Action OnLeftButtonDisabled; 
    public event Action OnLeftButtonEnabled; 
    public event Action OnRightButtonEnabled; 
    
    private readonly List<GameObject> _characterList;
    private int _currentSpriteIndex;
    
    public CharacterCreationModel(List<GameObject> characterList)
    {
        _characterList = characterList;
    }

    public void SetStartConditions()
    {
        OnCharacterChanged?.Invoke(_characterList[0]);
        OnLeftButtonDisabled?.Invoke();
    }

    public void SetNextSprite()
    {
        for (int i = 0; i < _characterList.Count; i++)
        {
            if (i == _currentSpriteIndex)
            {
                OnLeftButtonEnabled?.Invoke();
                OnCharacterChanged?.Invoke(_characterList[i+1]);
                _currentSpriteIndex = i + 1;
                if (_currentSpriteIndex == 3)
                {
                    OnRightButtonDisabled?.Invoke();
                }
                return;
            }
        }
    }

    public void SetPreviousSprite()
    {
        for (int i = 0; i < _characterList.Count; i++)
        {
            if (i == _currentSpriteIndex)
            {
                OnRightButtonEnabled?.Invoke();
                OnCharacterChanged?.Invoke(_characterList[i-1]);
                _currentSpriteIndex = i - 1;
                if (_currentSpriteIndex == 0)
                {
                    OnLeftButtonDisabled?.Invoke();
                }
                return;
            }
        }
    }
}
}