using System;
using Code.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image _fill;

        private Camera _camera;

        private void Start()
        {
            _camera = ServiceLocator.GetService<Camera>();
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
        }

        public void SetValue(int health, int maxHealth)
        {
            _fill.fillAmount = (float)health / maxHealth;
        }
    }
}