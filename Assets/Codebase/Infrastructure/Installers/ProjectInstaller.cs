using Codebase.Gameplay;
using Codebase.Infrastructure.States.Factory;
using Codebase.Loading;
using UnityEngine;
using Zenject;
using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.Services.Health;

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
            Container.Bind<SimpleEventBus>().To<SimpleEventBus>().AsSingle().NonLazy();

            BindServices();
            Debug.Log($"Project installer binding complete");
        }

        private void BindServices()
        {
            Container.Bind<HealthService>().To<HealthService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle().NonLazy();
        }
    }
}