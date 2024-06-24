using Bootstrap;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scripts.Views
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image _fill;

        private Camera _camera;

        private void Awake()
        {
            _camera = ServiceLocator.GetService<CameraController>().MainCamera;
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