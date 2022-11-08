using System;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
    public class UIManager
    {
        private readonly IDialogsManager _popupsManager = new PopupsManager();
        private readonly IDialogsManager _fullScreensManager = new FullScreensManager();
        
        private BaseUIWindow _currentWindow;

        public UIManager(string prefabsPath, IInstantiater instantiater)
        {
            InitializeManagers(prefabsPath, instantiater);
        }

        private void InitializeManagers(string prefabsPath, IInstantiater instantiater)
        {
            _popupsManager.Initialize(prefabsPath, instantiater);
            _fullScreensManager.Initialize(prefabsPath, instantiater);
        }

        public BaseUIWindow Show<T>() where T : BaseUIWindow, new()
        {
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            switch (new T())
            {
                case PopupDialog _:
                    return _popupsManager.Show<T>();
                case FullScreenDialog _:
                    return _fullScreensManager.Show<T>();
                default:
                    throw new Exception("Incorrect type: " + typeof(T));
            }
        }

        public BaseUIWindow Hide<T>() where T : BaseUIWindow, new()
        {
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            switch (new T())
            {
                case PopupDialog _:
                    return _popupsManager.Hide<T>();
                case FullScreenDialog _:
                    return _fullScreensManager.Hide<T>();
                default:
                    throw new Exception("Incorrect type: " + typeof(T));
            }
        }
    }
}