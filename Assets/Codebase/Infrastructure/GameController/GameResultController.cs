using System;
using Codebase.Gameplay.Pool;
using Codebase.Gameplay.ShapeSpawner;
using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Codebase.Infrastructure.Services.Health;
using Zenject;

namespace Codebase.Infrastructure.GameController
{
    public class GameResultController : IInitializable, IDisposable
    {
        [Inject] private SimpleEventBus _eventBus;
        [Inject] private ShapeSpawnerLimiter _shapeSpawnerLimiter;
        [Inject] private HealthService _healthService;
        [Inject] private ShapePool _shapePool;

        private bool _isAllShapesSpawned = false;

        public void Initialize()
        {
            _shapeSpawnerLimiter.OnLimitReached += AllShapesSpawned;
            _eventBus.Subscribe<OnShapeDespawnedSignal>(OnShapeDespawned);
        }

        public void Dispose()
        {
            _shapeSpawnerLimiter.OnLimitReached -= AllShapesSpawned;
            _eventBus.Unsubscribe<OnShapeDespawnedSignal>(OnShapeDespawned);
        }

        private void AllShapesSpawned()
        {
            _isAllShapesSpawned = true;
        }

        private void OnShapeDespawned(OnShapeDespawnedSignal obj)
        {
            if (_healthService.CurrentHealth > 0 && _isAllShapesSpawned && _shapePool.ActiveShapeCount <= 0)
            {
                _eventBus.Invoke(new GameOverSignal(GameStatus.Win));
            }
        }
    }
}