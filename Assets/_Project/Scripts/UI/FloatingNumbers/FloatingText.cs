using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.FloatingNumbers
{
    public class FloatingText : MonoBehaviour
    {
        public float moveDistance = 200f;
        public float duration = 2f;

        private TMP_Text _text;
        private DamageNumbers _damageNumbers;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _damageNumbers = GetComponentInParent<DamageNumbers>();
        }

        private void OnEnable()
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
            transform.localPosition = Vector3.zero;

            transform.DOLocalMoveY(transform.localPosition.y + moveDistance, duration).SetEase(Ease.OutCubic);
            _text.DOFade(0, duration).OnComplete(() => 
            {
                _damageNumbers.ReturnToPool(gameObject);
            });
        }

        private void OnDisable()
        {
            transform.DOKill();
            _text.DOKill();
        }
    }
}