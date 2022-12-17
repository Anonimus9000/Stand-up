using System;
using Script.Libraries.MVVM;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Toilet
{
public class ToiletModel:IModel
{
    public event Action<bool> InputActiveChanged;
    public bool InputActive
    {
        get => _inputActive;
        set
        {
            _inputActive = value;
            InputActiveChanged?.Invoke(value);
        }
    }

    private bool _inputActive;
}
}