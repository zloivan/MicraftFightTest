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

        private void Awake()
        {
            _dataLoader = ServiceLocator.GetService<IDataLoader>();
        }

        private void Start()
        {
            StartGame(false);
        }

        public void StartGame(bool withBuffs)
        {
            InitializePlayer(_player1, withBuffs);
            InitializePlayer(_player2, withBuffs);
        }

        private void InitializePlayer(PlayerController player, bool withBuffs)
        {
            player.Initialize(_dataLoader.Data.stats);

            if (!withBuffs) 
                return;
            
            
            var buffCount = Random.Range(_dataLoader.Data.settings.buffCountMin,
                _dataLoader.Data.settings.buffCountMax + 1);
            var selectedBuffs = new List<Buff>();

            for (var i = 0; i < buffCount; i++)
            {
                var buffIndex = Random.Range(0, _dataLoader.Data.buffs.Length);
                var buff = _dataLoader.Data.buffs[buffIndex];
                selectedBuffs.Add(buff);
            }

            player.ApplyBuffs(selectedBuffs.ToArray());
        }
    }
}