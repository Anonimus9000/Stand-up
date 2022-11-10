using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
    public class FullScreensManager : IDialogsManager
    {
        private IFullScreenDialog _currentDialog;
        private List<IUIWindow> _fullScreenDialogPrefabs;
        private IInstantiater _instantiater;

        public void Initialize(IInstantiater instantiater, List<IUIWindow> fullScreenDialogs)
        {
            _instantiater = instantiater;

            _fullScreenDialogPrefabs = fullScreenDialogs;
        }

        public IUIWindow Show<T>() where T : IUIWindow
        {
            var dialogToShow = GetPrefab<T>();

            TryCloseCurrentDialog();

            var fullScreenDialog = _instantiater.Instantiate(dialogToShow) as IFullScreenDialog;

            fullScreenDialog!.OnShown();

            return fullScreenDialog;
        }

        public IUIWindow Hide<T>() where T : IUIWindow
        {
            throw new NotImplementedException();
        }

        public void Close<T>() where T : IUIWindow
        {
            var fullScreenDialog = GetPrefab<T>();
            CloseDialog(fullScreenDialog);
        }

        private bool TryCloseCurrentDialog()
        {
            if (_currentDialog == null) return false;

            CloseDialog(_currentDialog);

            return true;
        }

        private IFullScreenDialog GetPrefab<T>() where T : IUIWindow
        {
            foreach (var dialogPrefab in _fullScreenDialogPrefabs)
            {
                if (dialogPrefab is T)
                {
                    return dialogPrefab as IFullScreenDialog;
                }
            }

            throw new Exception($"Can't find prefab {typeof(T)}");
        }

        private void CloseDialog(IUIWindow dialogToClose)
        {
            dialogToClose.OnHidden();
            
            _instantiater.Destroy(dialogToClose);
        }
    }
}