using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Codebase.Gameplay.Factory
{
    public class ShapeFactory : PlaceholderFactory<ShapeData, Shape>
    {
        public Shape CreateAt(Vector3 position, ShapeData shapeData)
        {
            Shape shape = Create(shapeData);
            shape.transform.position = position;
            return shape;
        }
    }
}