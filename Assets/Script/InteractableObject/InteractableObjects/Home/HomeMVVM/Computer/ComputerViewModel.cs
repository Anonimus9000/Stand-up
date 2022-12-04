using System;
using Script.DataServices.Base;
using Script.Libraries.MVVM;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Computer
{
public class ComputerViewModel : IViewModel
{
    public ComputerModel Model;

    public ComputerView View; 

    public ComputerViewModel(IView view, IDataService playerCharacteristicsService)
    {
        if (view is not ComputerView computerView)
        {
            throw new Exception($"Need dependency of type {typeof(ComputerView)}");
        }

        View = computerView;
        View.Initialize(this);
        Model = new ComputerModel();

        SubscribeOnViewEvents();
    }

    private void SubscribeOnViewEvents()
    {
        View.ObjectClicked += OnViewObjectClicked;
    }

    private void OnViewObjectClicked()
    {
        Debug.Log($"{View.gameObject.name} was clicked");
    }
}
}