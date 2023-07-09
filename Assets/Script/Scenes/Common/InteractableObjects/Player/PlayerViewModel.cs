using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.Observer.Base;
using Script.ProjectLibraries.Observer.DataObserver;
using UnityEngine;

namespace Script.Scenes.Common.InteractableObjects.Player
{
public class PlayerViewModel : ViewModel
{
    private PlayerView _playerView;
    private PlayerModel _playerModel;
    private DataObserver _observer;
    
    public PlayerViewModel(PlayerView playerView, IObserver observer)
    {
        _playerView = AddDisposable(playerView);
        _playerModel = AddDisposable(new PlayerModel());
        _observer = observer as DataObserver;
    }

    public void OnPlayerViewClicked()
    {
        Debug.Log("OnPlayerViewClicked");
    }
}
}