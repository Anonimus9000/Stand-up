using Script.Libraries.MVVM;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Bed
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