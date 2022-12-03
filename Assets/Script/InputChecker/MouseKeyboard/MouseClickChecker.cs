using System;
using Script.InputChecker.Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.InputChecker.MouseKeyboard
{
public class MouseClickChecker : IObjectClickChecker
{
    public event Action ObjectClicked;

    private readonly Camera _mainCamera;
    private readonly InputAction _mouseClickInputAction;
    private readonly Collider _trackCollider;
    
    public MouseClickChecker(Camera mainCamera, InputAction mouseClickInputAction, Collider trackCollider)
    {
        _trackCollider = trackCollider;
        _mainCamera = mainCamera;
        _mouseClickInputAction = mouseClickInputAction;
    }

    public void Activate()
    {
        _mouseClickInputAction.performed += OnMouseInputPerformed;
    }

    public void Deactivate()
    {
        _mouseClickInputAction.performed -= OnMouseInputPerformed;
    }

    public void OnObjectClicked()
    {
        ObjectClicked?.Invoke();
    }

    private void OnMouseInputPerformed(InputAction.CallbackContext context)
    {
        var screenPointToRay = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (Physics.Raycast(ray: screenPointToRay, hitInfo: out var hit) && hit.collider)
        {
            if (hit.collider == _trackCollider)
            {
                OnObjectClicked();
            }
        }
    }
}
}