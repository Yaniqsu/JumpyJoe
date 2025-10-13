using System.Collections;
using TMPro;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    public class StatsDisplayManager : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Animator _animator;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _heightText;
        [SerializeField] private float _animationTime = 0.5f;

        private float _lastScore;
        private float _lastHeight;

        public void Initialize(StatsManager statsManager)
        {
            SetScore(0);
            SetHeight(0);
            
            statsManager.OnDistanceChanged += distance => StartCoroutine(ChangeScore(distance));
            statsManager.OnHeightChanged += height => StartCoroutine(ChangeHeight(height));
            
            _canvas.gameObject.SetActive(false);
        }
        
        public void Show() => _canvas.gameObject.SetActive(true);
        public void Hide() => _animator.SetTrigger("Hide");

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

        private void SetScore(float value)
        {
            _lastScore = value;
            _scoreText.text = $"Score: {_lastScore.ToString("F2").Replace(",", ".")}m";
        }
        
        private void SetHeight(float value)
        {
            _lastHeight = value;
            _heightText.text = $"Height: {_lastHeight.ToString("F2").Replace(",", ".")}m";
        }
    }
}
