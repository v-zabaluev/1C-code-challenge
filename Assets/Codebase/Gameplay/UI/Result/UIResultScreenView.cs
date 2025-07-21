using System;
using Codebase.Infrastructure.EventBus.Signals;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Gameplay.UI.Result
{
    public class UIResultScreenView : UIBaseScreenView
    {
        private const string WIN_RESULT_TEXT = "Победа";
        private const string LOSE_RESULT_TEXT = "Поражение";
        
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _restartButton;
        
        public IObservable<Unit> OnRestartButtonClicked() => _restartButton.OnClickAsObservable();

        public void SetGameResultView(GameStatus signalStatus, int scoreServiceScore)
        {
            if (signalStatus == GameStatus.Win)
            {
                _resultText.text = WIN_RESULT_TEXT;
                _scoreText.gameObject.SetActive(true);
                _scoreText.text = scoreServiceScore.ToString();
            }
            else
            {
                _resultText.text = LOSE_RESULT_TEXT;
                _scoreText.gameObject.SetActive(false);
            }
        }
    }
}