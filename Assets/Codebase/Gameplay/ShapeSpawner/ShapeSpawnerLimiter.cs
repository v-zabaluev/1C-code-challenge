using System;

namespace Codebase.Gameplay.ShapeSpawner
{
    public class ShapeSpawnerLimiter : IShapeSpawnerLimiter
    {
        private int _currentSpawns;
        public int MaxSpawns { get; set; }

        public bool CanSpawn => _currentSpawns < MaxSpawns;

        public event Action OnLimitReached;

        public void RegisterShapeSpawn()
        {
            _currentSpawns++;

            if (_currentSpawns >= MaxSpawns)
            {
                OnLimitReached?.Invoke();
            }
        }

        public void SetShapeSpawnerLimit(int maxSpawns)
        {
            MaxSpawns = maxSpawns;
            _currentSpawns = 0;
        }
    }
}