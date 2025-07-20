using System;
using System.Threading;
using Codebase.Gameplay.Factory;
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

        public void Spawn(float speed)
        {
            Shape shape = _shapeFactory.CreateAt(transform.position);
            shape.Initialize(speed);
            shape.StartMovement();
        }
    }
}