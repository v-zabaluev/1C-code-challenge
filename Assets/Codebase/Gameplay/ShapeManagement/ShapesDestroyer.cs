using System;
using UnityEngine;

namespace Codebase.Gameplay
{
    public class ShapesDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var shape = other.GetComponent<Shape>();
            if (shape != null)
            {
                shape.Dispose(); 
            }
        }
    }
}