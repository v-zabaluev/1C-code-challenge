using System;
using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using UnityEngine;
using Zenject;

namespace Codebase.Gameplay
{
    public class Shape : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        [Inject] private SimpleEventBus _eventBus;
        [SerializeField] private ShapeMoving _moving;
        private IMemoryPool _pool;

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            
            _eventBus.Subscribe<GameOverSignal>(OnGameOver);
        }

        public void OnDespawned()
        {
            _moving.ResetState();
        }

        public void Initialize(float speed)
        {
            _moving.Initialize(speed, Vector2.right);
        }

        public void StartMovement()
        {
            _moving.StartMoving();
        }

        public void Dispose()
        {
            _pool?.Despawn(this);
            _eventBus.Unsubscribe<GameOverSignal>(OnGameOver);
        }

        private void OnGameOver(GameOverSignal signal)
        {
            _moving.ResetState();
        }
    }
}