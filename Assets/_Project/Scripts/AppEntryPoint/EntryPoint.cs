using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.AbilitySystem;
using _Project.Scripts.AddressableSystem;
using _Project.Scripts.CombatSystem;
using _Project.Scripts.CombatSystem.abstractions;
using _Project.Scripts.CombatSystem.CombatModifiers;
using _Project.Scripts.Configs;
using _Project.Scripts.PickupSystem;
using _Project.Scripts.StatsAndBuffsSystem;
using UnityEngine;
using Utilities.ServiceLocator;

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

        private readonly List<IInitializeble> _initializbles = new();


        private void Awake()
        {
            RegisterDataProviders();

            RegisterStatBuffsSystem();

            var combatController = RegisterCombatSystem();

            RegisterAbilitySystem(combatController);
        }

        private static void RegisterAbilitySystem(DamageProcessor damageProcessor)
        {
            ServiceLocator.Global.Register<IAbilityFactory>(new AbilityFactory(damageProcessor));
        }

        private void RegisterDataProviders()
        {
            RegisterAddressablesSystem();
            RegisterConfigProvider();
            RegisterPlayerIconProvider();
        }

        private DamageProcessor RegisterCombatSystem()
        {
            var combatModifiers = new List<ICombatModifier>
            {
                new CombatCriticalHitModifier()
            };
            var damageProcessor = new DamageProcessor(combatModifiers);
            
            ServiceLocator.Global.Register<IDamageProcessor>(damageProcessor);

            return damageProcessor;
        }

        private void RegisterStatBuffsSystem()
        {
            ServiceLocator.Global.Register<IStatBuffFactory>(new StatBuffFactory());

            var addressableBuffProvider = new AddressableBuffProvider("baseBuff",
                _addressablesService);

            ServiceLocator.Global.Register<IBuffProvider>(addressableBuffProvider);
            _initializbles.Add(addressableBuffProvider);
        }

        private void RegisterPlayerIconProvider()
        {
            var playerStatPanelPlayerIconsProvider = new PlayerIconsProvider(_iconsFolderPath);
            ServiceLocator.Global.Register<IPlayerIconProvider>(playerStatPanelPlayerIconsProvider);
            _initializbles.Add(playerStatPanelPlayerIconsProvider);
        }

        private void RegisterConfigProvider()
        {
            var dataProviderFromAddressables = new ConfigProviderFromAddressables(_dataFileName);
            ServiceLocator.Global.Register<IConfigProvider>(dataProviderFromAddressables);
            _initializbles.Add(dataProviderFromAddressables);
        }

        private void RegisterAddressablesSystem()
        {
            _addressablesService = new AddressablesService();
            ServiceLocator.Global.Register<IAddressableService>(_addressablesService);
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