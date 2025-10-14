using TMPro;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Klasa zarządzająca logiką ekranu menu głównego
    /// </summary>
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private MenuStrings _menuStrings;
        [SerializeField] private InputReference _reference;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TextMeshProUGUI _bestSessionText;
        [SerializeField] private TextMeshProUGUI _bestGlobalText;
        [SerializeField] private TMP_InputField _minDb;
        [SerializeField] private TMP_InputField _maxDb;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSource;

        private GameManager _gameManager;
        
        /// <summary>
        /// Pokazanie Canvasu i inicjalizacja komponentów
        /// </summary>
        public void Show(GameManager gameManager, StatsManager statsManager)
        {
            _gameManager = gameManager;

            _bestSessionText.text = _menuStrings.bestSession.Construct(statsManager.GetBestDistanceSession(0));
            _bestGlobalText.text = _menuStrings.bestGlobal.Construct(statsManager.GetBestDistanceGlobal(0));

            _minDb.text = _reference.minDbTreshold.ToString();
            _maxDb.text = _reference.maxDbTreshold.ToString();
            
            _minDb.onSubmit.AddListener(v => _reference.minDbTreshold = float.Parse(v));
            _minDb.onDeselect.AddListener(v => _reference.minDbTreshold = float.Parse(v));
            _maxDb.onSubmit.AddListener(v => _reference.maxDbTreshold = float.Parse(v));
            _maxDb.onDeselect.AddListener(v => _reference.maxDbTreshold = float.Parse(v));
            
            _canvas.gameObject.SetActive(true);
        }

        /// <summary>
        /// Rozpoczęcie nowej gry
        /// </summary>
        public void Play()
        {
            _gameManager.StartGame();
            _animator.SetTrigger("Hide");
            _audioSource.Stop();
        }
        
        /// <summary>
        /// Zamknięcie aplikacji
        /// </summary>
        public void Quit() => Application.Quit();
        
        /// <summary>
        /// Ukrycie Canvasu
        /// </summary>
        public void DisableCanvaas() => _canvas.gameObject.SetActive(false);
    }
}
