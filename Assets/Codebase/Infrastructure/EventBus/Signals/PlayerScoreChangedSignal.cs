namespace Codebase.Infrastructure.EventBus.Signals
{
    public class PlayerScoreChangedSignal
    {
        public readonly int Score;

        public PlayerScoreChangedSignal(int health)
        {
            Score = health;
        }
    }
}