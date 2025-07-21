using System;
using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Codebase.Infrastructure.Services.Health;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Codebase.Gameplay
{
    public class ShapesDestroyer : MonoBehaviour
    {
        [Inject] private HealthService _healthService;
        [SerializeField] private int _damage = -1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var shape = other.GetComponent<Shape>();

            if (shape != null)
            {
                shape.Dispose();

                _healthService.ChangeHealth(_damage);
            }
        }
    }
}