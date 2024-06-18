using Code.Managers;

namespace Code.UI
{
    public class GamePresenter
    {
        private readonly GameManager _gameManager;

        public GamePresenter(IGameView view, GameManager gameManager)
        {
            _gameManager = gameManager;
            view.SetPresenter(this);
        }

        public void OnGameWithBuffsButtonClicked()
        {
            _gameManager.StartGame(true);
        }

        public void OnGameWithoutBuffsButtonClicked()
        {
            _gameManager.StartGame(false);
        }
    }
}