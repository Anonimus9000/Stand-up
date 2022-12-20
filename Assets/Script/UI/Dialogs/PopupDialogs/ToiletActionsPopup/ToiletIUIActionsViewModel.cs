﻿using System;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.UI.Dialogs.PopupDialogs.ToiletActionsPopup
{
public class ToiletIUIActionsViewModel : IUIViewModel
{
    public event Action<IUIViewModel> ViewShown;
    public event Action<IUIViewModel> ViewHidden;
    
    private ToiletUIActionsView _view;
    private readonly ToiletUIActionsModel _model;
    private readonly PopupsUIService _popupService;

    public ToiletIUIActionsViewModel(PopupsUIService popupsUIService)
    {
        _popupService = popupsUIService;
        _model = new ToiletUIActionsModel();
    }

    public void ShowView(IUIView view)
    {
        _view = (ToiletUIActionsView)view;
        
        SubscribeOnViewEvent(_view);

        _view.Show(); 
        ViewShown?.Invoke(this);
    }

    public void ShowHiddenView()
    {
        SubscribeOnViewEvent(_view);
        
        _view.Show();

        ViewShown?.Invoke(this);
    }

    public void HideView()
    {
        UnsubscribeOnViewEvents(_view);
        
        _view.Hide();

        ViewHidden?.Invoke(this);
    }

    public IInstantiatable GetInstantiatable()
    {
        return _view;
    }

    private void SubscribeOnViewEvent(ToiletUIActionsView view)
    {
        view.OnClosePressed += OnCloseButtonPressed;
    }

    private void UnsubscribeOnViewEvents(ToiletUIActionsView view)
    {
        view.OnClosePressed -= OnCloseButtonPressed;
    }

    private void OnCloseButtonPressed()
    {
        _popupService.CloseCurrentView();
    }
}
}