using System;
using Script.ProjectLibraries.MVVM;

namespace Script.Scenes.Common.InteractableObjects.Toilet
{
public class ToiletModel : Model
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