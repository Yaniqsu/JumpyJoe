using System;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputReference _inputReference;
        [SerializeField] private PlayerMovementValues _playerMovementValues;
        
        private TileManager _tileManager;
        public PlayerMovement Movement { get; private set; }
        public PlayerInput PlayerInput { get; private set; }

        public void Initialize(TileManager tileManager)
        {
            _tileManager = tileManager;
            Movement = new PlayerMovement(this, _playerMovementValues);
            PlayerInput = new PlayerInput(_inputReference);

            PlayerInput.OnJump += Jump;
            PlayerInput.OnAlterJumpHeight += Movement.AlterHeight;
        }

        private void Jump()
        {
            Movement.Jump(_tileManager.CurrentPos, _tileManager.NextPos);
        }

        private void OnDisable()
        {
            PlayerInput.DisableInput();
        }

        private void OnDestroy()
        {
            PlayerInput.DisableInput();
        }
    }
}