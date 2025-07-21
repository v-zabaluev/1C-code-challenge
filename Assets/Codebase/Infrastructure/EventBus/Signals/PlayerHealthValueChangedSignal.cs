namespace Codebase.Infrastructure.EventBus.Signals
{
    public class PlayerHealthValueChangedSignal
    {
        public readonly int Health;

        public PlayerHealthValueChangedSignal(int health)
        {
            Health = health;
        }
    }
}