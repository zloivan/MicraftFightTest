using _Project.Scripts.Utility.Extensions;
using UnityEngine;

namespace _Project.Scripts.ServiceLocatorSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ServiceLocator))]
    public abstract class Bootstrapper : MonoBehaviour
    {
        internal ServiceLocator Container => _container.OrNull() ?? (_container = GetComponent<ServiceLocator>());
        private ServiceLocator _container;

        private bool _hasBeenBootstrapped;

        public void BootstrapOnDemand()
        {
            if (_hasBeenBootstrapped) return;
            _hasBeenBootstrapped = true;
            Bootstrap();
        }

        protected abstract void Bootstrap();

        private void Awake()
        {
            BootstrapOnDemand();
        }
    }
}