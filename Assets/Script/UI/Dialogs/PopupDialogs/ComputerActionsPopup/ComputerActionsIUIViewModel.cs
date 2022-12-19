using System;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.UI.Dialogs.PopupDialogs.ComputerActionsPopup
{
public class ComputerActionsIUIViewModel : IUIViewModel
{
    public event Action<IUIViewModel> ViewShown;
    public event Action<IUIViewModel> ViewHidden;
    
    private ComputerActionsUIView _view;
    private readonly ComputerActionsModel _model;
    private readonly IUIService _popupUIService;

    public ComputerActionsIUIViewModel(IUIService popupUIService)
    {
        _popupUIService = popupUIService;
        _model = new ComputerActionsModel();
    }

    public void ShowView(IUIView view)
    {
        if (view is not ComputerActionsUIView computerActionsUIView)
        {
            throw new Exception($"Incorrect type {view.GetType()}; Need {typeof(ComputerActionsUIView)}");
        }

        _view = computerActionsUIView;
        _view.Show();
        
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

    private void SubscribeOnViewEvents(ComputerActionsUIView view)
    {
        view.OnClosePressed += OnCloseButtonPressed;
    }

    private void UnsubscribeInViewEvents(ComputerActionsUIView view)
    {
        view.OnClosePressed -= OnCloseButtonPressed;
    }

    private void OnCloseButtonPressed()
    {
        _popupUIService.CloseCurrentView();
    }
}
}