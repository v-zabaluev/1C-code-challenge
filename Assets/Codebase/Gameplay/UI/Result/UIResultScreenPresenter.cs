using Codebase.Infrastructure.EventBus;
using Codebase.Infrastructure.EventBus.Signals;
using Codebase.Infrastructure.Services.Health;
using Codebase.Infrastructure.States;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Codebase.Gameplay.UI.Result
{
    public class UIResultScreenPresenter : UIBaseScreenPresenter<UIResultScreenView>, IInitializable
    {
        [Inject] private SimpleEventBus _eventBus;
        [Inject] private ScoreService _scoreService;
        public void Initialize()
        {
            _eventBus.Subscribe<GameOverSignal>(OnGameOver);

            _view.OnRestartButtonClicked().Subscribe(_ =>
            {
                Debug.Log("Restart");
            }).AddTo(_disposable);
            
            Hide();
        }

        private void OnGameOver(GameOverSignal signal)
        {
            _view.SetGameResultView(signal.Status, _scoreService.Score);
            Show();
        }
    }
}