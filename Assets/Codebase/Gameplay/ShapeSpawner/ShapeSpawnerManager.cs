﻿using System;
using System.Threading;
using Codebase.Gameplay.ShapeSpawner.Factory;
using Codebase.Utils;
using Cysharp.Threading.Tasks;
using Zenject;
using Random = UnityEngine.Random;

namespace Codebase.Gameplay.ShapeSpawner
{
    public class ShapeSpawnerManager : IInitializable, IDisposable
    {
        [Inject] private ShapeSpawnerFactory _spawnerFactory;
        [Inject] private IShapeSpawnerLimiter _shapeSpawnLimiter;
        private CancellationTokenSource _cts;

        public void Initialize()
        {
            _shapeSpawnLimiter.OnLimitReached += StopSpawning;
        }

        public void Dispose()
        {
            StopSpawning();
            _shapeSpawnLimiter.OnLimitReached -= StopSpawning;
        }

        public void StartSpawning(FloatRangeValues spawnIntervalRange, FloatRangeValues speedRange)
        {
            StopSpawning();
            _cts = new CancellationTokenSource();
            SpawnLoopAsync(spawnIntervalRange, speedRange, _cts.Token).Forget();
        }

        public void StopSpawning()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTask SpawnLoopAsync(FloatRangeValues spawnIntervalRange, FloatRangeValues speedRange,
            CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                float delay = Random.Range(spawnIntervalRange.Min, spawnIntervalRange.Max);
                float speed = Random.Range(speedRange.Min, speedRange.Max);

                int spawnerNumber = Random.Range(0, _spawnerFactory.CreatedSpawners.Count);
                _spawnerFactory.CreatedSpawners[spawnerNumber].Spawn(speed);

                _shapeSpawnLimiter.RegisterShapeSpawn();

                await UniTask.WaitForSeconds(delay, cancellationToken: token);
            }
        }

        private void SpawnTest()
        {
            FloatRangeValues spawnInterval = new FloatRangeValues(2, 5);
            FloatRangeValues speedInterval = new FloatRangeValues(2, 5);
            StartSpawning(spawnInterval, speedInterval);
        }
    }
}