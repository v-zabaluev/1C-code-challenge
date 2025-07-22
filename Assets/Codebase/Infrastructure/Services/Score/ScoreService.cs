using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Zenject;

namespace Codebase.Infrastructure.Services.Score
{
    public class ScoreService : IService
    {
        [Inject] private SimpleEventBus _eventBus;
        private int _score;
        public int Score => _score;

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