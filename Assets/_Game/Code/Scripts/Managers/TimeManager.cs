using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace YNQ.JumpyJoe
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private TimeSettings _timeSettings;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _timeBar;
        
        private float _currentTime;
        private Coroutine _handleTimeLose;

        public event Action OnTimeEnd;

        public void Initialize(PlayerController player)
        {
            _currentTime = _timeSettings.maxTime;
            
            player.OnJump += _ => IncreaseTime();
            player.OnDeath += _ => Stop();
            
            _canvas.gameObject.SetActive(true);
            
            _handleTimeLose = StartCoroutine(HandleTimeLose());
        }
        
        private IEnumerator HandleTimeLose()
        {
            while (_currentTime > 0)
            {
                _currentTime -= _timeSettings.timeDecreaseOverTime * Time.deltaTime;
                UpdateTimeBar();
                yield return null;
            }
            
            OnTimeEnd?.Invoke();
        }

        private void Stop()
        {
            StopCoroutine(_handleTimeLose);
            _canvas.gameObject.SetActive(false);
        }

        private void IncreaseTime()
        {
            _currentTime = Mathf.MoveTowards(_currentTime, _timeSettings.maxTime, _timeSettings.jumpTimeIncrease);
            UpdateTimeBar();
        }

        private void UpdateTimeBar()
        {
            _timeBar.value = _currentTime / _timeSettings.maxTime;
        }
    }
}
