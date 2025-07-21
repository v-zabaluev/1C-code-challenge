using System;
using Codebase.Utils;

namespace Codebase.Gameplay.ShapeSpawner
{
    public interface IShapeSpawnerLimiter
    {
        int MaxSpawns { get; set; }
        bool CanSpawn { get; }
        event Action OnLimitReached;
        void RegisterShapeSpawn();
        void SetShapeSpawnerLimit(IntRangeValues maxSpawns);
    }
}