using System.Collections.Generic;
using Script.Initializer;
using Script.Initializer.Base;
using Script.InteractableObject.Base;
using Script.Libraries.MVVM;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjectsManager.Managers
{
public class HomeInteractableObjectInitializer : MonoBehaviour, IDependenciesInitializer
{
    [SerializeField]
    private List<InteractableView> _views;

    public List<IViewModel> InteractableViewModels { get; }

    public IInitializable Initialize()
    {
        InitializeViewModels();

        return null;
    }

    private void InitializeViewModels()
    {
        foreach (var interactableView in _views)
        {
            var model = new InteractableModel();
            var viewModel = new InteractableViewModel(model, interactableView);
            
            InteractableViewModels.Add(viewModel);
        }
    }
}
}