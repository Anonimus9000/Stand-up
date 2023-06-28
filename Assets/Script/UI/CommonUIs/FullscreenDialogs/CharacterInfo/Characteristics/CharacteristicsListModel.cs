using System;
using System.Collections.Generic;
using Script.DataServices.Base;
using Script.DataServices.Services.PlayerDataService;
using Script.ProjectLibraries.MVVM;
using UnityEngine;

namespace Script.UI.CommonUIs.FullscreenDialogs.CharacterInfo.Characteristics
{
public class CharacteristicsListModel : IModel
{
    private readonly List<CharacteristicElementViewModel> _characteristicElementViewModels;

    public CharacteristicsListModel(
        IDataService dataService,
        CharacteristicElementView prefab,
        Transform elementsParent)
    {
        _characteristicElementViewModels = new List<CharacteristicElementViewModel>(6);
        
        InitCharacteristics(dataService.DataContainer, prefab, elementsParent);
    }

    public void DeinitElements()
    {
        foreach (var elementViewModel in _characteristicElementViewModels)
        {
            elementViewModel.Deinit();
        }
    }

    private void InitCharacteristics(
        IDataContainer dataContainer,
        CharacteristicElementView prefab,
        Transform elementsParent)
    {
        if (dataContainer is not PlayerProfileContainer playerDataContainer)
        {
            throw new Exception("Incorrect type");
        }

        var charisma = playerDataContainer.Charisma;
        _characteristicElementViewModels.Add(new CharacteristicElementViewModel(charisma, prefab, elementsParent));

        var appearance = playerDataContainer.Appearance;
        _characteristicElementViewModels.Add(new CharacteristicElementViewModel(appearance, prefab, elementsParent));

        var erudition = playerDataContainer.Erudition;
        _characteristicElementViewModels.Add(new CharacteristicElementViewModel(erudition, prefab, elementsParent));

        var insight = playerDataContainer.Insight;
        _characteristicElementViewModels.Add(new CharacteristicElementViewModel(insight, prefab, elementsParent));

        var senceOfHumor = playerDataContainer.SenceOfHumor;
        _characteristicElementViewModels.Add(new CharacteristicElementViewModel(senceOfHumor, prefab, elementsParent));
        
        foreach (var elementViewModel in _characteristicElementViewModels)
        {
            elementViewModel.Init();
        }
    }
}
}