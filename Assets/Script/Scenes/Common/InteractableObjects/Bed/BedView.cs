using Script.ProjectLibraries.MVVM;

namespace Script.Scenes.Common.InteractableObjects.Bed
{
public class BedView : IView
{
    private BedModel _model;
    public void InitializeModel(IModel model)
    {
        _model = model as BedModel;
    }

    public void OnClick()
    {
        
    }
}
}