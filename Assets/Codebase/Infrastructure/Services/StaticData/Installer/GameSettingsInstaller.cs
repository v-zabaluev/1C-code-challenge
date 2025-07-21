using Codebase.Infrastructure.Services.StaticData.Data.Data;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.Services.StaticData.Data.Installer
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField]
        private GameSettings _gameSettings;
        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle().NonLazy();
        }
    }
}