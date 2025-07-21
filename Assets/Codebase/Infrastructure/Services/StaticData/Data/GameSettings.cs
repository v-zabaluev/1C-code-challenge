using Codebase.Utils;
using UnityEngine;

namespace Codebase.Infrastructure.Services.StaticData.Data.Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Configuration/Game", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [Header("Winning Condition")]
        public FloatRangeValues SortTargetRange = new FloatRangeValues(10f, 20f);

        [Header("Spawn Settings")]
        public FloatRangeValues SpawnTimeoutRange = new FloatRangeValues(0.5f, 2f);

        [Header("Shape Settings")]
        public FloatRangeValues MovementSpeedRange = new FloatRangeValues(1f, 5f);
        
        [Header("Player Settings")]
        public int PlayerHealth = 3;
        
        private void OnValidate()
        {
            SortTargetRange.ClampValues();
            SpawnTimeoutRange.ClampValues();
            MovementSpeedRange.ClampValues();
        }
    }
}