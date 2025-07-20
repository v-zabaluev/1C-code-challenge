using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.States.Installers
{
    public class StatesMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BootstrapState>().AsSingle().NonLazy();
            Container.Bind<LoadLevelState>().AsSingle().NonLazy();
            Container.Bind<GameLoopState>().AsSingle().NonLazy();

            Container
                .BindInterfacesAndSelfTo<GameStateMachine>()
                .AsSingle();

            Debug.Log($"State machine installer registered");
        }
    }
}