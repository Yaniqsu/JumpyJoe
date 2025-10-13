using System;
using System.Collections;
using System.Linq;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static YNQ.JumpyJoe.StringUtilities;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Klasa odpowiadająca za zarządzanie logiką ekranu końca gry
    /// </summary>
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private MenuStrings _menuStrings;
        [SerializeField] private CanvasGroup _gameOverCanvas;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
        [SerializeField] private Volume _volume;
        [SerializeField] private TextMeshProUGUI _distanceScore;
        [SerializeField] private TextMeshProUGUI _bestScoreSession;
        [SerializeField] private TextMeshProUGUI _bestScoreGlobal;
        [SerializeField] private TextMeshProUGUI _heightScore;
        [SerializeField] private Animator _animator;

        [SerializeField, AnimatorParam(nameof(_animator))]
        private string _showTrigger;

        /// <summary>
        /// Pokazuje ekran końca gry
        /// </summary>
        /// <param name="statsManager">Referencja do obiektu typu StatsManager</param>
        public void ShowGameOverScreen(StatsManager statsManager)
        {
            StartCoroutine(WaitUntilShowingGameOverScreen());
            
            _distanceScore.text = _menuStrings.distanceText.Construct(statsManager.CurrentDistance
                .FormatToMeter());
            _heightScore.text = _menuStrings.heightText.Construct(statsManager.HeightSum
                .FormatToMeter());
            _bestScoreSession.text = _menuStrings.bestSession.Construct(statsManager.GetBestDistanceSession(statsManager.CurrentDistance)
                .FormatToMeter());
            _bestScoreGlobal.text = _menuStrings.bestGlobal.Construct(statsManager.GetBestDistanceGlobal(statsManager.CurrentDistance)
                .FormatToMeter());
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(_verticalLayoutGroup.transform as RectTransform);
            _animator.SetTrigger(_showTrigger);
        }

        /// <summary>
        /// Ponowne wczytanie sceny
        /// </summary>
        public void Retry()
        {
            SceneSwitcher.SwitchScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// Czeka sekundę, zanim może pokazać Canvas
        /// </summary>
        private IEnumerator WaitUntilShowingGameOverScreen()
        {
            var elapsedTime = 0f;

            while (elapsedTime < 1)
            {
                _volume.weight = elapsedTime;
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            _gameOverCanvas.gameObject.SetActive(true);
        }
    }
}
