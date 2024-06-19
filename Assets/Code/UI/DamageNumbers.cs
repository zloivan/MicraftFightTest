using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Code.Utility;

namespace Code.UI
{
    public class DamageNumbers : MonoBehaviour
    {
        public GameObject floatingTextPrefab;
        public int poolSize = 10;

        private readonly Queue<GameObject> _floatingTextPool = new();

        [SerializeField]
        private Canvas _displayCanvas;

        private Camera _camera;

        private void Start()
        {
            _camera = ServiceLocator.GetService<Camera>();
            for (var i = 0; i < poolSize; i++)
            {
                var instance = Instantiate(floatingTextPrefab, _displayCanvas.transform);
                instance.SetActive(false);
                _floatingTextPool.Enqueue(instance);
            }
        }

        public void ShowFloatingText(Vector3 worldPosition, string text, Color color)
        {
            if (_floatingTextPool.Count > 0)
            {
                var instance = _floatingTextPool.Dequeue();
                instance.transform.position = _camera.WorldToScreenPoint(worldPosition);
                var tmpText = instance.GetComponent<TMP_Text>();
                tmpText.text = text;
                tmpText.color = color;
                instance.SetActive(true);

                var floatingText = instance.GetComponent<FloatingText>();
                floatingText.enabled = true;
            }
        }

        public void ReturnToPool(GameObject instance)
        {
            instance.SetActive(false);
            _floatingTextPool.Enqueue(instance);
        }
    }
}