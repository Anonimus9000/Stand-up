using Script.Libraries.MVVM;

namespace Script.InteractableObject.InteractableObjectsManager.Home.HomeMVVM.Computer
{
public class ComputerViewModel : IViewModel
{
    public IModel Model { get; }
    public IView View { get; }

    public ComputerViewModel(IView view)
    {
        Model = new ComputerModel();
        View = view;
    }
}
}