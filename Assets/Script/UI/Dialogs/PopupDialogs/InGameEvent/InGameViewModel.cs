using System;
using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.UI.Dialogs.PopupDialogs.InGameEvent
{
public class InGameViewModel : IUIViewModel
{
    public event Action<IUIViewModel> ViewShown;
    public event Action<IUIViewModel> ViewHidden;
    
    private IUIView _view;

    public InGameViewModel()
    {
        
    }

    public void Init(IUIView view, IAnimatorService animatorService)
    {
        _view = view;
    }

    public void Deinit()
    {
    }

    public void ShowView()
    {
    }

    public void ShowHiddenView()
    {
    }

    public void HideView()
    {
    }

    public IInstantiatable GetInstantiatable()
    {
        return _view;
    }
}
}