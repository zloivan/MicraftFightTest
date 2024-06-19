using System;
using System.Collections.Generic;
using Code.Character;
using Code.Data;
using Code.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController _player1;
        [SerializeField] private PlayerController _player2;

        public event Action<int, List<Stat>, List<Buff>> OnPlayerStatsChanged;

        private IDataLoader _dataLoader;
        private List<Buff> _allBuffs;


        private void Start()
        {
            _dataLoader = ServiceLocator.GetService<IDataLoader>();
            LoadBuffs();

            _player1.OnStatsChanged.AddListener(() => HandleStatsChanged(_player1));
            _player2.OnStatsChanged.AddListener(() => HandleStatsChanged(_player2));
            _player1.OnBuffsChanged.AddListener(() => HandleBuffsChanged(_player1));
            _player2.OnBuffsChanged.AddListener(() => HandleBuffsChanged(_player2));
            
            StartGame(false);
        }

        private void LoadBuffs()
        {
            _allBuffs = new List<Buff>(_dataLoader.Data.buffs);
        }

        public void StartGame(bool withBuffs)
        {
            InitializePlayer(_player1, withBuffs);
            InitializePlayer(_player2, withBuffs);
            InitializePlayerStats();
        }

        private void InitializePlayer(PlayerController player, bool withBuffs)
        {
            player.Initialize(_dataLoader.Data.stats);

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
            var buffCount = Random.Range(_dataLoader.Data.settings.buffCountMin,
                _dataLoader.Data.settings.buffCountMax + 1);

            var selectedBuffs = new List<Buff>();
            var availableBuffs = new List<Buff>(_allBuffs);

            for (var i = 0; i < buffCount; i++)
            {
                if (availableBuffs.Count == 0) break;

                var buffIndex = Random.Range(0, availableBuffs.Count);
                var buff = availableBuffs[buffIndex];
                selectedBuffs.Add(buff);

                if (!_dataLoader.Data.settings.allowDuplicateBuffs)
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
