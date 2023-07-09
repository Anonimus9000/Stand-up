using System;
using System.Collections.Generic;
using Script.DataServices.Base;
using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.ResourceLoader;
using Script.ProjectLibraries.SceneSwitcherSystem;
using Script.ProjectLibraries.ServiceLocators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.Scenes.ApplicationLoading;
using Script.Scenes.Concert;
using Script.Scenes.Home;
using Script.Scenes.MainMenu;
using Script.Scenes.MainMenu.UIs.FullScreens.CharacterCreation.Components;
using Script.Utils.UIPositionConverter;
using UnityEngine;
using ILogger = Script.ProjectLibraries.Logger.LoggerBase.ILogger;

namespace Script.Scenes
{
public class SceneModel : Model, ISceneModel
{
    public ISceneSwitcher SceneSwitcher { get; private set; }
    
    private readonly IInActionProgressConfig _inActionProgressEventsFakeConfig;
    private readonly IUIServiceLocator _uiServiceLocator;
    private readonly CharacterSelector _characterSelector;
    private readonly ICharacterModelsConfig _characterConfig;
    private readonly PositionsConverter _positionsConverter;
    private readonly IDataService _playerData;
    private List<IScene> _scenes;
    private readonly IInteractableObjectsConfig _interactableObjectsConfig;
    private readonly Canvas _mainCanvas;
    private readonly IDataServiceLocator _dataServiceLocator;
    private readonly Camera _mainCamera;

    public SceneModel(ILogger logger,
        IResourceLoader resourceLoader,
        Transform scenesParent,
        IInActionProgressConfig inActionProgressEventsFakeConfig,
        IUIServiceLocator uiServiceLocator,
        ICharacterModelsConfig characterConfig,
        PositionsConverter positionsConverter,
        IDataService playerData,
        IInteractableObjectsConfig interactableObjectsConfig,
        Canvas mainCanvas,
        IDataServiceLocator dataServiceLocator, 
        Camera mainCamera)
    {
        _mainCamera = mainCamera;
        _dataServiceLocator = dataServiceLocator;
        _mainCanvas = mainCanvas;
        _interactableObjectsConfig = interactableObjectsConfig;
        _inActionProgressEventsFakeConfig = inActionProgressEventsFakeConfig;
        _uiServiceLocator = uiServiceLocator;
        _characterConfig = characterConfig;
        _positionsConverter = positionsConverter;
        _playerData = playerData;
        SceneSwitcher = AddDisposable(new SceneSwitcherViewModel(logger, this));

        InitializeScenes(resourceLoader, scenesParent);
    }
    
    public T GetScene<T>() where T : IScene
    {
        foreach (var gameLocation in _scenes)
        {
            if (gameLocation is T location)
            {
                return location;
            }
        }

        throw new Exception("Can't find scene");
    }

    private void InitializeScenes(IResourceLoader resourceLoader, Transform scenesParent)
    {
        var homeScene = AddDisposable(new HomeScene(
            resourceLoader,
            scenesParent,
            _inActionProgressEventsFakeConfig,
            SceneSwitcher,
            _uiServiceLocator,
            _characterSelector,
            _characterConfig,
            _positionsConverter,
            _playerData,
            _mainCanvas,
            _interactableObjectsConfig,
            _dataServiceLocator, 
            _mainCamera));
        var scenes = new IScene[]
        {
            AddDisposable(new ApplicationLoadingScene()),
            homeScene,
            new ConcertScene(),
            new MainMenuScene()
        };
        
        _scenes = new List<IScene>(scenes);
    }
}
}