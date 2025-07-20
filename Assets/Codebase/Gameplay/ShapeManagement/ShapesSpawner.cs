using System;
using System.Threading;
using Codebase.Gameplay.Factory;
using Codebase.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Codebase.Gameplay
{
    public class ShapesSpawner : MonoBehaviour
    {
        [Inject] private ShapeFactory _shapeFactory;
        private CancellationTokenSource _cts;

        private async void Start()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            SpawnTest();
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

                Shape shape = _shapeFactory.CreateAt(transform.position);
                shape.Initialize(speed);
                shape.StartMovement();

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