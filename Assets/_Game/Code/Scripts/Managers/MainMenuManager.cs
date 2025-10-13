using TMPro;
using UnityEngine;

namespace YNQ.JumpyJoe
{
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

        private GameManager _gameManager;
        
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

        public void Play()
        {
            _gameManager.StartGame();
            _animator.SetTrigger("Hide");
        }
        
        public void DisableCanvaas() => _canvas.gameObject.SetActive(false);
    }
}
