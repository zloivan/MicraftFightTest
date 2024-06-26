using System.Collections.Generic;
using _Project.Scripts.ServiceLocatorSystem;
using _Project.Scripts.StatsAndBuffsSystem;
using _Project.Scripts.UI.Elements;
using _Project.Scripts.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Button _gameWithBuffsButton;
        [SerializeField] private Button _gameWithoutBuffsButton;
        [SerializeField] private Button _player1AttackButton;
        [SerializeField] private Button _player2AttackButton;
        [SerializeField] private Transform _player1StatsPanel;
        [SerializeField] private Transform _player2StatsPanel;
        [SerializeField] private GameObject _statPrefab;
        [SerializeField] private GameObject _buffPrefab;

        [SerializeField]
        private HealthBar _player1Health;

        [SerializeField]
        private HealthBar _player2Health;

        private GamePresenter _presenter;
        private IPlayerIconProvider _playerIconsProvider;

        private readonly Dictionary<int, List<StatBuffView>> _playerStatViews = new();
        private readonly Dictionary<int, List<StatBuffView>> _playerBuffViews = new();

        public void SetPresenter(GamePresenter presenter)
        {
            _presenter = presenter;
            _playerIconsProvider = ServiceLocator.For(this).Get<IPlayerIconProvider>();
            
            _gameWithBuffsButton.onClick.AddListener(() => _presenter.OnGameWithBuffsButtonClicked());
            _gameWithoutBuffsButton.onClick.AddListener(() => _presenter.OnGameWithoutBuffsButtonClicked());
            _player1AttackButton.onClick.AddListener(() => _presenter.OnPlayerAttackButtonClicked(1));
            _player2AttackButton.onClick.AddListener(() => _presenter.OnPlayerAttackButtonClicked(2));
        }

        public void InitializePlayerStats(int playerId, List<Stat> stats, List<Buff> buffs)
        {
            var panel = playerId == 1 ? _player1StatsPanel : _player2StatsPanel;

            if (!_playerStatViews.ContainsKey(playerId))
                _playerStatViews[playerId] = new List<StatBuffView>();

            if (!_playerBuffViews.ContainsKey(playerId))
                _playerBuffViews[playerId] = new List<StatBuffView>();

            foreach (Transform child in panel)
            {
                Destroy(child.gameObject);
            }

            _playerStatViews[playerId].Clear();
            foreach (var stat in stats)
            {
                var statObj = Instantiate(_statPrefab, panel);
                var statBuffView = statObj.GetComponent<StatBuffView>();

                if (statBuffView == null) 
                    continue;
                
                var icon = _playerIconsProvider.GetSpriteById(stat.icon);
                var value = stat.value.ToString();
                statBuffView.SetData(icon, value);
                _playerStatViews[playerId].Add(statBuffView);
            }

            _playerBuffViews[playerId].Clear();
            foreach (var buff in buffs)
            {
                var buffObj = Instantiate(_buffPrefab, panel);
                var statBuffView = buffObj.GetComponent<StatBuffView>();

                if (statBuffView == null) 
                    continue;
                
                var icon = _playerIconsProvider.GetSpriteById(buff.icon);
                var value = buff.title;
                statBuffView.SetData(icon, value);
                _playerBuffViews[playerId].Add(statBuffView);
            }
        }

        public void UpdatePlayerStats(int playerId, List<Stat> stats)
        {
            if (!_playerStatViews.TryGetValue(playerId, out var statViews)) 
                return;
            
            for (var i = 0; i < stats.Count; i++)
            {
                if (i < statViews.Count)
                {
                    statViews[i].UpdateValue(stats[i].value.ToString());
                }
            }
        }

        public void UpdatePlayerBuffs(int playerId, List<Buff> buffs)
        {
            if (!_playerBuffViews.TryGetValue(playerId, out var buffViews)) 
                return;
            
            for (var i = 0; i < buffs.Count; i++)
            {
                if (i < buffViews.Count)
                {
                    buffViews[i].UpdateValue(buffs[i].title);
                }
            }
        }
    }
}
