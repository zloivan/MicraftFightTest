using System.Collections.Generic;
using Code.Character;
using Code.Data;
using Code.Utility;
using UnityEngine;

namespace Code.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _player1;

        [SerializeField]
        private PlayerController _player2;

        private IDataLoader _dataLoader;
        private List<Buff> _allBuffs;

        private void Awake()
        {
            _dataLoader = ServiceLocator.GetService<IDataLoader>();
            LoadBuffs();
        }

        private void Start()
        {
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
    }
}
