using Script.ProjectLibraries.MVVM;

namespace Script.Scenes.Common.InteractableObjects.Bed
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