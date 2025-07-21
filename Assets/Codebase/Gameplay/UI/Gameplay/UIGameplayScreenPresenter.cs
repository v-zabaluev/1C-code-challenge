using System;
using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Codebase.Infrastructure.Services.StaticData.Data.Data;
using UniRx;
using Zenject;

namespace Codebase.Gameplay.UI
{
    public class UIGameplayScreenPresenter : UIBaseScreenPresenter<UIGamePlayScreenView>, IInitializable, IDisposable
    {
        [Inject] private SimpleEventBus _eventBus;

        public void Initialize()
        {
            _eventBus.Subscribe<GameOverSignal>(HideScreen);
            _eventBus.Subscribe<GameRestartSignal>(ShowScreen);
        }

        private void ShowScreen(GameRestartSignal obj)
        {
            Show();
        }

        private void HideScreen(GameOverSignal signal)
        {
            Hide();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<GameOverSignal>(HideScreen);
            _eventBus.Unsubscribe<GameRestartSignal>(ShowScreen);
        }
    }
}