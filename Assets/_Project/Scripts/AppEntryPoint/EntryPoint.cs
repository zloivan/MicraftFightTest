using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.AbilitySystem;
using _Project.Scripts.AddressableSystem;
using _Project.Scripts.Characters;
using _Project.Scripts.Characters.Test;
using _Project.Scripts.CombatSystem;
using _Project.Scripts.CombatSystem.abstractions;
using _Project.Scripts.CombatSystem.CombatModifiers;
using _Project.Scripts.Configs;
using _Project.Scripts.PickupSystem;
using _Project.Scripts.ServiceLocatorSystem;
using _Project.Scripts.StatsAndBuffsSystem;
using _Project.Scripts.StatsSystem;
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

        [SerializeField]
        private DirectFromSceneBuffProvider _directFromSceneBuffProvider;


        //private DataProviderFromAddressables _dataProviderFromAddressables;
        //private PlayerIconsProvider _playerStatPanelPlayerIconsProvider;
        private readonly List<IInitializeble> _initializbles = new();

        private void Awake()
        {
            _addressablesService = new AddressablesService();
            ServiceLocator.Global.Register<IAddressableService>(_addressablesService);

            var dataProviderFromAddressables = new DataProviderFromAddressables(_dataFileName);
            ServiceLocator.Global.Register<IDataProvider>(dataProviderFromAddressables);
            _initializbles.Add(dataProviderFromAddressables);

            var playerStatPanelPlayerIconsProvider = new PlayerIconsProvider(_iconsFolderPath);
            ServiceLocator.Global.Register<IPlayerIconProvider>(playerStatPanelPlayerIconsProvider);
            _initializbles.Add(playerStatPanelPlayerIconsProvider);

            ServiceLocator.Global.Register<IStatBuffFactory>(new StatBuffFactory());

            var addressableBuffProvider = new AddressableBuffProvider("baseBuff",
                _addressablesService);

            ServiceLocator.Global.Register<IBuffProvider>(addressableBuffProvider);
            _initializbles.Add(addressableBuffProvider);


            var combatController = new CombatController(
                new List<ICombatModifier> { new CombatCriticalHitModifier() });
            
            ServiceLocator.Global.Register<ICombatController>(combatController);
            ServiceLocator.Global.Register<IAbilityFactory>(new AbilityFactory(combatController));
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