using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Zenject;

namespace Codebase.Infrastructure.Services.Health
{
    public class HealthService
    {
        [Inject] private SimpleEventBus _eventBus;
        private int _health;

        public void Initialize(int health)
        {
            _health = health;
            InvokeHealthChanged();
        }

        public void ChangeHealth(int health)
        {
            _health += health;
            InvokeHealthChanged();
        }

        private void InvokeHealthChanged()
        {
            _eventBus.Invoke(new PlayerHealthValueChangedSignal(_health));
        }
    }
}