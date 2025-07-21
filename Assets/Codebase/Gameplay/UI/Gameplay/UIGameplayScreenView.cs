using Codebase.Gameplay.UI.ShowedValues;
using UnityEngine;

namespace Codebase.Gameplay.UI
{
    public class UIGamePlayScreenView : UIBaseScreenView
    {
        [SerializeField] private ShowedValue _points;
        [SerializeField] private ShowedValue _health;

        public ShowedValue Points => _points;
        public ShowedValue Health => _health;
    }
}