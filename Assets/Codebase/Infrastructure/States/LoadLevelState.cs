using System.Collections.Generic;
using System.Linq;
using Codebase.Gameplay.ShapeSpawner;
using Codebase.Gameplay.ShapeSpawner.Factory;
using Codebase.Infrastructure.Services.StaticData.Data.Data;
using Codebase.Loading;
using Codebase.Utils;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private LoadingCurtain _loadingCurtain;
        private readonly GameSettings _gameSettings;

        private IShapeSpawnerLimiter _shapeSpawnerLimiter;
        private ShapeSpawnerFactory _shapeSpawnerFactory;
        private ShapeSpawnerManager _shapeSpawnerManager;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, GameSettings gameSettings)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameSettings = gameSettings;
        }

        public async void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            await _sceneLoader.LoadAsync(sceneName, OnLoaded);
            Debug.Log($"Loading level {sceneName}");
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            GetLazyDependencies();
            CreateAndSetActors();
            StartGame();
            _stateMachine.Enter<GameLoopState>();
        }

        private void GetLazyDependencies()
        {
            var sceneContext = Object.FindFirstObjectByType<SceneContext>(FindObjectsInactive.Exclude);
            _shapeSpawnerLimiter = sceneContext.Container.Resolve<IShapeSpawnerLimiter>();
            _shapeSpawnerFactory = sceneContext.Container.Resolve<ShapeSpawnerFactory>();
            _shapeSpawnerManager = sceneContext.Container.Resolve<ShapeSpawnerManager>();
        }

        private void CreateAndSetActors()
        {
            //Create spawners

            IEnumerable<ShapeSpawnerPoint> spawnerPoints =
                Object.FindObjectsByType<ShapeSpawnerPoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            foreach (ShapeSpawnerPoint point in spawnerPoints)
            {
                ShapeSpawner shapeSpawner = _shapeSpawnerFactory.CreateAt(point.transform.position);
                shapeSpawner.transform.SetParent(point.transform.parent);
            }

            //Set player health
            //Reset score
        }

        private void StartGame()
        {
            _shapeSpawnerLimiter.SetShapeSpawnerLimit(_gameSettings.ShapesCountRange);
            _shapeSpawnerManager.StartSpawning(_gameSettings.SpawnTimeoutRange,_gameSettings.MovementSpeedRange);
        }
    }
}