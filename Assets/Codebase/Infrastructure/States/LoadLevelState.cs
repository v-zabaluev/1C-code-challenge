﻿using System.Collections.Generic;
using System.Linq;
using Codebase.Gameplay.ShapeSpawner;
using Codebase.Gameplay.ShapeSpawner.Factory;
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

        private IShapeSpawnerLimiter _shapeSpawnerLimiter;
        private ShapeSpawnerFactory _shapeSpawnerFactory;
        private ShapeSpawnerManager _shapeSpawnerManager;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
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
            var sceneContext = GameObject.FindObjectOfType<SceneContext>();
            _shapeSpawnerLimiter = sceneContext.Container.Resolve<IShapeSpawnerLimiter>();
            _shapeSpawnerFactory = sceneContext.Container.Resolve<ShapeSpawnerFactory>();
            _shapeSpawnerManager = sceneContext.Container.Resolve<ShapeSpawnerManager>();
        }

        private void CreateAndSetActors()
        {
            //Read data from SO
            //Create spawners

            IEnumerable<ShapeSpawnerPoint> spawnerPoints =
                Object.FindObjectsByType<ShapeSpawnerPoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            foreach (ShapeSpawnerPoint point in spawnerPoints)
            {
                ShapeSpawner shapeSpawner = _shapeSpawnerFactory.CreateAt(point.transform.position);
                shapeSpawner.transform.SetParent(point.transform.parent);
            }
            //Set params to spawner limit

            //Set their params

            //Set player health
            //Reset score
        }

        private void StartGame()
        {
            _shapeSpawnerLimiter.SetShapeSpawnerLimit(15);

            //Read data
            FloatRangeValues spawnInterval = new FloatRangeValues(2, 5);
            FloatRangeValues speedInterval = new FloatRangeValues(2, 5);
            _shapeSpawnerManager.StartSpawning(spawnInterval, speedInterval);
        }
    }
}