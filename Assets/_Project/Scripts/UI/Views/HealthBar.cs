using UnityEngine;
using UnityEngine.UI;
using Utilities.ServiceLocator;

namespace _Project.Scripts.UI.Views
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image _fill;

        private Camera _camera;

        private void Start()
        {
            if (ServiceLocator.For(this).TryGet(out Camera camera))
            {
                _camera = camera;
            }
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