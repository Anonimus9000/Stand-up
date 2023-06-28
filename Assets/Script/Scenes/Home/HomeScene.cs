using System;
using System.Threading.Tasks;
using Script.DataServices.Base;
using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.ConfigParser.Configs.FakeConfigs;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.ResourceLoader;
using Script.ProjectLibraries.SceneSwitcherSystem;
using Script.ProjectLibraries.ServiceLocators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.Scenes.Common.ActionProgressSystem.Handler;
using Script.Scenes.Home.ActionProgressSystem.Handler;
using Script.Scenes.Home.UIs.MainUIs.MainHome;
using Script.Scenes.MainMenu.UIs.FullScreens.CharacterCreation.Components;
using Script.Utils.UIPositionConverter;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script.Scenes.Home
{
public class HomeScene : ViewModel, IScene
{
    public event Action<SceneType> SceneOpened;
    public event Action<SceneType> SceneClosed;

    private readonly ResourceImage _resourceImage = new("HomeScene", "Scenes");
    private readonly IResourceLoader _resourceLoader;
    private readonly Transform _scenesParent;
    private readonly ISceneSwitcher _sceneSwitcher;

    private HomeRoot _homeSceneRoot;
    private readonly IUIServiceLocator _uiServiceLocator;
    private readonly CharacterSelector _characterSelector;
    private readonly ICharacterModelsConfig _characterConfig;
    private readonly PositionsConverter _positionsConverter;
    private readonly IDataService _playerData;
    private readonly IDataServiceLocator _dataServiceLocator;
    private readonly IInteractableObjectsConfig _interactableObjectsConfig;
    private readonly Canvas _mainCanvas;
    private readonly HomeActionProgressHandler _homeActionProgressHandler;
    private readonly Camera _mainCamera;

    public HomeScene(IResourceLoader resourceLoader,
        Transform scenesParent,
        IInActionProgressConfig inActionProgressEventsFakeConfig,
        ISceneSwitcher sceneSwitcher,
        IUIServiceLocator uiServiceLocator,
        CharacterSelector characterSelector,
        ICharacterModelsConfig characterConfig,
        PositionsConverter positionsConverter,
        IDataService playerData,
        Canvas mainCanvas,
        IInteractableObjectsConfig interactableObjectsConfig,
        IDataServiceLocator dataServiceLocator, 
        Camera mainCamera)
    {
        _mainCamera = mainCamera;
        _mainCanvas = mainCanvas;
        _interactableObjectsConfig = interactableObjectsConfig;
        _dataServiceLocator = dataServiceLocator;
        _homeActionProgressHandler = InitializeActionProgressHandler(inActionProgressEventsFakeConfig);
        _resourceLoader = resourceLoader;
        _scenesParent = scenesParent;
        _sceneSwitcher = sceneSwitcher;
        _uiServiceLocator = uiServiceLocator;
        _characterSelector = characterSelector;
        _characterConfig = characterConfig;
        _positionsConverter = positionsConverter;
        _playerData = playerData;
    }

    public async Task OpenAsync()
    {
        if (_homeSceneRoot == null)
        {
            await LoadSceneAsync();
        }

        ShowHome();

        SceneOpened?.Invoke(SceneType.Home);
    }

    public Task CloseAsync()
    {
        Close();
        return Task.CompletedTask;
    }
    
    public void Open()
    {
        if (_homeSceneRoot == null)
        {
            LoadScene();
        }

        ShowHome();

        SceneOpened?.Invoke(SceneType.Home);
    }

    public void Close()
    {
        HideHome();
        
        SceneClosed?.Invoke(SceneType.Home);
    }

    private void ShowHome()
    {
        var mainUIService = _uiServiceLocator.GetService<MainUIService>();
        var homeUIViewModel = new HomeUIViewModel(
            _sceneSwitcher,
            _uiServiceLocator,
            _characterConfig,
            _characterSelector,
            _positionsConverter,
            _homeActionProgressHandler,
            _playerData,
            _resourceLoader);

        mainUIService.Show<HomeUIView>(homeUIViewModel);
        
        _homeSceneRoot.gameObject.SetActive(true);
    }

    private void HideHome()
    {
        _homeSceneRoot.gameObject.SetActive(false);
    }

    private async Task LoadSceneAsync()
    {
        var prefab = await _resourceLoader.LoadResourceAsync(_resourceImage, Application.exitCancellationToken);
        CreateScene(prefab);
    }
    
    private void LoadScene()
    {
        _resourceLoader.LoadResource(_resourceImage, OnResourceLoaded);
    }

    private void OnResourceLoaded(GameObject prefab)
    {
        CreateScene(prefab);
    }

    private void CreateScene(GameObject prefab)
    {
        _homeSceneRoot = AddDisposable(Object.Instantiate(prefab, _scenesParent).GetComponent<HomeRoot>());
        _homeSceneRoot.Initialize(
            _uiServiceLocator,
            _dataServiceLocator,
            _homeActionProgressHandler,
            _mainCanvas,
            _interactableObjectsConfig,
            _resourceLoader,
            _mainCamera);
    }
    
    private HomeActionProgressHandler InitializeActionProgressHandler(IInActionProgressConfig inActionProgressEventsFakeConfig)
    {
        var homeActionProgressHandler = new HomeActionProgressHandler(inActionProgressEventsFakeConfig);
        
        return homeActionProgressHandler;
    }
}
}