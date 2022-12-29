using Script.Initializer.Base;
using Script.UI.Dialogs.MainUI.MainHome;
using UnityEngine;

namespace Script.Initializer.StartApplicationDependenciesInitializers
{
public class ActionProgressHandler : IDependenciesInitializer
{
    private HomeUIViewModel _homeUIViewModel;
    public IInitializable Initialize()
    {
        return null;

    }

    public void StartActionProgress(HomeUIViewModel homeUIViewModel, float duration, Vector3 position)
    {
        _homeUIViewModel = homeUIViewModel;

        _homeUIViewModel.ShowProgressBar(duration, position);
    }
}
}