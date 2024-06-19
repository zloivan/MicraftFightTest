using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class StatBuffView : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Text _valueText;

        public void SetData(Sprite icon, string value)
        {
            _iconImage.sprite = icon;
            _valueText.text = value;
        }

        public void UpdateValue(string value)
        {
            _valueText.text = value;
        }
    }
}