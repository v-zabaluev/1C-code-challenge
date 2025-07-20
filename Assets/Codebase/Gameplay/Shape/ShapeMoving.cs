using System;
using UnityEngine;

namespace Codebase.Gameplay
{
    public class ShapeMoving : MonoBehaviour
    {
        private float _moveSpeed = 5f;
        private Vector2 _direction;
        private bool _isMoving = false;

        public void Initialize(float speed, Vector2 direction)
        {
            _moveSpeed = speed;
            _direction = direction;
        }

        public void ResetState()
        {
            _isMoving = false;
            _direction = Vector2.right;
        }

        public void StartMoving() => _isMoving = true;

        public void StopMoving() => _isMoving = false;

        private void Update()
        {
            if (_isMoving)
                transform.Translate(_direction * (_moveSpeed * Time.deltaTime));
        }
    }
}