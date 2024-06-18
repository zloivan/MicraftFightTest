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
    }
}