using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
    public class UIManager
    {
        private readonly IDialogsManager _popupsManager = new PopupsManager();
        private readonly IDialogsManager _fullScreensManager = new FullScreensManager();
        
        private IUIWindow _currentWindow;

        public UIManager(IInstantiater instantiater, List<IUIWindow> windows)
        {
            InitializeManagers(instantiater, windows);
        }

        public IUIWindow Show<T>() where T : IUIWindow, new()
        {
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            switch (new T())
            {
                case IPopupDialog _:
                    return _popupsManager.Show<T>();
                case IFullScreenDialog _:
                    return _fullScreensManager.Show<T>();
                default:
                    throw new Exception("Incorrect type: " + typeof(T));
            }
        }
        public void Close<T>() where T : IUIWindow, new()
        {
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            switch (new T())
            {
                case IPopupDialog _:
                    _popupsManager.Close<T>();
                    break;
                case IFullScreenDialog _:
                    _fullScreensManager.Close<T>();
                    break;
                default:
                    throw new Exception("Incorrect type: " + typeof(T));
            }
        }

        // private IUIWindow Hide<T>() where T : IUIWindow, new()
        // {
        //     // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
        //     switch (new T())
        //     {
        //         case IPopupDialog _:
        //             return _popupsManager.Hide<T>();
        //         case IFullScreenDialog _:
        //             return _fullScreensManager.Hide<T>();
        //         default:
        //             throw new Exception("Incorrect type: " + typeof(T));
        //     }
        // }

        private void InitializeManagers(IInstantiater instantiater, List<IUIWindow> uiWindows)
        {
            var popupDialogs = new List<IUIWindow>(uiWindows.Count);
            var fullScreenDialogs = new List<IUIWindow>(uiWindows.Count);

            foreach (var uiWindow in uiWindows)
            {
                switch (uiWindow)
                {
                    case IPopupDialog popupDialog:
                        popupDialogs.Add(popupDialog);
                        break;
                    case IFullScreenDialog fullScreenDialog:
                        fullScreenDialogs.Add(fullScreenDialog);
                        break;
                    default:
                        throw new Exception($"Incorrect type window {uiWindow.GetType()}");
                }
            }
            
            _popupsManager.Initialize(instantiater, popupDialogs, this);
            _fullScreensManager.Initialize(instantiater, fullScreenDialogs, this);
        }
    }
}