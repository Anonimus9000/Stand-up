using Script.Libraries.MVVM;
using Script.Libraries.Observer.Base;
using Script.Libraries.Observer.DataObserver;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjects.Common.Player.PlayerMVVM
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