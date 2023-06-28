using Script.ProjectLibraries.MVVM;
using UnityEngine;

namespace Script.Scenes.MainMenu.UIs.FullScreens.CharacterCreation.Components
{
public class CharacterSelector: BehaviourDisposableBase
{
    [SerializeField]
    private RenderTexture _renderTexture;

    [SerializeField] 
    private Transform _characterPosition;

    [SerializeField] 
    private Transform _characterParent;

    public RenderTexture RenderTexture => _renderTexture;
    
    private GameObject _characterSelectionPrefab;
    private RectTransform _rectTransform;
    private GameObject _selectedCharacter;

    public void SetCharacter(GameObject characterSelectionPrefab)
    {
        DestroySelectedCharacter();
        
        _selectedCharacter = Instantiate(characterSelectionPrefab, _characterParent);
        _selectedCharacter.transform.position = _characterPosition.position;
    }

    private void DestroySelectedCharacter()
    {
        Destroy(_selectedCharacter);
    }
}
}