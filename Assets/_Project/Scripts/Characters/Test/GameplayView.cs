using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Characters.Test
{
    public class GameplayView : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerOneBuffIconParent;

        [SerializeField]
        private Transform _playerTwoBuffIconParent;

        [SerializeField]
        private Button _buffsOn;

        [SerializeField]
        private Button _buffsOff;
        
        [SerializeField]
        private GameObject _buffIconPrefab;
    }
}