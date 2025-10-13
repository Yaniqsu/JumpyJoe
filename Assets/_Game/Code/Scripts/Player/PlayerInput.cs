using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Odpowiada za obsługę wejścia gracza, w tym przetwarzanie danych z mikrofonu i generowanie skoków.
    /// </summary>
    public class PlayerInput
    {
        public Action OnJump;
        public Action<float> OnSetJumpHeight;
        private readonly InputReference _reference;
        private readonly MicrophoneInputController _micController;
        private readonly float _maxDbNormalised;
        private bool _inTreshold;
        private float _loudnessSum;
        private int _loudnesCount;

        public bool EnableMicInput { get; set; } = true;
        
        /// <summary>
        /// Tworzy nowy obiekt wejścia gracza i ustawia zależności od mikrofonu oraz konfiguracji wejścia.
        /// </summary>
        /// <param name="microphoneInputController">Kontroler mikrofonu odczytujący poziom głośności.</param>
        /// <param name="inputReference">Obiekt z konfiguracją progów dźwięku i ustawieniami wejścia.</param>
        public PlayerInput(MicrophoneInputController microphoneInputController, InputReference inputReference)
        {
            _reference = inputReference;
            _micController = microphoneInputController;
            _maxDbNormalised = _reference.maxDbTreshold - _reference.minDbTreshold;
            _loudnessSum = 0;
            _loudnesCount = 0;
        }

        /// <summary>
        /// Aktualizuje stan wejścia — jeśli mikrofon jest aktywny, sprawdza jego wartość.
        /// </summary>
        public void Update()
        {
            if (EnableMicInput)
                CheckMicrophoneInput();
        }

        /// <summary>
        /// Analizuje sygnał z mikrofonu i wykrywa moment, w którym gracz generuje skok.
        /// </summary>
        private void CheckMicrophoneInput()
        {
            if (_micController.DbValue >= _reference.minDbTreshold &&
                _micController.DbValue <= _reference.maxDbTreshold)
            {
                _loudnessSum += _micController.DbValue;
                _loudnesCount++;
                _inTreshold = true;
            }
            else if (_inTreshold && _loudnesCount > _reference.minTresholdFrames)
            {
                var ratio = Mathf.InverseLerp(_reference.minDbTreshold, _reference.maxDbTreshold,
                    _loudnessSum / _loudnesCount);
                OnSetJumpHeight?.Invoke(ratio);
                
                _loudnessSum = 0;
                _loudnesCount = 0;
                _inTreshold = false;
            }
        }
    }
}
