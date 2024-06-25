using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Elements
{
    public class StatBuffView : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _valueText;

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