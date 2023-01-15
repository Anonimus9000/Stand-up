using UnityEngine;

namespace Script.UI.Converter
{
public class PositionsConverter
{
    private readonly Camera _camera;
    private readonly RectTransform _mainArea;
    public PositionsConverter(Canvas mainCanvas, Camera mainCamera)
    {
        _mainArea = mainCanvas.transform as RectTransform;
        _camera = mainCamera;
    }
    
    public Vector3 WorldToScreenSpace(Vector3 worldPos)
    {
        var screenPoint = _camera.WorldToScreenPoint(worldPos);
        screenPoint.z = 0;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_mainArea, screenPoint, _camera, out var screenPos))
        {
            return screenPos;
        }

        return screenPoint;
    }
}
}