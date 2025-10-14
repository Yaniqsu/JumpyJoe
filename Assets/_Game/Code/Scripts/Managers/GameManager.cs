using UnityEngine;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Główna klasa zarządzająca logiką gry.
    /// Odpowiada za inicjalizację menedżerów, gracza i kontrolę przebiegu rozgrywki.
    /// </summary>
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

        /// <summary>
        /// Uruchamia grę, inicjalizując wszystkie główne elementy i systemy.
        /// </summary>
        private void Start()
        {
            _statsManager = new StatsManager(_gameValues);
            
            _tileManager.GenerateStartTiles();
            InstantiateMenus();
            SpawnPlayer();
        }

        /// <summary>
        /// Tworzy i inicjalizuje elementy interfejsu użytkownika (menu główne, ekran końcowy, statystyki).
        /// </summary>
        private void InstantiateMenus()
        {
            _mainMenuManager = Instantiate(_mainMenuManagerPrefab);
            _gameOverScreen = Instantiate(_gameOverScreenPrefab);
            _statsDisplayManager = Instantiate(_statsDisplayManagerPrefab);
            
            _mainMenuManager.Show(this, _statsManager);
            _statsDisplayManager.Initialize(_statsManager);
        }

        /// <summary>
        /// Tworzy instancję gracza i podłącza odpowiednie zdarzenia (skok, śmierć).
        /// </summary>
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

        /// <summary>
        /// Rozpoczyna nową rozgrywkę i aktywuje odpowiednie systemy.
        /// </summary>
        public void StartGame()
        {
            _playerController.CameraManager.SwitchCamera(CameraType.Game);
            _playerController.Movement.OnJumpEnd += _tileManager.AddTile;
            _playerController.MicrophoneInputController.StartRecording();
            _statsDisplayManager.Show();
        }
    }
}
