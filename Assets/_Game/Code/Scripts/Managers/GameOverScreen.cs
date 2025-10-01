using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace YNQ.JumpyJoe
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private MenuStrings _menuStrings;
        [SerializeField] private CanvasGroup _gameOverCanvas;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
        [SerializeField] private TextMeshProUGUI _distanceScore;
        [SerializeField] private TextMeshProUGUI _bestScoreSession;
        [SerializeField] private TextMeshProUGUI _bestScoreGlobal;
        [SerializeField] private TextMeshProUGUI _heightScore;

        public void ShowGameOverScreen(StatsManager statsManager)
        {
            StartCoroutine(WaitUntilShowingGameOverScreen());
            
            _distanceScore.text = _menuStrings.distanceText.Construct(statsManager.CurrentDistance);
            _heightScore.text = _menuStrings.heightText.Construct(statsManager.HeightSum);
            _bestScoreSession.text = _menuStrings.bestSession.Construct(statsManager.GetBestDistanceSession(statsManager.CurrentDistance));
            _bestScoreGlobal.text = _menuStrings.bestGlobal.Construct(statsManager.GetBestDistanceGlobal(statsManager.CurrentDistance));
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(_verticalLayoutGroup.transform as RectTransform);
        }

        public void Retry()
        {
            SceneSwitcher.SwitchScene(SceneManager.GetActiveScene().buildIndex);
        }

        private IEnumerator WaitUntilShowingGameOverScreen()
        {
            yield return new WaitForSeconds(1f);
            
            _gameOverCanvas.gameObject.SetActive(true);
        }
    }
}
