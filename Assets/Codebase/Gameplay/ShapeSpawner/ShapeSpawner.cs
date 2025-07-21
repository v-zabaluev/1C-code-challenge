using System;
using System.Collections.Generic;
using System.Threading;
using Codebase.Gameplay.Factory;
using Codebase.Infrastructure.EventBus;
using Codebase.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Codebase.Gameplay.ShapeSpawner
{
    public class ShapeSpawner : MonoBehaviour
    {
        [Inject] private ShapeFactory _shapeFactory;
        private List<Shape> _activeShapes = new List<Shape>();
        public List<Shape> ActiveShapes => _activeShapes;
        
        public Shape Spawn(float speed)
        {
            Shape shape = _shapeFactory.CreateAt(transform.position);
            shape.Initialize(speed);
            shape.StartMovement();
            _activeShapes.Add(shape);

            return shape;
        }

        public void Despawn()
        {
            _activeShapes.ForEach(x=>x.Dispose());
            _activeShapes.Clear();
        }
    }
}