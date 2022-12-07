using System;
using System.Collections.Generic;
using System.Linq;
using Script.Initializer;
using Script.InteractableObject.InteractableObjects.Container.Base;
using Script.Libraries.MVVM;

namespace Script.InteractableObject.InteractableObjects.Container.Containers
{
public class HomeInteractableObjectsManager : IInteractableViewModelsContainer, IInitializable
{
    private readonly List<IViewModel> _viewModels;
    public HomeInteractableObjectsManager(params IViewModel[] viewModels)
    {
        _viewModels = new List<IViewModel>(viewModels);
    }

    public IViewModel GetViewModel<T>() where T : IViewModel
    {
        foreach (var viewModel in _viewModels.OfType<T>())
        {
            return viewModel;
        }

        throw new Exception($"Can't find {typeof(T)} in container");
    }

    private void ActivateInputForAllObjects()
    {
        foreach (var viewModel in _viewModels)
        {
            
        }
    }
}
}