using System;
using System.Collections.Generic;
using Script.InputChecker.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Script.InputChecker.TouchScreen
{
// ReSharper disable once UnusedType.Global
public class TouchClickChecker : IObjectClickChecker
{
    private readonly Collider _trackCollider;
    private readonly Camera _mainCamera;
    private readonly InputAction _mouseClickInputAction;
    public bool IsBlockedByUI { get; }
    public event Action ObjectClicked;
    
    public TouchClickChecker(
        Camera mainCamera, 
        InputAction mouseClickInputAction,
        Collider trackCollider,
        bool isBlockByUI = true)
    {
        IsBlockedByUI = isBlockByUI;
        _trackCollider = trackCollider;
        _mainCamera = mainCamera;
        _mouseClickInputAction = mouseClickInputAction;
    }
    public void OnObjectClicked()
    {
        ObjectClicked?.Invoke();
    }

    public void Activate()
    {
        _mouseClickInputAction.performed += OnTouch;
    }

    public void Deactivate()
    {
        _mouseClickInputAction.performed -= OnTouch;
    }

    private void OnTouch(InputAction.CallbackContext context)
    {
        Debug.Log("OnMouseInputPerformed");

        if (IsUIBlock())
        {
            return;
        }

        Debug.Log("Screen to point ray");
        var screenPointToRay = _mainCamera.ScreenPointToRay(context.ReadValue<Vector2>());

        if (Physics.Raycast(ray: screenPointToRay, hitInfo: out var hit) && hit.collider)
        {
            if (hit.collider == _trackCollider)
            {
                OnObjectClicked();
            }
        }
    }
    
    private bool IsUIBlock()
    {
        if (!IsBlockedByUI) return false;
        
        var isPointerOverGameObject = IsPointerOverUIObject();
        
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