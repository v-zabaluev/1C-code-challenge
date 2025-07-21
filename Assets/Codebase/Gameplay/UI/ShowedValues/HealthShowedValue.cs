using System;
using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Zenject;

namespace Codebase.Gameplay.UI.ShowedValues
{
    public class HealthShowedValue : ShowedValue
    {
        [Inject] private SimpleEventBus _eventBus;

        private void Awake()
        {
            _eventBus.Subscribe<PlayerHealthValueChangedSignal>(DisplayHealth);
        }

        private void DisplayHealth(PlayerHealthValueChangedSignal signal)
        {
            SetValue(signal.Health.ToString());
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlayerHealthValueChangedSignal>(DisplayHealth);
        }
    }
}