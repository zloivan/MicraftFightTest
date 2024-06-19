using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Code.Data;
using Code.Utility;

namespace Code.UI
{
    public class GameView : MonoBehaviour, IGameView
    {
        [SerializeField]
        private Button _gameWithBuffsButton;

        [SerializeField]
        private Button _gameWithoutBuffsButton;

        [SerializeField]
        private Button _player1AttackButton;

        [SerializeField]
        private Button _player2AttackButton;

        [SerializeField]
        private Transform _player1StatsPanel;

        [SerializeField]
        private Transform _player2StatsPanel;

        [SerializeField]
        private GameObject _statPrefab;

        [SerializeField]
        private GameObject _buffPrefab;

        private GamePresenter _presenter;
        private ImageProvider _imageProvider;


        public void SetPresenter(GamePresenter presenter)
        {
            _presenter = presenter;
            _imageProvider = ServiceLocator.GetService<ImageProvider>();
            _gameWithBuffsButton.onClick.AddListener(() => _presenter.OnGameWithBuffsButtonClicked());
            _gameWithoutBuffsButton.onClick.AddListener(() => _presenter.OnGameWithoutBuffsButtonClicked());
            _player1AttackButton.onClick.AddListener(() => _presenter.OnPlayerAttackButtonClicked(1));
            _player2AttackButton.onClick.AddListener(() => _presenter.OnPlayerAttackButtonClicked(2));
        }

        public void UpdatePlayerStats(int playerId, List<Stat> stats, List<Buff> buffs)
        {
            Transform panel = playerId == 1 ? _player1StatsPanel : _player2StatsPanel;
            foreach (Transform child in panel)
            {
                Destroy(child.gameObject);
            }

            foreach (var stat in stats)
            {
                GameObject statObj = Instantiate(_statPrefab, panel);
                var image = statObj.GetComponentInChildren<Image>();
                var text = statObj.GetComponentInChildren<Text>();

                image.sprite = _imageProvider.GetSpriteById(stat.icon);
                text.text = stat.value.ToString();
            }

            foreach (var buff in buffs)
            {
                GameObject buffObj = Instantiate(_buffPrefab, panel);
                var image = buffObj.GetComponentInChildren<Image>();
                var text = buffObj.GetComponentInChildren<Text>();

                image.sprite = _imageProvider.GetSpriteById(buff.icon);
                text.text = buff.title;
            }
        }
    }
}
