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
        [Inject] private ShapeSpritesDatabase _spritesDatabase;
        
        public Shape Spawn(float speed)
        {
            var data = _spritesDatabase.GetRandomShapeData();
            Shape shape = _shapeFactory.CreateAt(transform.position, data);
            
            shape.Initialize(speed);
            shape.StartMovement();

            return shape;
        }
    }
}