using Script.Libraries.MVVM;

namespace Script.InteractableObject.InteractableObjectsManager.Home.HomeMVVM.Bed
{
public class BedView : IView
{
    public IViewModel ViewModel { get; private set; }

    public void Initialize()
    {
        ViewModel = new BedViewModel(this);
    }

    public void OnClick()
    {
        
    }
}
}