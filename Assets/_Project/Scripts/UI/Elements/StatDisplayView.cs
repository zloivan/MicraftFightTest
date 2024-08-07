using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Elements
{
    public class StatDisplayView : MonoBehaviour
    {
        [SerializeField]
        private Image _statIcon;

        [SerializeField]
        private TMP_Text _statName;


        public void Setup(Sprite sprite, string statName)
        {
            _statIcon.sprite = sprite;
            _statName.text = statName;
        }
    }
}