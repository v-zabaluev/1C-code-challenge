using System.Collections.Generic;
using System.Linq;
using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Zenject;

namespace Codebase.Gameplay.Pool
{
    public class ShapePool : MonoPoolableMemoryPool<ShapeData, IMemoryPool, Shape>
    {
        [Inject] private SimpleEventBus _eventBus;
        private List<Shape> _activeShapes = new List<Shape>();
        
        public int ActiveShapeCount => _activeShapes.Count;
        protected override void OnSpawned(Shape item)
        {
            base.OnSpawned(item);
            _activeShapes.Add(item);
        }

        protected override void OnDespawned(Shape item)
        {
            base.OnDespawned(item);
            _activeShapes.Remove(item);
            _eventBus.Invoke(new OnShapeDespawnedSignal());
        }
        
        public void DespawnAllActive()
        {
            var shapesToDespawn = _activeShapes.ToList();
            foreach (var shape in shapesToDespawn)
            {
                Despawn(shape);
            }
        }
    }

}