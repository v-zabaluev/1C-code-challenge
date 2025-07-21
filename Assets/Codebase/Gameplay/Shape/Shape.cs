using System;
using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using UnityEngine;
using Zenject;

namespace Codebase.Gameplay
{
    public class Shape : MonoBehaviour, IPoolable<ShapeData, IMemoryPool>, IDisposable
    {
        [Inject] private SimpleEventBus _eventBus;
        [SerializeField] private ShapeMoving _moving;
        [SerializeField] private ShapeView _view;
        private IMemoryPool _pool;
        private ShapeData _data;

        public void OnSpawned(ShapeData data, IMemoryPool pool)
        {
            _data = data;
            _pool = pool;
            
            _view.SetSprite(data.ShapeSprite);

            _eventBus.Subscribe<GameOverSignal>(OnGameOver);
        }

        public void OnDespawned()
        {
            _moving.ResetState();
            _data = null;
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
            _eventBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _pool?.Despawn(this);
            _pool = null;
        }

        private void OnGameOver(GameOverSignal signal)
        {
            _moving.ResetState();
        }
    }

    public enum ShapeType
    {
        Square,
        Circle,
        Triangle,
        Star
    }
}