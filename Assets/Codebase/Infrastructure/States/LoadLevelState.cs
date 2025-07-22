using System.Collections.Generic;
using System.Linq;
using Codebase.Gameplay.Pool;
using Codebase.Gameplay.ShapeSpawner;
using Codebase.Gameplay.ShapeSpawner.Factory;
using Codebase.Gameplay.UI;
using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Codebase.Infrastructure.Services.Health;
using Codebase.Infrastructure.Services.Score;
using Codebase.Infrastructure.Services.StaticData.Data.Data;
using Codebase.Loading;
using Codebase.Utils;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly GameSettings _gameSettings;
        private readonly HealthService _healthService;
        private readonly ScoreService _scoreService;
        private readonly SimpleEventBus _eventBus;
        private readonly IShapeSpawnerLimiter _shapeSpawnerLimiter;
        private readonly ShapeSpawnerFactory _shapeSpawnerFactory;
        private readonly ShapeSpawnerManager _shapeSpawnerManager;
        private readonly ShapePool _shapePool;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            GameSettings gameSettings, HealthService healthService, ScoreService scoreService, SimpleEventBus eventBus,
            ShapeSpawnerFactory shapeSpawnerFactory, IShapeSpawnerLimiter shapeSpawnerLimiter,
            ShapeSpawnerManager shapeSpawnerManager, ShapePool shapePool)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameSettings = gameSettings;
            _healthService = healthService;
            _scoreService = scoreService;
            _eventBus = eventBus;
            _shapeSpawnerFactory = shapeSpawnerFactory;
            _shapeSpawnerLimiter = shapeSpawnerLimiter;
            _shapeSpawnerManager = shapeSpawnerManager;
            _shapePool = shapePool;

            _eventBus.Subscribe<GameRestartSignal>(OnGameRestart);
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
            CreateAndSetActors();
            StartGame();
            _stateMachine.Enter<GameLoopState>();
        }

        private void CreateAndSetActors()
        {
            IEnumerable<ShapeSpawnerPoint> spawnerPoints =
                Object.FindObjectsByType<ShapeSpawnerPoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            foreach (ShapeSpawnerPoint point in spawnerPoints)
            {
                ShapeSpawner shapeSpawner = _shapeSpawnerFactory.CreateAt(point.transform.position);
                shapeSpawner.transform.SetParent(point.transform.parent);
             
            }
        }

        private void OnGameRestart(GameRestartSignal signal)
        {
            _shapePool.DespawnAllActive();
            StartGame();
        }

        private void StartGame()
        {
            _healthService.Initialize(_gameSettings.PlayerHealth);
            _scoreService.Initialize(0);

            _shapeSpawnerLimiter.SetShapeSpawnerLimit(_gameSettings.ShapesCountRange);
            _shapeSpawnerManager.StartSpawning(_gameSettings.SpawnTimeoutRange, _gameSettings.MovementSpeedRange);
        }
    }
}