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

namespace Script.Scenes.Common.InteractableObjects.Toilet
{
public class ToiletViewModel : ViewModel
{
    private ToiletModel _model;
    private ToiletView _view;
    private readonly IUIServiceLocator _iuiServiceLocator;
    private readonly IInteractableObjectsConfig _interactableObjectsFakeConfig;
    private readonly HomeActionProgressHandler _homeActionProgressHandler;
    private readonly ResourceImage _toiletResourceImager = new("ToiletView", "InteractableObjects");
    private readonly Transform _parent;
    private readonly IObserver _observer;
    private readonly Canvas _mainCanvas;
    private readonly InputControls _inputControls;
    private readonly Camera _mainCamera;
    private IObjectClickChecker _clickChecker;

    public ToiletViewModel(IUIServiceLocator iuiServiceLocator,
        IInteractableObjectsConfig interactableObjectsFakeConfig,
        HomeActionProgressHandler homeActionProgressHandler,
        IResourceLoader resourceLoader,
        Transform parent,
        IObserver observer,
        Camera mainCamera,
        InputControls inputControls,
        Canvas mainCanvas)
    {
        _inputControls = inputControls;
        _observer = observer;
        _mainCanvas = mainCanvas;
        _mainCamera = mainCamera;
        _iuiServiceLocator = iuiServiceLocator;
        _parent = parent;
        _interactableObjectsFakeConfig = interactableObjectsFakeConfig;
        _homeActionProgressHandler = homeActionProgressHandler;
        resourceLoader.LoadResource(_toiletResourceImager, OnResourceLoaded);
    }

    private void OnResourceLoaded(GameObject prefab)
    {
        _model = AddDisposable(new ToiletModel());

        _view = AddDisposable(Object.Instantiate(prefab, _parent).GetComponent<ToiletView>());
        _clickChecker = GetInteractableClickChecker(_view.ClickTrackCollider, _mainCamera, _inputControls);
        _view.Initialize(_observer, _mainCanvas, _clickChecker);

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

        var popupsUIService = _iuiServiceLocator.GetService<PopupsUIService>();
        var viewModel = AddDisposable(new ActionsUIViewModel(_iuiServiceLocator,
            _interactableObjectsFakeConfig.ToiletLocationActionData,
            _homeActionProgressHandler, _view.ProgressBarPosition.position));

        popupsUIService.CloseAll();
        popupsUIService.Show<ActionsUIView>(viewModel);
    }

    #endregion
}
}