using Code.Managers;
using Code.UI;
using UnityEngine;

namespace Code.Utility
{
    public class Bootstrapper : MonoBehaviour
    {
        private const string DATA_FILE_NAME = "data";

        [SerializeField]
        private GameView _gameView;

        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private Camera _camera;

        private void Awake()
        {
            ServiceLocator.RegisterService<IDataLoader>(new ResourcesLoader(DATA_FILE_NAME));
            ServiceLocator.RegisterService(_camera);

            new GamePresenter(_gameView, _gameManager);
        }
    }
}