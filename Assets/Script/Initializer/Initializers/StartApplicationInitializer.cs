using Script.Initializer.Initializables.StartApplicationInitializables;
using UnityEngine;

namespace Script.Initializer.Initializers
{
    public class StartApplicationInitializer : MonoBehaviour
    {
        [SerializeField] 
        private UIManagerInitializer _uiManagerInitializer;
        
        private void Awake()
        {
            InitializeElements();
        }

        private void InitializeElements()
        {
            InitializeUISystem();
        }

        private void InitializeUISystem()
        {
            _uiManagerInitializer.Initialize();
        }
    }
}
