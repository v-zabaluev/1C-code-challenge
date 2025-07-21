using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Codebase.Infrastructure.Services.StaticData.Data.Data;
using Zenject;

namespace Codebase.Gameplay.UI
{
    public class UIGameplayScreenPresenter : UIBaseScreenPresenter<UIGamePlayScreenView>, IInitializable
    {
        [Inject] private SimpleEventBus _eventBus;

        public void Initialize()
        {
            _eventBus.Subscribe<GameOverSignal>(HideScreen);
        }

        private void HideScreen(GameOverSignal signal)
        {
            Hide();
        }
    }
}