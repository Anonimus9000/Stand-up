using System;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterInfo
{
public class CharacterInfoViewModel : IUIViewModel
{
    public event Action<IUIViewModel> ViewShown;
    public event Action<IUIViewModel> ViewHidden;
    
    private CharacterInfoView _view;
    private readonly CharacterInfoModel _model;
    private readonly IUIService _fullScreenService;

    public CharacterInfoViewModel(IUIService fullScreensUIService)
    {
        _fullScreenService = fullScreensUIService;
        _model = new CharacterInfoModel();
    }

    public void ShowView(IUIView view)
    {
        _view = view as CharacterInfoView;
        
        SubscribeOnViewEvents(_view);
        _view!.Show();
        
        ViewShown?.Invoke(this);
    }

    public void ShowHiddenView()
    {
        _view.Hide();
        
        ViewShown?.Invoke(this);
    }

    public void HideView()
    {
        _view.Show();
        
        ViewHidden?.Invoke(this);
    }

    public IInstantiatable GetInstantiatable()
    {
        return _view;
    }

    private void SubscribeOnViewEvents(CharacterInfoView view)
    {
        view.CloseButtonPressed += OnCloseButtonPressed;
    }

    private void OnCloseButtonPressed()
    {
        _fullScreenService.CloseCurrentView();
    }
}
}