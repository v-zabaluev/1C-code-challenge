using System;
using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Codebase.Infrastructure.Services.Health;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Codebase.Gameplay
{
    public class Shape : MonoBehaviour, IPoolable<ShapeData, IMemoryPool>, IDisposable
    {
        [Inject] private SimpleEventBus _eventBus;
        [Inject] private ScoreService _scoreService;

        [SerializeField] private ShapeMoving _moving;
        [SerializeField] private ShapeView _view;
        [SerializeField] private ShapeDragHandler _dragHandler;
        private IMemoryPool _pool;
        private ShapeData _data;

        public void OnSpawned(ShapeData data, IMemoryPool pool)
        {
            _data = data;
            _pool = pool;

            _view.SetSprite(data.ShapeSprite);

            _eventBus.Subscribe<GameOverSignal>(OnGameOver);
            _dragHandler.OnBeginDragAction += OnBeginDrag;
            _dragHandler.OnEndDragAction += OnEndDrag;
            _dragHandler.OnMissedDragAction += OnMissedDrag;
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
            _dragHandler.OnBeginDragAction -= OnBeginDrag;
            _dragHandler.OnEndDragAction -= OnEndDrag;
        }

        private void OnGameOver(GameOverSignal signal)
        {
            _moving.ResetState();
        }

        private void OnBeginDrag()
        {
            _moving.StopMoving();
        }

        private void OnEndDrag(SorterSlot sorter)
        {
            if (_data.ShapeType == sorter.Type)
            {
                _scoreService.ChangeScore(1);
                Dispose();
            }
            else if (_data.ShapeType != sorter.Type)
            {
                _scoreService.ChangeScore(-1);
                Dispose();
            }
        }

        private void OnMissedDrag()
        {
            _moving.StartMoving();
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