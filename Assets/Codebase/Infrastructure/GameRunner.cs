using UnityEngine;
using UnityEngine.Serialization;

namespace Codebase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _bootstrapperPrefab;
        private void Awake()
        {
            var bootstrapper = FindFirstObjectByType<GameBootstrapper>();

            if (bootstrapper == null)
            {
                bootstrapper = Instantiate(_bootstrapperPrefab);
            }
        }
    }
}