using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UiMVVM;
using NotImplementedException = System.NotImplementedException;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterInfo
{
public class CharacterInfoViewModel : UIViewModel
{
    private CharacterInfoView _view;
    private readonly CharacterInfoModel _model;

    public CharacterInfoViewModel()
    {
        _model = new CharacterInfoModel();
    }
    
    public override void ShowView()
    {
        _view = fullScreensUiManager.Show<CharacterInfoView>(this);
        
        SubscribeOnViewEvents(_view);
    }

    public override void CloseView()
    {
        fullScreensUiManager.TryCloseCurrent();
    }

    public override IInstantiatable GetInstantiatable()
    {
        return _view;
    }

    private void SubscribeOnViewEvents(CharacterInfoView view)
    {
        view.CloseButtonPressed += OnCloseButtonPressed;
    }

    private void OnCloseButtonPressed()
    {
        fullScreensUiManager.TryCloseCurrent();
    }
}
}