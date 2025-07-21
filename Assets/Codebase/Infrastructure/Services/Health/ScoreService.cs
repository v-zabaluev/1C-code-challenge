using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Zenject;

namespace Codebase.Infrastructure.Services.Health
{
    public class ScoreService
    {
        [Inject] private SimpleEventBus _eventBus;
        private int _score;

        public void Initialize(int score)
        {
            _score = score;
            InvokeScoreChanged();
        }

        public void ChangeScore(int score)
        {
            _score += score;
            InvokeScoreChanged();
        }

        private void InvokeScoreChanged()
        {
            _eventBus.Invoke(new PlayerScoreChangedSignal(_score));
        }
    }
}