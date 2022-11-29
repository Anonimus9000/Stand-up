using Script.InteractableObject.Base;
using Script.Libraries.MVVM;

namespace Script.InteractableObject.InteractableObjectsManager.Home.HomeMVVM.Bed
{
public class BedViewModel : IViewModel
{
    public IModel Model { get; }
    public IView View { get; }

    public BedViewModel(IView view)
    {
        View = view;
        Model = new BedModel();
    }
}
}