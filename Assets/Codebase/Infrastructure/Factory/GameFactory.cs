using System.Collections.Generic;
using Codebase.Infrastructure.AssetManagement;
using UnityEngine;


namespace Codebase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        public GameObject CreateHeroAt(Transform initialPoint)
        {
            return _assetProvider.InstantiateFromResources("HeroPath", initialPoint.position);
        }
        
    }
}