using System;
using System.Collections;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    class PlayerMovement
    {
        private PlayerController _playerController;
        private PlayerMovementValues _values;
        private float _currentHeight = 1;
        private bool _isJumping = false;

        public event Action OnJumpEnd = null;
        
        public PlayerMovement(PlayerController playerController, PlayerMovementValues values)
        {
            _playerController = playerController;
            _values = values;
        }

        public void Jump(Vector3 startPos, Vector3 endPos)
        {
            if(!_isJumping)
                _playerController.StartCoroutine(HandleJump(startPos, endPos));
        }

        public void AlterHeight(float modifier)
        {
            _currentHeight = Mathf.Clamp(_currentHeight + modifier, _values.minHeight, _values.maxHeight);
            Debug.Log($"Current height: {_currentHeight}");
        }

        private IEnumerator HandleJump(Vector3 startPos, Vector3 endPos)
        {
            var time = _values.minSpeed + _currentHeight * _values.heightIncreaseSpeedRatio;
            var elapsedTime = 0f;
            _isJumping = true;

            while(elapsedTime < time)
            {
                _playerController.transform.position = GetQuadraticBezierPoint(startPos, endPos, elapsedTime / time);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _isJumping = false;
            _playerController.transform.position = endPos;
            OnJumpEnd?.Invoke();
        }

        private Vector3 GetQuadraticBezierPoint(Vector3 A, Vector3 B, float t)
        {
            Vector3 M = (A + B) / 2f;
            Vector3 C = new Vector3(M.x, M.y + _currentHeight, M.z);
            Vector3 P = (1 - t) * (1 - t) * A + 2 * (1 - t) * t * C + t * t * B;

            return P;
        }
    }
}