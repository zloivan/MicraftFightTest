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

        private GamePresenter _presenter;

        public void SetPresenter(GamePresenter presenter)
        {
            _presenter = presenter;
            _gameWithBuffsButton.onClick.AddListener(() => _presenter.OnGameWithBuffsButtonClicked());
            _gameWithoutBuffsButton.onClick.AddListener(() => _presenter.OnGameWithoutBuffsButtonClicked());
        }
    }
}