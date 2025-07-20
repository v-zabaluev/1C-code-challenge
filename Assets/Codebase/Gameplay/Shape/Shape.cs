using System;
using UnityEngine;
using Zenject;

namespace Codebase.Gameplay
{
    public class Shape : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        [SerializeField] private ShapeMoving _moving;
        private IMemoryPool _pool;

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
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
        }
    }
}