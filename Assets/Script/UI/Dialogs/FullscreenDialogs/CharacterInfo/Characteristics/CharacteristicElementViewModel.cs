using Script.DataServices.Services.PlayerDataService;
using Script.Libraries.MVVM;
using TMPro;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterInfo.Characteristics
{
public class CharacteristicElementViewModel : IViewModel
{
    private readonly CharacteristicElementModel _model;
    private CharacteristicElementView _view;

    public CharacteristicElementViewModel(
        CharacteristicModel characteristicModel,
        CharacteristicElementView viewPrefab,
        Transform parent)
    {
        CreateElement(viewPrefab, parent);

        _model = new CharacteristicElementModel(characteristicModel);
    }

    public void Init()
    {
        SubscribeOnModelEvents(_model);
        InitData();
    }

    public void Deinit()
    {
        UnsubscribeOnModelEvents(_model);

    }

    #region Model
    
    private void InitData()
    {
        _view.Level.text = _model.Level.ToString();
        _view.Icon.texture = _model.Icon;
        _view.CharacteristicName.text = _model.Name;
        
        _model.UpdateProgress();
    }

    private void SubscribeOnModelEvents(CharacteristicElementModel model)
    {
        model.IconChanged += OnIconChanged;
        model.NameChanged += OnNameChanged;
        model.LevelChanged += OnLevelChanged;
        model.PercentProgressChanged += OnPercentToNextLevelChanged;
        
    }

    private void UnsubscribeOnModelEvents(CharacteristicElementModel model)
    {
        model.IconChanged -= OnIconChanged;
        model.NameChanged -= OnNameChanged;
        model.LevelChanged -= OnLevelChanged;
        model.PercentProgressChanged -= OnPercentToNextLevelChanged;
    }

    private void OnPercentToNextLevelChanged(int percent)
    {
        _view.Progress.value = percent / 100f;
    }

    private void OnNameChanged(string name)
    {
        _view.CharacteristicName.text = name;
    }

    private void OnLevelChanged(int level)
    {
        _view.Level.text = level.ToString();
    }

    private void OnIconChanged(Texture texture)
    {
        _view.Icon.texture = texture;
    }

    #endregion

    private void CreateElement(CharacteristicElementView viewPrefab, Transform parent)
    {
        _view = Object.Instantiate(viewPrefab, parent);
    }
}
}