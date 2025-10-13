using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Zarządza upływem czasu podczas rozgrywki.
    /// Obsługuje pasek czasu, jego regenerację po skokach oraz zakończenie gry po wyczerpaniu czasu.
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private TimeSettings _timeSettings;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _timeBar;
        
        private float _currentTime;
        private Coroutine _handleTimeLose;

        /// <summary>
        /// Wywoływane, gdy czas dobiegnie końca.
        /// </summary>
        public event Action OnTimeEnd;

        /// <summary>
        /// Inicjuje zarządzanie czasem i przypina odpowiednie zdarzenia gracza.
        /// </summary>
        /// <param name="player">Obiekt gracza, którego zdarzenia (skok, śmierć) wpływają na czas.</param>
        public void Initialize(PlayerController player)
        {
            _currentTime = _timeSettings.maxTime;
            
            player.OnJump += _ => IncreaseTime();
            player.OnDeath += _ => Stop();
            
            _canvas.gameObject.SetActive(true);
            
            _handleTimeLose = StartCoroutine(HandleTimeLose());
        }
        
        /// <summary>
        /// Coroutine odpowiedzialna za stopniowe zmniejszanie pozostałego czasu.
        /// Gdy czas spadnie do zera, wywołuje zdarzenie <see cref="OnTimeEnd"/>.
        /// </summary>
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

        /// <summary>
        /// Zatrzymuje licznik czasu i ukrywa interfejs paska czasu.
        /// </summary>
        private void Stop()
        {
            StopCoroutine(_handleTimeLose);
            _canvas.gameObject.SetActive(false);
        }

        /// <summary>
        /// Zwiększa aktualny czas po wykonaniu skoku przez gracza.
        /// Wartość czasu nie może przekroczyć maksymalnego limitu.
        /// </summary>
        private void IncreaseTime()
        {
            _currentTime = Mathf.MoveTowards(_currentTime, _timeSettings.maxTime, _timeSettings.jumpTimeIncrease);
            UpdateTimeBar();
        }

        /// <summary>
        /// Aktualizuje wartość paska czasu na podstawie aktualnego stanu.
        /// </summary>
        private void UpdateTimeBar()
        {
            _timeBar.value = _currentTime / _timeSettings.maxTime;
        }
    }
}
