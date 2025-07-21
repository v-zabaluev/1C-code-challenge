using Codebase.Infrastructure.Services.StaticData.Data.Data;
using Zenject;

namespace Codebase.Gameplay.UI
{
    public class UIGameplayScreenPresenter : UIBaseScreenPresenter<UIGamePlayScreenView>
    {
        [Inject] private GameSettings _gameSettings;

        public override void Show()
        {
            _view.Health.SetValue(_gameSettings.PlayerHealth.ToString());
            base.Show();
        }
    }
}