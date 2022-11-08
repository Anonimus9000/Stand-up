using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
    public class FullScreensManager : IDialogsManager
    {
        private FullScreenDialog _currentDialog;
        private List<FullScreenDialog> _fullScreenDialogPrefabs;
        private string _fullScreenDialogPrefabsPath;
        private IInstantiater _instantiater;

        public void Initialize(string fullScreenDialogsPrefabsPath, IInstantiater instantiater)
        {
            _instantiater = instantiater;
            _fullScreenDialogPrefabsPath = fullScreenDialogsPrefabsPath;

            InitializePrefabs(_fullScreenDialogPrefabsPath);
        }

        public BaseUIWindow Show<T>() where T : BaseUIWindow
        {
            var dialogToShow = GetPrefab<T>();

            TryCloseCurrentDialog();

            var fullScreenDialog = _instantiater.Instantiate(dialogToShow) as FullScreenDialog;

            fullScreenDialog!.OnShown();

            return fullScreenDialog;
        }

        public BaseUIWindow Hide<T>() where T : BaseUIWindow
        {
            throw new NotImplementedException();
        }

        public void Close<T>() where T : BaseUIWindow
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

        private FullScreenDialog GetPrefab<T>() where T : BaseUIWindow
        {
            foreach (var dialogPrefab in _fullScreenDialogPrefabs)
            {
                if (dialogPrefab is T)
                {
                    return dialogPrefab;
                }
            }

            throw new Exception($"Can't find prefab {typeof(T)}. Need add prefab in {_fullScreenDialogPrefabsPath}");
        }

        private void CloseDialog(BaseUIWindow dialogToClose)
        {
            dialogToClose.OnHidden();
            
            _instantiater.Destroy(dialogToClose);
        }

        private void InitializePrefabs(string prefabsPath)
        {
            var fullScreenDialogs = Resources.LoadAll<FullScreenDialog>(_fullScreenDialogPrefabsPath);
            _fullScreenDialogPrefabs = new List<FullScreenDialog>(fullScreenDialogs);
        }
    }
}