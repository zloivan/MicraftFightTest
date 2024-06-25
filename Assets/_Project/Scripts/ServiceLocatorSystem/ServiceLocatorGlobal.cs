using UnityEngine;

namespace _Project.Scripts.ServiceLocatorSystem
{
    [AddComponentMenu("ServiceLocator/ServiceLocator Global")]
    public class ServiceLocatorGlobal : Bootstrapper
    {
        [SerializeField]
        private bool _dontDestroyOnLoad = true;

        protected override void Bootstrap()
        {
            Container.ConfigureAsGlobal(_dontDestroyOnLoad);
        }
    }
}