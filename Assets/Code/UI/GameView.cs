using UnityEngine;
using UnityEngine.UI;

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

        private GamePresenter _presenter;

        public void SetPresenter(GamePresenter presenter)
        {
            _presenter = presenter;
            _gameWithBuffsButton.onClick.AddListener(() => _presenter.OnGameWithBuffsButtonClicked());
            _gameWithoutBuffsButton.onClick.AddListener(() => _presenter.OnGameWithoutBuffsButtonClicked());
            _player1AttackButton.onClick.AddListener(() => _presenter.OnPlayerAttackButtonClicked(1));
            _player2AttackButton.onClick.AddListener(() => _presenter.OnPlayerAttackButtonClicked(2));
        }
    }
}