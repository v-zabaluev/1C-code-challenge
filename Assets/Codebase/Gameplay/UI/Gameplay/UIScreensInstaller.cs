using Codebase.Gameplay.UI.Result;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Codebase.Gameplay.UI
{
    public class UIScreensInstaller : MonoInstaller
    {
        [SerializeField] private UIGamePlayScreenView _uiGamePlayScreenView;
        [SerializeField] private UIResultScreenView _resultScreenView;

        public override void InstallBindings()
        {
            Container.Bind<UIGamePlayScreenView>().FromInstance(_uiGamePlayScreenView).AsSingle();
            Container.BindInterfacesAndSelfTo<UIGameplayScreenPresenter>().AsSingle().NonLazy();
            
            Container.Bind<UIResultScreenView>().FromInstance(_resultScreenView).AsSingle();
            Container.BindInterfacesAndSelfTo<UIResultScreenPresenter>().AsSingle().NonLazy();
        }
    }
}