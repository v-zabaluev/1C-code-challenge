using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Codebase.Gameplay.UI
{
    public class UIScreensInstaller : MonoInstaller
    {
        [SerializeField] private UIGamePlayScreenView _uiGamePlayScreenView;

        public override void InstallBindings()
        {
            Container.Bind<UIGamePlayScreenView>().FromInstance(_uiGamePlayScreenView).AsSingle();
            Container.Bind<UIGameplayScreenPresenter>().To<UIGameplayScreenPresenter>().AsSingle().NonLazy();
        }
    }
}