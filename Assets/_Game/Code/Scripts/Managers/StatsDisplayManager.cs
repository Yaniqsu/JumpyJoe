using System.Collections;
using TMPro;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Klasa zarządzająca wyświetlaniem statystyk gry
    /// </summary>
    public class StatsDisplayManager : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Animator _animator;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _heightText;
        [SerializeField] private float _animationTime = 0.5f;

        private float _lastScore;
        private float _lastHeight;

        /// <summary>
        /// Inicjalizaja wartości i komponentów
        /// </summary>
        /// <param name="statsManager">Referencja do obiektu typu StatsManager</param>
        public void Initialize(StatsManager statsManager)
        {
            SetScore(0);
            SetHeight(0);
            
            statsManager.OnDistanceChanged += distance => StartCoroutine(ChangeScore(distance));
            statsManager.OnHeightChanged += height => StartCoroutine(ChangeHeight(height));
            
            _canvas.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Pokazanie Canvasu
        /// </summary>
        public void Show() => _canvas.gameObject.SetActive(true);
        
        /// <summary>
        /// Wywołanie animacji ukrycia Canvasu
        /// </summary>
        public void Hide() => _animator.SetTrigger("Hide");

        /// <summary>
        /// Animacja zmiany wartości obecnego wyniku
        /// </summary>
        /// <param name="targetScore">Nowy wynik</param>
        private IEnumerator ChangeScore(float targetScore)
        {
            var last = _lastScore;
            var elapsed = 0f;

            while (elapsed < _animationTime)
            {
                SetScore(Mathf.Lerp(last, targetScore, elapsed / _animationTime));
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            SetScore(targetScore);
        }
        
        /// <summary>
        /// Animacja zmiany wartości sumy skoków
        /// </summary>
        /// <param name="targetScore">Nowa suma skoków</param>
        private IEnumerator ChangeHeight(float targetScore)
        {
            var last = _lastHeight;
            var elapsed = 0f;

            while (elapsed < _animationTime)
            {
                SetHeight(Mathf.Lerp(last, targetScore, elapsed / _animationTime));
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            SetHeight(targetScore);
        }

        /// <summary>
        /// Ustawianie dystansu do wyświetlenia
        /// </summary>
        /// <param name="value">Dystans</param>
        private void SetScore(float value)
        {
            _lastScore = value;
            _scoreText.text = $"Score: {_lastScore.ToString("F2").Replace(",", ".")}m";
        }
        
        /// <summary>
        /// Ustawienie sumy skoków do wyświetlenia 
        /// </summary>
        /// <param name="value"Suma skoków></param>
        private void SetHeight(float value)
        {
            _lastHeight = value;
            _heightText.text = $"Height: {_lastHeight.ToString("F2").Replace(",", ".")}m";
        }
    }
}
