using Zenject;

namespace Codebase.Gameplay.UI
{
    public abstract class UIBaseScreenPresenter<TView> where TView : IView
    {
        [Inject] protected TView _view;

        public virtual void Show()
        {
            _view.Show();
        }

        public virtual void Hide()
        {
            _view.Hide();
        }
        
        
    }
}