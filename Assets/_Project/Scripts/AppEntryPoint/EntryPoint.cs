using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.AddressableSystem;
using _Project.Scripts.ServiceLocatorSystem;
using _Project.Scripts.StatsAndBuffsSystem;
using UnityEngine;

namespace _Project.Scripts.AppEntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        private AddressablesService _addressablesService;

        [SerializeField]
        private string _dataFileName = "data";

        [SerializeField]
        private string _iconsFolderPath = "Icons";

        [SerializeField]
        private string _nextSceneName = "Main";

        private DataProviderFromAddressables _dataProviderFromAddressables;
        private PlayerIconsProvider _playerStatPanelPlayerIconsProvider;
        private readonly List<IInitializeble> _initializbles = new();

        private void Awake()
        {
            _addressablesService = new AddressablesService();
            ServiceLocator.Global.Register<IAddressableService>(_addressablesService);
            
            _dataProviderFromAddressables = new DataProviderFromAddressables(_dataFileName);
            ServiceLocator.Global.Register<IDataProvider>(_dataProviderFromAddressables);
            _initializbles.Add(_dataProviderFromAddressables);
            
            _playerStatPanelPlayerIconsProvider = new PlayerIconsProvider(_iconsFolderPath);
            ServiceLocator.Global.Register<IPlayerIconProvider>(_playerStatPanelPlayerIconsProvider);
            _initializbles.Add(_playerStatPanelPlayerIconsProvider);
        }

        private async void Start()
        {
            foreach (var initializeble in _initializbles)
            {
                await initializeble.Initialize(CancellationToken.None);
            }

            await _addressablesService.ChangeSceneAsync(_nextSceneName);
        }
    }
}