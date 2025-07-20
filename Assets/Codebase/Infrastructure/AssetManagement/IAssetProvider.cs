using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject InstantiateFromResources(string path);
        GameObject InstantiateFromResources(string path, Vector3 position);
    }
}