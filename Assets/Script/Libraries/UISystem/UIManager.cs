using System.Collections.Generic;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.Libraries.UISystem
{
    public class UIManager : MonoBehaviour
    {
        private List<BaseUIWindow> _windowPrefabs;
        private Transform _parentToInstantiate;
        private BaseUIWindow _currentWindow;


        public void Initialize(string prefabsPath, Transform prefabTransform)
        {
            var baseUIWindows = Resources.LoadAll<BaseUIWindow>(prefabsPath);
            _windowPrefabs = new List<BaseUIWindow>(baseUIWindows);
            _parentToInstantiate = prefabTransform;

        }
        public void Show<T>() where T : BaseUIWindow
        {
            foreach (var windowPrefab in _windowPrefabs)
            {
                if (windowPrefab is T)
                {
                    _currentWindow = Instantiate(windowPrefab, _parentToInstantiate);
                    
                }
            }
        }

        public void Hide<T>() where T : BaseUIWindow
        {
            Destroy(_currentWindow);
        }
        
        

    }
}