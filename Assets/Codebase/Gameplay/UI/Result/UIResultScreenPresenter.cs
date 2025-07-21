using System;
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
    public class UIResultScreenPresenter : UIBaseScreenPresenter<UIResultScreenView>, IInitializable, IDisposable
    {
        [Inject] private SimpleEventBus _eventBus;
        [Inject] private ScoreService _scoreService;

        private CompositeDisposable _disposable;

        public void Initialize()
        {
            _disposable = new CompositeDisposable();
            _eventBus.Subscribe<GameOverSignal>(OnGameOver);
            _eventBus.Subscribe<GameRestartSignal>(OnGameRestart);

            _view.OnRestartButtonClicked().Subscribe(_ => { _eventBus.Invoke(new GameRestartSignal()); })
                .AddTo(_disposable);
            
            Hide();
        }

        private void OnGameOver(GameOverSignal signal)
        {
            _view.SetGameResultView(signal.Status, _scoreService.Score);
            Show();
        }

        private void OnGameRestart(GameRestartSignal obj)
        {
            Hide();
        }

        public void Dispose()
        {
            _disposable.Dispose();

            _eventBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _eventBus.Unsubscribe<GameRestartSignal>(OnGameRestart);
        }
    }
}