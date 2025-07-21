namespace Codebase.Infrastructure.EventBus.Signals
{
    public class PlayerHealthChangedSignal
    {
        public readonly int Health;

        public PlayerHealthChangedSignal(int health)
        {
            Health = health;
        }
    }
}