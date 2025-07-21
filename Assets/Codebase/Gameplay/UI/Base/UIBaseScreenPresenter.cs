using UniRx;
using Zenject;

namespace Codebase.Gameplay.UI
{
    public abstract class UIBaseScreenPresenter<TView> where TView : IView
    {
        [Inject] protected TView _view;

        protected CompositeDisposable _disposable = new CompositeDisposable();
        
        public virtual void Show()
        {
            _view.Show();
        }

        public virtual void Hide()
        {
            _view.Hide();
            _disposable.Dispose();
        }
        
        
    }
}