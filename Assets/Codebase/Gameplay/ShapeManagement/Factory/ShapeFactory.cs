using Codebase.Gameplay.Pool;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Codebase.Gameplay.Factory
{
    public class ShapeFactory : IFactory<ShapeData, Shape>
    {
        private readonly ShapePool _shapePool;

        public ShapeFactory(ShapePool shapePool)
        {
            _shapePool = shapePool;
        }

        public Shape Create(ShapeData shapeData)
        {
            return _shapePool.Spawn(shapeData, _shapePool);
        }

        public Shape CreateAt(Vector3 position, ShapeData shapeData)
        {
            var shape = Create(shapeData);
            shape.transform.position = position;

            return shape;
        }
    }
}