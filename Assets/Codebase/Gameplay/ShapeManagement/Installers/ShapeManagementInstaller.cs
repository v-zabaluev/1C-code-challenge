using Codebase.Gameplay.Factory;
using Codebase.Gameplay.Pool;
using Codebase.Infrastructure.GameController;
using UnityEngine;
using Zenject;

namespace Codebase.Gameplay.Installers
{
    public class ShapeManagementInstaller : MonoInstaller
    {
        [SerializeField] private Shape _shapePrefab;

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Shape, ShapePool>()
                .WithInitialSize(15)
                .FromComponentInNewPrefab(_shapePrefab)
                .UnderTransformGroup("Shapes");

            Container.BindIFactory<ShapeData, Shape, ShapeFactory>()
                .To<ShapeFactory>()
                .FromResolve();

            // Биндим сам ShapeFactory
            Container.Bind<ShapeFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameResultController>().AsSingle();
        }
    }
}