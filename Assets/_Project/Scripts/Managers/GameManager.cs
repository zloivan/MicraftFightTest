using System;
using System.Collections.Generic;
using _Project.Scripts.CameraControllSystem;
using _Project.Scripts.Controllers;

using _Project.Scripts.StatsAndBuffsSystem;
using _Project.Scripts.UI.Presenters;
using _Project.Scripts.UI.Views;
using UnityEngine;
using Utilities.ServiceLocator;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _player1;

        [SerializeField]
        private PlayerController _player2;

        public event Action<int, List<Stat>, List<Buff>> OnPlayerStatsChanged;

        private IConfigProvider _configProvider;
        private List<Buff> _allBuffs;
        private ICameraController _cameraController;

        [SerializeField]
        private GameView _gameView;
        private GamePresenter _gamePresenter;

        private void Start()
        {
            _configProvider = ServiceLocator.For(this).Get<IConfigProvider>();
            _cameraController = ServiceLocator.For(this).Get<ICameraController>();
            
            _cameraController.SetupLookAtPosition((_player1.transform.position + _player2.transform.position) / 2);

            _gamePresenter = new GamePresenter(_gameView, this);
            _player1.SetupView();

            LoadBuffs();

            _player1.OnStatsChanged.AddListener(() => HandleStatsChanged(_player1));
            _player2.OnStatsChanged.AddListener(() => HandleStatsChanged(_player2));
            _player1.OnBuffsChanged.AddListener(() => HandleBuffsChanged(_player1));
            _player2.OnBuffsChanged.AddListener(() => HandleBuffsChanged(_player2));

            StartGame(false);
        }

        private void LoadBuffs()
        {
            _allBuffs = new List<Buff>(_configProvider.Data.buffs);
        }

        public void StartGame(bool withBuffs)
        {
            InitializePlayer(_player1, withBuffs);
            InitializePlayer(_player2, withBuffs);
            InitializePlayerStats();
        }

        private void InitializePlayer(PlayerController player, bool withBuffs)
        {
            player.Initialize(_configProvider.Data.stats);

            if (withBuffs)
            {
                ApplyRandomBuffs(player);
            }
            else
            {
                player.ClearBuffs();
            }
        }

        private void ApplyRandomBuffs(PlayerController player)
        {
            var buffCount = Random.Range(_configProvider.Data.settings.buffCountMin,
                _configProvider.Data.settings.buffCountMax + 1);

            var selectedBuffs = new List<Buff>();
            var availableBuffs = new List<Buff>(_allBuffs);

            for (var i = 0; i < buffCount; i++)
            {
                if (availableBuffs.Count == 0) break;

                var buffIndex = Random.Range(0, availableBuffs.Count);
                var buff = availableBuffs[buffIndex];
                selectedBuffs.Add(buff);

                if (!_configProvider.Data.settings.allowDuplicateBuffs)
                {
                    availableBuffs.RemoveAt(buffIndex);
                }
            }

            player.ApplyBuffs(selectedBuffs.ToArray());
        }

        private void InitializePlayerStats()
        {
            OnPlayerStatsChanged?.Invoke(1, new List<Stat>(_player1.GetCurrentStats()), _player1.GetBuffs());
            OnPlayerStatsChanged?.Invoke(2, new List<Stat>(_player2.GetCurrentStats()), _player2.GetBuffs());
        }

        private void HandleStatsChanged(PlayerController player)
        {
            var playerId = player == _player1 ? 1 : 2;
            OnPlayerStatsChanged?.Invoke(playerId, new List<Stat>(player.GetCurrentStats()), player.GetBuffs());
        }

        private void HandleBuffsChanged(PlayerController player)
        {
            var playerId = player == _player1 ? 1 : 2;
            OnPlayerStatsChanged?.Invoke(playerId, new List<Stat>(player.GetCurrentStats()), player.GetBuffs());
        }

        [ContextMenu("Player 1 attack player 2")]
        public void Player1Attack()
        {
            _player1.Attack(_player2);
        }

        [ContextMenu("Player 2 attack player 1")]
        public void Player2Attack()
        {
            _player2.Attack(_player1);
        }
    }
}