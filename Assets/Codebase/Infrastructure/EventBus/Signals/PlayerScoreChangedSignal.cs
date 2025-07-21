namespace Codebase.Infrastructure.EventBus.Signals
{
    public class PlayerScoreChangedSignal
    {
        public readonly int Score;

        public PlayerScoreChangedSignal(int points)
        {
            Score = points;
        }
    }
}