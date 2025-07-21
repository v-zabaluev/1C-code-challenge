using TMPro;
using UnityEngine;

namespace Codebase.Gameplay.UI.ShowedValues
{
    public class ShowedValue : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _text;
        public virtual void SetValue(string value)
        {
            _text.text = value;
        }
    }
}