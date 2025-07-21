using System;
using Codebase.Utils;
using Random = UnityEngine.Random;

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
        public void SetShapeSpawnerLimit(IntRangeValues count)
        {
            MaxSpawns = Random.Range(count.Min, count.Max);
            _currentSpawns = 0;
        }
    }
}