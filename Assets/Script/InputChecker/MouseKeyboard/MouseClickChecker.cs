﻿using System;
using Script.InputChecker.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Script.InputChecker.MouseKeyboard
{
public class MouseClickChecker : IObjectClickChecker
{
    public bool IsBlockedByUI { get; }
    public event Action ObjectClicked;

    private readonly Camera _mainCamera;
    private readonly InputAction _mouseClickInputAction;
    private readonly Collider _trackCollider;
    
    public MouseClickChecker(Camera mainCamera, InputAction mouseClickInputAction, Collider trackCollider, bool isU = true)
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
        if (IsBlockedByUI)
        {
            var isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
            if (isPointerOverGameObject)
            {
                return true;
            }
        }

        return false;
    }
}
}