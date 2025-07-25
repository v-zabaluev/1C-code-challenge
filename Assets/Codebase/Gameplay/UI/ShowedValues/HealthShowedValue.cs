﻿using System;
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
            _eventBus.Subscribe<PlayerHealthChangedSignal>(DisplayHealth);
        }

        private void DisplayHealth(PlayerHealthChangedSignal signal)
        {
            SetValue(signal.Health.ToString());
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlayerHealthChangedSignal>(DisplayHealth);
        }
    }
}