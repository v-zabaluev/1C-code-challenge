using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Codebase.Gameplay.ShapeSpawner.Factory
{
    public class ShapeSpawnerFactory : PlaceholderFactory<ShapeSpawner>
    {
        private readonly List<ShapeSpawner> _createdSpawners = new();

        public List<ShapeSpawner> CreatedSpawners => _createdSpawners;

        public ShapeSpawner CreateAt(Vector3 position)
        {
            ShapeSpawner spawner = Create();
            spawner.transform.position = position;
            _createdSpawners.Add(spawner);

            return spawner;
        }
    }
}