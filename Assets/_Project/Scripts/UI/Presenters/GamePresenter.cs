using System.Collections.Generic;
using _Project.Scripts.Managers;
using _Project.Scripts.StatsAndBuffsSystem;
using _Project.Scripts.UI.Views;

namespace _Project.Scripts.UI.Presenters
{
    public class GamePresenter
    {
        private readonly GameManager _gameManager;
        private readonly GameView _view;

        public GamePresenter(GameView view, GameManager gameManager)
        {
            _gameManager = gameManager;
            _view = view;

            _gameManager.OnPlayerStatsChanged += InitializePlayerStats;
            _view.SetPresenter(this);
        }

        public void OnGameWithBuffsButtonClicked()
        {
            _gameManager.StartGame(true);
        }

        public void OnGameWithoutBuffsButtonClicked()
        {
            _gameManager.StartGame(false);
        }

        public void OnPlayerAttackButtonClicked(int playerId)
        {
            if (playerId == 1)
            {
                _gameManager.Player1Attack();
            }
            else if (playerId == 2)
            {
                _gameManager.Player2Attack();
            }
        }

        private void InitializePlayerStats(int playerId, List<Stat> stats, List<Buff> buffs)
        {
            _view.InitializePlayerStats(playerId, stats, buffs);
        }

        private void UpdatePlayerStats(int playerId, List<Stat> stats, List<Buff> buffs)
        {
            _view.UpdatePlayerStats(playerId, stats);
            _view.UpdatePlayerBuffs(playerId, buffs);
        }
    }
}