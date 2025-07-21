using Codebase.Infrastructure.Services.StaticData.Data.Data;
using Zenject;

namespace Codebase.Gameplay.UI
{
    public class UIGameplayScreenPresenter : UIBaseScreenPresenter<UIGamePlayScreenView>
    {

        public override void Show()
        {
            base.Show();
        }

        public void InitializeScreenData(int healthValue, int pointsValue)
        {
            SetHealthValue(healthValue);
            SetPointsValue(pointsValue);
        }

        private void SetPointsValue(int pointsValue)
        {
            _view.Points.SetValue(pointsValue.ToString());
        }

        private void SetHealthValue(int healthValue)
        {
            _view.Health.SetValue(healthValue.ToString());
        }
    }
}