namespace Codebase.Infrastructure.EventBus.Signals
{
    public class GameOverSignal
    {
        public readonly GameStatus Status;

        public GameOverSignal(GameStatus status)
        {
            Status = status;
        }
    }

    public enum GameStatus
    {
        Win = 0,
        Lose = 1
    }
}