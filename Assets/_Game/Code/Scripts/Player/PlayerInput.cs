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
        private bool _inTreshold;
        private float _loudnessSum;
        private int _loudnesCount;

        public bool EnableMicInput { get; set; } = true;
        
        public PlayerInput(MicrophoneInputController microphoneInputController, InputReference inputReference)
        {
            _reference = inputReference;
            _micController = microphoneInputController;
            _maxDbNormalised = _reference.maxDbTreshold - _reference.minDbTreshold;
            _loudnessSum = 0;
            _loudnesCount = 0;
        }

        public void Update()
        {
            if(EnableMicInput)
                CheckMicrophoneInput();
        }

        private void CheckMicrophoneInput()
        {
            if (_micController.DbValue >= _reference.minDbTreshold &&
                _micController.DbValue <= _reference.maxDbTreshold)
            {
                _loudnessSum += _micController.DbValue;
                _loudnesCount++;
                _inTreshold = true;
            }
            else if(_inTreshold && _loudnesCount > _reference.minTresholdFrames)
            {
                var ratio = Mathf.InverseLerp(_reference.minDbTreshold, _reference.maxDbTreshold,
                    _loudnessSum / _loudnesCount);
                Debug.Log($"Db: {_loudnessSum / _loudnesCount}");
                OnSetJumpHeight?.Invoke(ratio);
                
                _loudnessSum = 0;
                _loudnesCount = 0;
                _inTreshold = false;
            }
        }
    }
}
