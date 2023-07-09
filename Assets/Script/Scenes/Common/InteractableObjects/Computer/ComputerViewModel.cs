using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.InputChecker.Base;
using Script.ProjectLibraries.InputChecker.MouseKeyboard;
using Script.ProjectLibraries.InputChecker.TouchScreen;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.Observer.Base;
using Script.ProjectLibraries.ResourceLoader;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.Scenes.Home.ActionProgressSystem.Handler;
using Script.Scenes.Home.UIs.Popups.ActionsPopup;
using UnityEngine;

namespace Script.Scenes.Common.InteractableObjects.Computer
{
public class ComputerViewModel : ViewModel
{
    private ComputerModel _model;
    private ComputerView _view;
    private readonly IUIServiceProvider _iuiServiceProvider;
    private readonly IInteractableObjectsConfig _interactableObjectsFakeConfig;
    private readonly HomeActionProgressHandler _homeActionProgressHandler;
    private readonly ResourceImage _computerResourceImager = new("ComputerView", "InteractableObjects");
    private IObjectClickChecker _objectClickChecker;
    private readonly IObserver _observer;
    private readonly Canvas _canvas;
    private readonly Transform _parent;
    private readonly Camera _mainCamera;
    private readonly InputControls _inputControls;

    public ComputerViewModel(
        IUIServiceProvider iuiServiceProvider,
        IInteractableObjectsConfig interactableObjectsFakeConfig,
        HomeActionProgressHandler homeActionProgressHandler,
        IObserver observer, 
        Canvas canvas,
        IResourceLoader resourceLoader,
        Transform parent,
        Camera mainCamera,
        InputControls inputControls)
    {
        _observer = observer;
        _mainCamera = mainCamera;
        _inputControls = inputControls;
        _canvas = canvas;
        _parent = parent;
        _iuiServiceProvider = iuiServiceProvider;
        _interactableObjectsFakeConfig = interactableObjectsFakeConfig;
        _homeActionProgressHandler = homeActionProgressHandler;
        
        resourceLoader.LoadResource(_computerResourceImager, OnResourceLoaded);
    }

    private void OnResourceLoaded(GameObject prefab)
    {
        _view = AddDisposable(Object.Instantiate(prefab, _parent).GetComponent<ComputerView>());

        _objectClickChecker = GetInteractableClickChecker(_view.ClickTrackCollider, _mainCamera, _inputControls);

        _model = AddDisposable(new ComputerModel());
        
        _view.Initialize(_observer, _canvas, _objectClickChecker);
        
        SubscribeOnViewEvents();
        SubscribeOnModelEvents();

        _model.InputActive = true;
    }
    
    private IObjectClickChecker GetInteractableClickChecker(
        Collider clickAreaCollider, 
        Camera mainCamera,
        InputControls inputControls)
    {
        if (Application.isEditor)
        {
            return new MouseLeftClickChecker(mainCamera, inputControls.MouseKeyboard.LeftButtonMousePress,
                clickAreaCollider);
        }
        else
        {
            return new TouchClickChecker(mainCamera, inputControls.TochScreen.SingleTouch, clickAreaCollider);
        }
    }

    #region ModelEvents

    private void SubscribeOnModelEvents()
    {
        _model.InputActiveChanged += OnInputActiveChanged;
    }

    private void OnInputActiveChanged(bool isActive)
    {
        _view.ChangeClickInputActive(isActive);
    }

    #endregion

    #region ViewEvents

    private void SubscribeOnViewEvents()
    {
        _view.ObjectClicked += OnViewObjectClicked;
    }

    private void OnViewObjectClicked()
    {
        Debug.Log($"{_view.gameObject.name} was clicked");

        var viewModel = AddDisposable(new ActionsUIViewModel(
            _iuiServiceProvider,
            _interactableObjectsFakeConfig.ComputerLocationActionData,
            _homeActionProgressHandler,
            _view.ProgressBarTransform.position));
        
        _iuiServiceProvider.Show<ActionsUIView>(viewModel);
    }

    #endregion
}
}