using TMPro;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private MenuStrings _menuStrings;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TextMeshProUGUI _bestSessionText;
        [SerializeField] private TextMeshProUGUI _bestGlobalText;

        private GameManager _gameManager;
        
        public void Show(GameManager gameManager, StatsManager statsManager)
        {
            _gameManager = gameManager;

            _bestSessionText.text = _menuStrings.bestSession.Construct(statsManager.GetBestDistanceSession(0));
            _bestGlobalText.text = _menuStrings.bestGlobal.Construct(statsManager.GetBestDistanceGlobal(0));
            
            _canvas.gameObject.SetActive(true);
        }

        public void Play()
        {
            _gameManager.StartGame();
            _canvas.gameObject.SetActive(false);
        }
    }
}
