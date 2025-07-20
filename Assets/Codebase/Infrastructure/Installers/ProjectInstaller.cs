using Codebase.Infrastructure.States.Factory;
using Codebase.Loading;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;

        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().To<SceneLoader>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<LoadingCurtain>()
                .FromComponentInNewPrefab(_loadingCurtainPrefab)
                .WithGameObjectName("Curtain")
                .UnderTransformGroup("Infrastructure")
                .AsSingle().NonLazy();

            Container.Bind<StateFactory>().AsSingle();
            
            Debug.Log($"Project installer binding complete");
        }
    }
}