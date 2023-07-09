using Script.ProjectLibraries.MVVM;

namespace Script.Scenes.Common.InteractableObjects.Bed
{
public class BedViewModel : ViewModel
{
    private BedModel _model;
    private BedView _view; 

    public BedViewModel(BedView view)
    {
        _view = AddDisposable(view);
        _model = AddDisposable(new BedModel());
    }
}
}