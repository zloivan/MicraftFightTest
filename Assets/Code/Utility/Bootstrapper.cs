using Code.Managers;
using Code.UI;
using UnityEngine;

namespace Code.Utility
{
    public class Bootstrapper : MonoBehaviour
    {
        private const string DATA_FILE_NAME = "data";
        private const string ICONS_FOLDER_PATH = "Icons";
        
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
            ServiceLocator.RegisterService(new ImageProvider(ICONS_FOLDER_PATH));
            
            new GamePresenter(_gameView, _gameManager);
        }
    }
}