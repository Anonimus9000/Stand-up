using System;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.PopupDialogs.Components;
using Script.UI.Dialogs.PopupDialogs.InteractableObjectsData;
using UnityEngine;

namespace Script.UI.Dialogs.PopupDialogs.ActionsPopup
{
public class ActionsIuiViewModel : IUIViewModel
{
    public event Action<IUIViewModel> ViewShown;
    public event Action<IUIViewModel> ViewHidden;
    
    private ActionsUIView _view;
    private readonly ActionsModel _model;
    private readonly IUIService _popupUIService;
    private readonly ActionData _actionData;
    private ActionFieldsSetter _actionFieldsSetter;

    public ActionsIuiViewModel(IUIService popupUIService, ActionData actionData)
    {
        _popupUIService = popupUIService;
        _actionData = actionData;
        _model = new ActionsModel();
    }

    public void ShowView(IUIView view)
    {
        if (view is not ActionsUIView ActionsUIView)
        {
            throw new Exception($"Incorrect type {view.GetType()}; Need {typeof(ActionsUIView)}");
        }

        _view = ActionsUIView;
        
        _view.Show();
        _view.Init(_actionData.ActionFields);

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
        _popupUIService.CloseCurrentView();
    }
}
}