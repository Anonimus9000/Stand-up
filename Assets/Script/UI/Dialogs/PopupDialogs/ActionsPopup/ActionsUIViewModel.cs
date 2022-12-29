using System;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.MainUI.MainHome;
using Script.UI.Dialogs.PopupDialogs.Components;
using Script.UI.Dialogs.PopupDialogs.InteractableObjectsData;
using UnityEngine;

namespace Script.UI.Dialogs.PopupDialogs.ActionsPopup
{
public class ActionsUIViewModel : IUIViewModel
{
    public event Action<IUIViewModel> ViewShown;
    public event Action<IUIViewModel> ViewHidden;
    
    private ActionsUIView _view;
    private readonly ActionsModel _model;
    private readonly IUIService _popupService;
    private readonly MainUIService _mainUiService;
    private readonly ActionData _actionData;
    private ActionFields _actionFields;
    private HomeUIViewModel _homeUIViewModel;
    private readonly ActionProgressHandler _actionProgressHandler;
    private readonly Vector3 _position;

    public ActionsUIViewModel(
        IUIServiceProvider serviceProvider,
        ActionData actionData,
        ActionProgressHandler actionProgressHandler,
        Vector3 position)
    {
        _popupService = serviceProvider.GetService<PopupsUIService>();
        _mainUiService = serviceProvider.GetService<MainUIService>();
        _actionData = actionData;
        _model = new ActionsModel();
        _actionProgressHandler = actionProgressHandler;
        _position = position;
    }

    public void ShowView(IUIView view)
    {
        if (view is not ActionsUIView ActionsUIView)
        {
            throw new Exception($"Incorrect type {view.GetType()}; Need {typeof(ActionsUIView)}");
        }

        _view = ActionsUIView;
        
        _view.Show();
        _view.Init(_actionData.ActionFields, _mainUiService, _actionProgressHandler, _position);

        SubscribeOnViewEvents(_view);

        ViewShown?.Invoke(this);
    }

    public void ShowHiddenView()
    {
        SubscribeOnViewEvents(_view);
        _view.Show();
        
        ViewShown?.Invoke(this);
    }

    public void HideView()
    {
        UnsubscribeInViewEvents(_view);
        
        _view.Hide();
        
        ViewHidden?.Invoke(this);
    }

    public IInstantiatable GetInstantiatable()
    {
        return _view;
    }

    private void SubscribeOnViewEvents(ActionsUIView view)
    {
        view.OnClosePressed += OnCloseButtonPressed;
    }

    private void UnsubscribeInViewEvents(ActionsUIView view)
    {
        view.OnClosePressed -= OnCloseButtonPressed;
    }

    private void OnCloseButtonPressed()
    {
        _popupService.CloseCurrentView();
    }

   
}
}