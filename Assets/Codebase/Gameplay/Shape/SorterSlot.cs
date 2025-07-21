using UnityEngine;

namespace Codebase.Gameplay
{
    public class SorterSlot : MonoBehaviour
    {
        [SerializeField] private ShapeType _shapeType;
        public ShapeType Type => _shapeType;
    }
}