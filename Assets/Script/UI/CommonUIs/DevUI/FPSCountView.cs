using System.Globalization;
using TMPro;
using UnityEngine;

namespace Script.UI.CommonUIs.DevUI
{
public class FPSCountView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _fpsCount;

    [SerializeField]
    private int _targetFrameRate = 60;

    private float _count;

    // private IEnumerator Start()
    // {
    //     while (true)
    //     {
    //         Application.targetFrameRate = _targetFrameRate;
    //         _count = 1f / Time.unscaledDeltaTime;
    //         _fpsCount.text = Mathf.Round(_count).ToString(CultureInfo.InvariantCulture);
    //         yield return new WaitForSeconds(0.1f);
    //     }
    // }

    void Update()
    {
        Application.targetFrameRate = _targetFrameRate;
        
        
        _count += (Time.deltaTime - _count) * 0.1f;
        var fps = 1.0f / _count;
        var fpsCount = Mathf.Ceil(fps);

        if (fpsCount > _targetFrameRate)
        {
            fpsCount = _targetFrameRate;
        }
        
        _fpsCount.text = fpsCount.ToString(CultureInfo.InvariantCulture);
    }
}
}