using System;
using System.Collections.Generic;
using Script.ProjectLibraries.InputChecker.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Script.ProjectLibraries.InputChecker.MouseKeyboard
{
public class MouseLeftClickChecker : IObjectClickChecker
{
    public bool IsBlockedByUI { get; }
    public event Action ObjectClicked;

    private readonly Camera _mainCamera;
    private readonly InputAction _mouseClickInputAction;
    private readonly Collider _trackCollider;

    public MouseLeftClickChecker(Camera mainCamera, InputAction mouseClickInputAction, Collider trackCollider,
        bool isU = true)
    {
        IsBlockedByUI = isU;
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
        if (IsBlocked())
        {
            return;
        }

        var screenPointToRay = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray: screenPointToRay, hitInfo: out var hit) && hit.collider)
         {
            if (hit.collider == _trackCollider)
            {
                OnObjectClicked();
            }
        }
    }

    private bool IsBlocked()
    {
        var isBlocked = IsUIBlock();

        return isBlocked;
    }

    private bool IsUIBlock()
    {
        if (!IsBlockedByUI) return false;
        
        var isPointerOverGameObject = IsPointerOverUIObject() || EventSystem.current.IsPointerOverGameObject();
        
        return isPointerOverGameObject;
    }

    private static bool IsPointerOverUIObject()
    {
        var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        
        return results.Count > 0;
    }
}
}