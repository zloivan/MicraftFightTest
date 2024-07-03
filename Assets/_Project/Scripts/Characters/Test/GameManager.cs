using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.AbilitySystem;
using _Project.Scripts.AbilitySystem.abstractions;
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
        private GameplayView _gameplayView;

        [SerializeField]
        private CameraController _cameraController;

        private IBuffProvider _buffProvider;
        private IConfigProvider _configProvider;
        private IAbilityFactory _abilityFactory;
        private List<StatBuff> availableBuffs;

        private void Start()
        {
            _buffProvider = ServiceLocator.For(this).Get<IBuffProvider>();
            _configProvider = ServiceLocator.For(this).Get<IConfigProvider>();
            _abilityFactory = ServiceLocator.For(this).Get<IAbilityFactory>();


            availableBuffs = _buffProvider.GetBuffs().ToList();
            SetupAbilities();
            SetupCamera();
        }

        private void SetupAbilities()
        {
            var firstPlayerAbilities = new List<Ability>
                { _abilityFactory.Create(_playerOne, AbilityType.BasicAttack) };
            var secondPlayerAbilities = new List<Ability>
                { _abilityFactory.Create(_playerTwo, AbilityType.BasicAttack) };

            _playerOne.AbilityModel.AddAbilities(firstPlayerAbilities);
            _playerTwo.AbilityModel.AddAbilities(secondPlayerAbilities);
        }

        private void SetupCamera()
        {
            _cameraController.SetupLookAtPosition((_playerOne.transform.position + _playerTwo.transform.position) / 2);
        }

        [ContextMenu("Apply Buffs")]
        public void ApplyBuffs()
        {
            var minBuffCount = _configProvider.Data.settings.buffCountMin;
            var maxBuffCount = _configProvider.Data.settings.buffCountMax;

            var buffsForPlayerOne = availableBuffs.TakeRandom(Random.Range(minBuffCount,
                maxBuffCount));

            var buffsForPlayerTwo = availableBuffs.TakeRandom(Random.Range(minBuffCount,
                maxBuffCount));

            BuffApplier.ApplyBuffs(_playerOne, buffsForPlayerOne);
            BuffApplier.ApplyBuffs(_playerTwo, buffsForPlayerTwo);

            Debug.Log($"Player One: {_playerOne.StatsController}");
            Debug.Log($"Player Two: {_playerTwo.StatsController}");
        }


        [ContextMenu("Reset to Base")]
        public void TestResetStats()
        {
            _playerOne.StatsController.Mediator.ClearBuffs();
            _playerTwo.StatsController.Mediator.ClearBuffs();

            Logger.Log($"{_playerOne.StatsController}", Color.clear);
            Logger.Log($"{_playerTwo.StatsController}", Color.clear);
        }

        [ContextMenu("Output buffs")]
        public void Output()
        {
            Logger.Log(
                $"Player One Buffs:\n{string.Join('\n', _playerOne.StatsController.Mediator.ActiveBuffs.Select(e => e.Name))}",
                Color.green);
            Logger.Log(
                $"Player Two Buffs:\n{string.Join('\n', _playerTwo.StatsController.Mediator.ActiveBuffs.Select(e => e.Name))}",
                Color.green);

            Logger.Log($"Player One Abilities:\n{string.Join('\n', _playerOne.AbilityModel.AvailableAbilities)}",
                Color.yellow);
            Logger.Log($"Player Two Abilities:\n{string.Join('\n', _playerTwo.AbilityModel.AvailableAbilities)}",
                Color.yellow);

            Logger.Log($"Player One Stats:\n{_playerOne.StatsController}", Color.blue);
            Logger.Log($"Player Two Stats:\n{_playerTwo.StatsController}", Color.blue);

            Logger.Log($"Player One Health:\n{_playerOne.HealthController.CurrentHealth}", Color.red);
            Logger.Log($"Player Two Health:\n{_playerTwo.HealthController.CurrentHealth}", Color.red);
        }

        [ContextMenu("Player One Attack player Two")]
        public void PlayerOneAttack()
        {
            AbilityApplier.Apply(_playerOne, new List<IEntity> { _playerTwo }, AbilityType.BasicAttack);
            Logger.Log($"{_playerTwo.StatsController}", Color.magenta);
            Logger.Log($"{_playerTwo.HealthController.CurrentHealth}", Color.magenta);
        }

        [ContextMenu("Player Two Attack player One")]
        public void PlayerTwoAttack()
        {
            AbilityApplier.Apply(_playerTwo, new List<IEntity> { _playerOne }, AbilityType.BasicAttack);
            Logger.Log($"{_playerOne.StatsController}", Color.magenta);
            Logger.Log($"{_playerOne.HealthController.CurrentHealth}", Color.magenta);
        }
    }
}