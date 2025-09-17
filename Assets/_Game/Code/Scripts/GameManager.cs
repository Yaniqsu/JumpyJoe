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
            _tileManager.GenerateStartTiles();
            _playerController = Instantiate(_playerControllerPrefab, _tileManager.CurrentPos, Quaternion.identity);
            _playerController.Initialize(_tileManager);
        }

        public void StartGame()
        {
            _playerController.PlayerInput.EnableInput();
            _playerController.CameraManager.SwitchCamera(CameraType.Game);
            _playerController.Movement.OnJumpEnd += _tileManager.AddTile;
        }
    }
}
