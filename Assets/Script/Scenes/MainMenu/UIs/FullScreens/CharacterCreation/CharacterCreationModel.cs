using System;
using System.Collections.Generic;
using Script.ProjectLibraries.MVVM;
using UnityEngine;

namespace Script.Scenes.MainMenu.UIs.FullScreens.CharacterCreation
{
public class CharacterCreationModel : Model
{
    public event Action<GameObject> OnCharacterChanged;
    public event Action OnRightButtonDisabled;
    public event Action OnLeftButtonDisabled;
    public event Action OnLeftButtonEnabled;
    public event Action OnRightButtonEnabled;

    private readonly List<GameObject> _characterList;
    private int _currentSpriteIndex = 0;

    public CharacterCreationModel(List<GameObject> characterList)
    {
        _characterList = characterList;
    }

    public void ShowFirstCharacter()
    {
        OnCharacterChanged?.Invoke(_characterList[_currentSpriteIndex]);
        OnLeftButtonDisabled?.Invoke();
    }

    public void ShowNextCharacter()
    {
        if (_characterList.Count == 0)
        {
            OnLeftButtonDisabled?.Invoke();
            OnRightButtonDisabled?.Invoke();
            
            return;
        }

        if (_currentSpriteIndex == 0)
        {
            OnLeftButtonEnabled?.Invoke();
        }

        _currentSpriteIndex++;

        if (_currentSpriteIndex == _characterList.Count - 1)
        {
            OnRightButtonDisabled?.Invoke();
        }
        
        var nextCharacter = _characterList[_currentSpriteIndex];
        OnCharacterChanged?.Invoke(nextCharacter);
    }

    public void ShowPreviousCharacter()
    {
        if (_characterList.Count == 0)
        {
            OnLeftButtonDisabled?.Invoke();
            OnRightButtonDisabled?.Invoke();
            
            return;
        }

        if (_currentSpriteIndex == _characterList.Count - 1)
        {
            OnRightButtonEnabled?.Invoke();
        }

        _currentSpriteIndex--;

        if (_currentSpriteIndex == 0)
        {
            OnLeftButtonDisabled?.Invoke();
        }
        
        var nextCharacter = _characterList[_currentSpriteIndex];
        OnCharacterChanged?.Invoke(nextCharacter);
    }
}
}