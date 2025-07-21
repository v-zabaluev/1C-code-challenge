using System;
using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Zenject;

namespace Codebase.Gameplay.UI.ShowedValues
{
    public class ScoreShowedValue : ShowedValue
    {
        [Inject] private SimpleEventBus _eventBus;

        private void Awake()
        {
            _eventBus.Subscribe<PlayerScoreChangedSignal>(DisplayScore);
        }

        private void DisplayScore(PlayerScoreChangedSignal signal)
        {
            SetValue(signal.Score.ToString());
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlayerScoreChangedSignal>(DisplayScore);
        }
    }
}