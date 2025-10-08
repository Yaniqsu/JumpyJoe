using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YNQ.JumpyJoe
{
    public class PlayerInput
    {
        public Action OnJump;
        public Action<float> OnSetJumpHeight;
        private readonly InputReference _reference;
        private readonly MicrophoneInputController _micController;
        private readonly float _maxDbNormalised;
        
        public PlayerInput(MicrophoneInputController microphoneInputController, InputReference inputReference)
        {
            _reference = inputReference;
            _micController = microphoneInputController;
            _maxDbNormalised = _reference.maxDbTreshold - _reference.minDbTreshold;
        }

        public void Update()
        {
            CheckMicrophoneInput();
        }

        private void CheckMicrophoneInput()
        {
            if (_micController.DbValue >= _reference.minDbTreshold &&
                _micController.DbValue <= _reference.maxDbTreshold)
            {
                var dbNormalised = _micController.DbValue - _reference.minDbTreshold;
                var ratio = dbNormalised / _maxDbNormalised;
                
                Debug.Log($"Ratio: {ratio}");
                OnSetJumpHeight?.Invoke(ratio);
            }
        }
    }
}
