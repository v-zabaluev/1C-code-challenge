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
        
        private bool _dragEnded;
        private Vector3 _originalPosition;
        private bool _dragging;

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

        public void Dispose()
        {
            _eventBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _pool?.Despawn(this);
            _pool = null;
            
            _dragHandler.OnBeginDragAction -= OnBeginDrag;
            _dragHandler.OnEndDragAction -= OnEndDrag;
        }

        public void Initialize(float speed)
        {
            _moving.Initialize(speed, Vector2.right);
        }

        public void StartMovement()
        {
            _moving.StartMoving();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!_dragging)
            {
                if (other.transform.TryGetComponent(out SorterSlot slot))
                {
                    HandleSlotCollision(slot);
                }
                else
                {
                    transform.position = _originalPosition;
                    _moving.StartMoving();
                }
            }

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
        }

        private void OnGameOver(GameOverSignal signal)
        {
            _moving.ResetState();
        }

        private void HandleSlotCollision(SorterSlot sorter)
        {
            if (_data.ShapeType == sorter.Type)
            {
                _scoreService.ChangeScore(1);
            }
            else
            {
                _scoreService.ChangeScore(-1);
            }

            Dispose();
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