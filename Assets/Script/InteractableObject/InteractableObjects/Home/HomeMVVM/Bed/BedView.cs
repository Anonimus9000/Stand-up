using Script.Libraries.MVVM;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Bed
{
public class BedView : IView
{
    public IViewModel ViewModel { get; private set; }

    public void Initialize(IViewModel viewModel)
    {
        ViewModel = new BedViewModel(this);
    }

    public void OnClick()
    {
        
    }
}
}