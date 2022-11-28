using Script.Libraries.MVVM;

namespace Script.InteractableObject.Base
{
public class InteractableViewModel : IViewModel
{
    IModel Model { get; }
    IView View { get; }

    public InteractableViewModel(IModel model, IView view)
    {
        Model = model;
        View = view;
    }
}
}