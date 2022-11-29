using Script.InteractableObject.Base;
using Script.Libraries.MVVM;

namespace Script.InteractableObject.InteractableObjectsManager.Home.HomeMVVM.Computer
{
public class ComputerView : IView, IInteractable
{
    public IViewModel ViewModel { get; private set; }

    public void Initialize()
    {
        ViewModel = new ComputerViewModel(this);
    }

    public void OnClick()
    {
    }
}
}