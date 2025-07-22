using System;
using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Codebase.Infrastructure.Services.Health;
using Codebase.Infrastructure.Services.Score;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Codebase.Gameplay
{
    public class Shape : MonoBehaviour, IPoolable<ShapeData, IMemoryPool>, IDisposable
    {
        [Inject] private SimpleEventBus _eventBus;
        [Inject] private ScoreService _scoreService;
        [Inject] private HealthService _healthService;

        [SerializeField] private ShapeMoving _moving;
        [SerializeField] private ShapeView _view;
        [SerializeField] private ShapeDragHandler _dragHandler;

        private IMemoryPool _pool;
        private ShapeData _data;

        private bool _dragEnded;
        private Vector3 _originalPosition;
        private bool _dragging;

        private bool _isCollidingWithSort;

        public void OnSpawned(ShapeData data, IMemoryPool pool)
        {
            _data = data;
            _pool = pool;

            _view.SetSprite(data.ShapeSprite);

            _eventBus.Subscribe<GameOverSignal>(OnGameOver);
            _dragHandler.OnBeginDragAction += OnBeginDrag;
            _dragHandler.OnEndDragAction += OnEndDrag;
            _dragEnded = false;
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

        public void Dispose()
        {
            _eventBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _pool?.Despawn(this);
            _pool = null;

            _dragHandler.OnBeginDragAction -= OnBeginDrag;
            _dragHandler.OnEndDragAction -= OnEndDrag;
        }

        public void StartMovement()
        {
            _moving.StartMoving();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out SorterSlot slot))
            {
                _isCollidingWithSort = true;
                if (!_dragging)
                {
                    HandleSlotCollision(slot);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isCollidingWithSort = false;
        }

        private void OnBeginDrag()
        {
            _originalPosition = transform.position;
            _dragging = true;
            _moving.StopMoving();
        }

        private void OnEndDrag()
        {
            _dragging = false;

            if (!_isCollidingWithSort)
            {
                transform.position = _originalPosition;
                _moving.StartMoving();
            }
        }

        private void HandleSlotCollision(SorterSlot sorter)
        {
            if (_data.ShapeType == sorter.Type)
            {
                _scoreService.ChangeScore(1);
            }
            else
            {
                _healthService.ChangeHealth(-1);
            }

            Dispose();
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