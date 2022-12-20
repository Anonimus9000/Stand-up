using UnityEngine;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterCreation.Components
{
public class CharacterSelector: MonoBehaviour
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

    public void DestroySelectedCharacter()
    {
        Destroy(_selectedCharacter);
    }

}
}