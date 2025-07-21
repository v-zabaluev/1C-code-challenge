using Codebase.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Codebase.Infrastructure.Services.StaticData.Data.Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Configuration/Game", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [Header("Winning Condition")]
        public IntRangeValues ShapesCountRange = new IntRangeValues(10, 20);

        [Header("Spawn Settings")]
        public FloatRangeValues SpawnTimeoutRange = new FloatRangeValues(0.5f, 2f);

        [Header("Shape Settings")]
        public FloatRangeValues MovementSpeedRange = new FloatRangeValues(1f, 5f);
        
        [Header("Player Settings")]
        public int PlayerHealth = 3;
        
        private void OnValidate()
        {
            ShapesCountRange.ClampValues();
            SpawnTimeoutRange.ClampValues();
            MovementSpeedRange.ClampValues();
        }
    }
}