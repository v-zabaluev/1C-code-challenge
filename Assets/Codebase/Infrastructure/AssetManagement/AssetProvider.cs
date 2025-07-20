using UnityEngine;

namespace Codebase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject InstantiateFromResources(string path)
        {
            var objectPrefab = Resources.Load<GameObject>(path);

            return Object.Instantiate(objectPrefab);
        }

        public GameObject InstantiateFromResources(string path, Vector3 position)
        {
            var objectPrefab = Resources.Load<GameObject>(path);

            return Object.Instantiate(objectPrefab, position, Quaternion.identity);
        }
    }
}

