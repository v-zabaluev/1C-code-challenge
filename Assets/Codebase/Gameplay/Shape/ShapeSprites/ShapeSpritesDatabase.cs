using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase.Gameplay
{
    [CreateAssetMenu(fileName = "ShapeSpritesDatabase", menuName = "Configuration/SpritesDatabase")]
    public class ShapeSpritesDatabase : ScriptableObject
    {
        [Serializable]
        public class ShapeSpritesEntry
        {
            public ShapeType ShapeType;
            public List<Sprite> Sprites;
        }

        [SerializeField] private List<ShapeSpritesEntry> _entries;

        private Dictionary<ShapeType, List<Sprite>> _spriteLookup;

        public void Initialize()
        {
            _spriteLookup = new Dictionary<ShapeType, List<Sprite>>();

            foreach (var entry in _entries)
            {
                if (entry.Sprites == null || entry.Sprites.Count == 0)
                    continue;

                if (!_spriteLookup.ContainsKey(entry.ShapeType))
                    _spriteLookup[entry.ShapeType] = new List<Sprite>();

                _spriteLookup[entry.ShapeType].AddRange(entry.Sprites);
            }
        }
        
        public ShapeData GetRandomShapeData()
        {
            var types = new List<ShapeType>(_spriteLookup.Keys);
            var randomType = types[Random.Range(0, types.Count)];
            return GetRandomShapeData(randomType);
        }

        public ShapeData GetRandomShapeData(ShapeType type)
        {
            if (_spriteLookup == null)
                Initialize();

            if (_spriteLookup.TryGetValue(type, out var sprites) && sprites.Count > 0)
            {
                var sprite = sprites[Random.Range(0, sprites.Count)];

                return new ShapeData(type, sprite);
            }

            Debug.LogWarning($"No sprites found for shape type {type}");

            return null;
        }
    }
}