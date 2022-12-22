using UnityEngine;

namespace Script.UI.Converter
{
public class PositionsConverter
{
    private readonly Camera _camera;
    private readonly Canvas _canvas;
    public PositionsConverter(Canvas mainCanvas, Camera mainCamera)
    {
        _canvas = mainCanvas;
        _camera = mainCamera;
    }
    
    public Vector3 WorldToScreenSpace(Vector3 worldPos, RectTransform area)
    {
        var screenPoint = _camera.WorldToScreenPoint(worldPos);
        screenPoint.z = 0;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(area, screenPoint, _camera, out var screenPos))
        {
            return screenPos;
        }
        
        return screenPoint;
    }

}
}