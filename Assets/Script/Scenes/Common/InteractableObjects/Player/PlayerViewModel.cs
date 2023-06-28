using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.Observer.Base;
using Script.ProjectLibraries.Observer.DataObserver;
using UnityEngine;

namespace Script.Scenes.Common.InteractableObjects.Player
{
public class PlayerViewModel : IViewModel
{
    private PlayerView _playerView;
    private PlayerModel _playerModel;
    private DataObserver _observer;
    
    public PlayerViewModel(IView playerView, IObserver observer)
    {
        _playerView = playerView as PlayerView;
        _playerModel = new PlayerModel();
        _observer = observer as DataObserver;
    }

    public void OnPlayerViewClicked()
    {
        Debug.Log("OnPlayerViewClicked");
    }
}
}