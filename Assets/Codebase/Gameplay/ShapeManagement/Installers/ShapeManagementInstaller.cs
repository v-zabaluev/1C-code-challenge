using Codebase.Gameplay.Factory;
using Codebase.Gameplay.Pool;
using UnityEngine;
using Zenject;

namespace Codebase.Gameplay.Installers
{
    public class ShapeManagementInstaller : MonoInstaller
    {
        [SerializeField] private Shape _shapePrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<ShapeData, Shape, ShapeFactory>()
                .FromPoolableMemoryPool<ShapeData, Shape, ShapePool>(poolBinder =>
                    poolBinder
                        .WithInitialSize(15)
                        .FromComponentInNewPrefab(_shapePrefab)
                        .UnderTransformGroup("Shapes"));
        }
    }
}