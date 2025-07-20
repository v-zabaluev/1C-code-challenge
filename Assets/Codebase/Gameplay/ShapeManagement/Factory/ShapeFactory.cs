using UnityEngine;
using Zenject;

namespace Codebase.Gameplay.Factory
{
    public class ShapeFactory : PlaceholderFactory<Shape>
    {
        public Shape CreateAt(Vector3 position)
        {
            Shape shape = Create();
            shape.transform.position = position;
            return shape;
        }
    }
}