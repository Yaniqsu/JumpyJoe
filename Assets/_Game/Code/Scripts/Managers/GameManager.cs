using UnityEngine;

namespace YNQ.JumpyJoe
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameValues _gameValues;
        [SerializeField] private TileManager _tileManager;
        [SerializeField] private PlayerController _playerControllerPrefab;
        [SerializeField] private MainMenuManager _mainMenuManagerPrefab;
        [SerializeField] private GameOverScreen _gameOverScreenPrefab;
        [SerializeField] private StatsDisplayManager _statsDisplayManagerPrefab;

        private PlayerController _playerController;
        private StatsManager _statsManager;
        private MainMenuManager _mainMenuManager;
        private GameOverScreen _gameOverScreen;
        private StatsDisplayManager _statsDisplayManager;

        private void Start()
        {
            _statsManager = new StatsManager(_gameValues);
            
            _tileManager.GenerateStartTiles();
            InstantiateMenus();
            SpawnPlayer();
            
        }

        private void InstantiateMenus()
        {
            _mainMenuManager = Instantiate(_mainMenuManagerPrefab);
            _gameOverScreen = Instantiate(_gameOverScreenPrefab);
            _statsDisplayManager = Instantiate(_statsDisplayManagerPrefab);
            
            _mainMenuManager.Show(this, _statsManager);
            _statsDisplayManager.Initialize(_statsManager);
        }

        private void SpawnPlayer()
        {
            _playerController = Instantiate(_playerControllerPrefab, _tileManager.CurrentPos, Quaternion.identity);
            _playerController.Initialize(_tileManager);
            _playerController.OnDeath += obstacle =>
            {
                if(obstacle != null)
                    Destroy(obstacle);
                _gameOverScreen.ShowGameOverScreen(_statsManager);
                _statsDisplayManager.Hide();
            };
            _playerController.OnJump += _statsManager.OnPlayerJump;
        }

        public void StartGame()
        {
            _playerController.CameraManager.SwitchCamera(CameraType.Game);
            _playerController.Movement.OnJumpEnd += _tileManager.AddTile;
            _playerController.MicrophoneInputController.StartRecording();
            _statsDisplayManager.Show();
        }
    }
}
