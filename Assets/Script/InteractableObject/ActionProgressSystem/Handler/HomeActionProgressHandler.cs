using Script.Initializer;
using Script.UI.Dialogs.MainUI.MainHome;
using UnityEngine;

namespace Script.InteractableObject.ActionProgressSystem
{
public class HomeActionProgressHandler : IInitializable, IActionProgressHandler
{
    private HomeUIViewModel _homeUIViewModel;
    
    public void StartActionProgress(HomeUIViewModel homeUIViewModel, float duration, Vector3 position)
    {
        _homeUIViewModel = homeUIViewModel;

        _homeUIViewModel.ShowProgressBar(duration, position);
    }
}
}