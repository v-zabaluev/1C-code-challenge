using UnityEngine;

namespace Codebase.Gameplay.UI
{
    public class UIBaseScreenView : MonoBehaviour, IView
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
    
    public interface IView
    {
        void Show();
        void Hide();
    }
}