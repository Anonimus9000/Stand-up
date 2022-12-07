using Script.UI.UIFollower.Base;
using UnityEngine;

namespace Script.UI.UIFollower.SpaceObjectFollower
{
public class UiSpaceObjectFollower : IUiFollower
{
    private readonly Transform _transformToFollow;
    private readonly RectTransform _follower;
    private readonly Camera _camera;
    private readonly Canvas _canvas;

    public UiSpaceObjectFollower(
        Transform transformToFollow, 
        RectTransform follower, 
        Camera mainCamera, 
        Canvas canvas)
    {
        _transformToFollow = transformToFollow;
        _follower = follower;
        _camera = mainCamera;
        _canvas = canvas;
    }

    public void SetPosition()
    {
        var canvasRect = _canvas.GetComponent<RectTransform>();
        Vector2 viewportPosition = _camera.WorldToViewportPoint(_transformToFollow.transform.position);
        var sizeDelta = canvasRect.sizeDelta;
        
        var worldObjectScreenPosition = new Vector2(
            viewportPosition.x * sizeDelta.x - sizeDelta.x * 0.5f,
            viewportPosition.y * sizeDelta.y - sizeDelta.y * 0.5f);

        _follower.anchoredPosition = worldObjectScreenPosition;
    }
}
}