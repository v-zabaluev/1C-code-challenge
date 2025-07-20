using Codebase.Gameplay.ShapeSpawner.Factory;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Codebase.Gameplay.ShapeSpawner.Installers
{
    public class ShapeSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private ShapeSpawner _shapePrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ShapeSpawnerLimiter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ShapeSpawnerManager>().AsSingle().NonLazy();

            Container.BindFactory<ShapeSpawner, ShapeSpawnerFactory>().FromComponentInNewPrefab(_shapePrefab);
        }
    }
}