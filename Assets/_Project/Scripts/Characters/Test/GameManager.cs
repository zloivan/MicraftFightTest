using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.AddressableSystem;
using _Project.Scripts.CameraControllSystem;
using _Project.Scripts.Configs;
using _Project.Scripts.ServiceLocatorSystem;
using _Project.Scripts.StatsAndBuffsSystem;
using _Project.Scripts.StatsSystem;
using _Project.Scripts.Utilities.Extensions;
using UnityEngine;
using Logger = Utilities.Logger;

namespace _Project.Scripts.Characters.Test
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Entity _playerOne;

        [SerializeField]
        private Entity _playerTwo;

        [SerializeField]
        private Transform _buffIconParent;

        [SerializeField]
        private GameObject _buffIconPrefab;

        [SerializeField]
        private CameraController _cameraController;


        private IBuffProvider _buffProvider;
        private IAddressableService _addressableService;
        private IDataProvider _dataProvider;

        private void Start()
        {
            _buffProvider = ServiceLocator.For(this).Get<IBuffProvider>();
            _addressableService = ServiceLocator.For(this).Get<IAddressableService>();
            _dataProvider = ServiceLocator.For(this).Get<IDataProvider>();


            _cameraController.SetupLookAtPosition((_playerOne.transform.position + _playerTwo.transform.position) / 2);
        }

        [ContextMenu("Apply Buffs")]
        public void ApplyBuffs()
        {
            _playerOne.Stats.Mediator.ClearBuffs();
            _playerTwo.Stats.Mediator.ClearBuffs();
            
            
            var buffsToApplie = _buffProvider.GetBuffs().ToList();

            var buffsForPlayerOne = buffsToApplie.TakeRandom(Random.Range(_dataProvider.Data.settings.buffCountMin,
                _dataProvider.Data.settings.buffCountMax));
            
            var buffsForPlayerTwo = buffsToApplie.TakeRandom(Random.Range(_dataProvider.Data.settings.buffCountMin,
                _dataProvider.Data.settings.buffCountMax));

            BuffApplier.ApplyBuffs(_playerOne, buffsForPlayerOne);
            BuffApplier.ApplyBuffs(_playerTwo, buffsForPlayerTwo);

            Debug.Log($"Player One: {_playerOne.Stats}");
            Debug.Log($"Player Two: {_playerTwo.Stats}");
        }


        [ContextMenu("Reset to Base")]
        public void TestResetStats()
        {
            _playerOne.Stats.Mediator.ClearBuffs();
            Debug.Log($"{_playerOne.Stats}");
        }

        [ContextMenu("Output buffs")]
        public void Output()
        {
            Logger.Log($"{string.Join('\n', _playerOne.Stats.Mediator.ActiveBuffs.Select(e => e.Name))}", Color.green);
            Logger.Log($"{string.Join('\n', _playerTwo.Stats.Mediator.ActiveBuffs.Select(e => e.Name))}", Color.green);
        }

        [ContextMenu("Player One Attack player Two")]
        public void PlayerOneAttack()
        {
            var attackCommand = new AttackCommand.Builder(new List<IEntity> { _playerTwo })
                .WithAction(playerTwo =>
                {
                    Logger.Log($"Player One attack player two", Color.red);
                    playerTwo.CurrentHealth -= _playerOne.Stats.GetStat(StatType.Damage);
                    Logger.Log($"{playerTwo.CurrentHealth}", Color.green);
                })
                .Build();

            _playerOne.EnqueueCammand(attackCommand);
        }


        [ContextMenu("Player Two Attack player One")]
        public void PlayerTwoAttack()
        {
            var attackCommand = new AttackCommand.Builder(new List<IEntity> { _playerOne })
                .WithAction(_playerOne =>
                {
                    Logger.Log($"Player Two attack player One", Color.red);
                    _playerOne.CurrentHealth -= _playerTwo.Stats.GetStat(StatType.Damage);
                    Logger.Log($"{_playerOne.CurrentHealth}", Color.green);
                })
                .Build();

            _playerOne.EnqueueCammand(attackCommand);
        }
    }
}