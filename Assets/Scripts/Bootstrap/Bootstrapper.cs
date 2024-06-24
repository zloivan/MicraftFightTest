using System;
using System.Threading;
using Controllers;
using Data;
using UnityEngine;

namespace Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField]
        private string _dataFileName = "data";

        [SerializeField]
        private string _iconsFolderPath = "Icons";

        [SerializeField]
        private string _nextSceneName;

        [SerializeField]
        private CameraController _cameraController;


        private PlayerIconsProvider _playerStatPanelPlayerIconsProvider;
        private DataProviderFromAddressables _dataProviderFromAddressables;
        private AddressablesService _addressablesService;

        private void Awake()
        {
            _addressablesService = new AddressablesService();
            ServiceLocator.RegisterService(_addressablesService);
            _dataProviderFromAddressables = new DataProviderFromAddressables(_dataFileName);
            ServiceLocator.RegisterService(_dataProviderFromAddressables);
            _playerStatPanelPlayerIconsProvider = new PlayerIconsProvider(_iconsFolderPath);
            ServiceLocator.RegisterService(_playerStatPanelPlayerIconsProvider);
            ServiceLocator.RegisterService(_cameraController);
        }

        private async void Start()
        {
            await _dataProviderFromAddressables.LoadData(CancellationToken.None);
            await _playerStatPanelPlayerIconsProvider.LoadSprites(CancellationToken.None);

            _cameraController.SetupModel(_dataProviderFromAddressables.Data.cameraSettings);

            await _addressablesService.LoadSceneAdditiveAsync(_nextSceneName);
        }
    }
}