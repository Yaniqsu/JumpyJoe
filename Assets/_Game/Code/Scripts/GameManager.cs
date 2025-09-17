using UnityEngine;

namespace YNQ.JumpyJoe
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TileManager _tileManager;
        [SerializeField] private PlayerController _playerControllerPrefab;

        PlayerController _playerController;

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            _tileManager.GenerateStartTiles();

            _playerController = Instantiate(_playerControllerPrefab, _tileManager.CurrentPos, Quaternion.identity);
            _playerController.Initialize(_tileManager);
            _playerController.Movement.OnJumpEnd += _tileManager.AddTile;
        }
    }
}
