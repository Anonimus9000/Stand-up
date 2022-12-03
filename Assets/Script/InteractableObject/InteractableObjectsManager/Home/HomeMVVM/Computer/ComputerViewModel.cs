using Script.Libraries.MVVM;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjectsManager.Home.HomeMVVM.Computer
{
public class ComputerViewModel : IViewModel
{
    public ComputerModel Model;

    public ComputerView View; 

    public ComputerViewModel(IView view)
    {
        Model = new ComputerModel();
        View = view as ComputerView;
        
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