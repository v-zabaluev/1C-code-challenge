using UnityEngine;

namespace Codebase.Gameplay
{
    public class ShapeData
    {
        public ShapeType ShapeType;
        public Sprite ShapeSprite;

        public ShapeData(ShapeType type, Sprite sprite)
        {
            ShapeType = type;
            ShapeSprite = sprite;
        }
    }
}