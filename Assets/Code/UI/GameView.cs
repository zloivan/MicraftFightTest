using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Code.Data;
using Code.Utility;

namespace Code.UI
{
    public class GameView : MonoBehaviour, IGameView
    {
        [SerializeField] private Button _gameWithBuffsButton;
        [SerializeField] private Button _gameWithoutBuffsButton;
        [SerializeField] private Button _player1AttackButton;
        [SerializeField] private Button _player2AttackButton;
        [SerializeField] private Transform _player1StatsPanel;
        [SerializeField] private Transform _player2StatsPanel;
        [SerializeField] private GameObject _statPrefab;
        [SerializeField] private GameObject _buffPrefab;

        private GamePresenter _presenter;
        private ImageProvider _imageProvider;

        private Dictionary<int, List<StatBuffView>> _playerStatViews = new();
        private Dictionary<int, List<StatBuffView>> _playerBuffViews = new();

        public void SetPresenter(GamePresenter presenter)
        {
            _presenter = presenter;
            _imageProvider = ServiceLocator.GetService<ImageProvider>();
            _gameWithBuffsButton.onClick.AddListener(() => _presenter.OnGameWithBuffsButtonClicked());
            _gameWithoutBuffsButton.onClick.AddListener(() => _presenter.OnGameWithoutBuffsButtonClicked());
            _player1AttackButton.onClick.AddListener(() => _presenter.OnPlayerAttackButtonClicked(1));
            _player2AttackButton.onClick.AddListener(() => _presenter.OnPlayerAttackButtonClicked(2));
        }

        public void InitializePlayerStats(int playerId, List<Stat> stats, List<Buff> buffs)
        {
            Transform panel = playerId == 1 ? _player1StatsPanel : _player2StatsPanel;

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
                GameObject statObj = Instantiate(_statPrefab, panel);
                var statBuffView = statObj.GetComponent<StatBuffView>();

                if (statBuffView != null)
                {
                    Sprite icon = _imageProvider.GetSpriteById(stat.icon);
                    string value = stat.value.ToString();
                    statBuffView.SetData(icon, value);
                    _playerStatViews[playerId].Add(statBuffView);
                }
            }

            _playerBuffViews[playerId].Clear();
            foreach (var buff in buffs)
            {
                GameObject buffObj = Instantiate(_buffPrefab, panel);
                var statBuffView = buffObj.GetComponent<StatBuffView>();

                if (statBuffView != null)
                {
                    Sprite icon = _imageProvider.GetSpriteById(buff.icon);
                    string value = buff.title;
                    statBuffView.SetData(icon, value);
                    _playerBuffViews[playerId].Add(statBuffView);
                }
            }
        }

        public void UpdatePlayerStats(int playerId, List<Stat> stats)
        {
            if (_playerStatViews.TryGetValue(playerId, out var statViews))
            {
                for (int i = 0; i < stats.Count; i++)
                {
                    if (i < statViews.Count)
                    {
                        statViews[i].UpdateValue(stats[i].value.ToString());
                    }
                }
            }
        }

        public void UpdatePlayerBuffs(int playerId, List<Buff> buffs)
        {
            if (_playerBuffViews.TryGetValue(playerId, out var buffViews))
            {
                for (int i = 0; i < buffs.Count; i++)
                {
                    if (i < buffViews.Count)
                    {
                        buffViews[i].UpdateValue(buffs[i].title);
                    }
                }
            }
        }
    }
}
