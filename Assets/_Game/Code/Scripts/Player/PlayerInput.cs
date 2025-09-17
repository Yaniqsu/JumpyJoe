using System;
using UnityEngine.InputSystem;

namespace YNQ.JumpyJoe
{
    
    public class PlayerInput
    {
        public Action OnJump;
        public Action<float> OnAlterJumpHeight;
        private InputReference _reference;
        
        public PlayerInput(InputReference inputReference)
        {
            _reference = inputReference;

            EnableInput();
        }

        private void EnableInput()
        {
            _reference.jumpAction.action.started += Jump;
            _reference.alterHeightAction.action.started += AlterHeight;

            _reference.action.Enable();
        }

        public void DisableInput()
        {
            _reference.jumpAction.action.started -= Jump;
            _reference.alterHeightAction.action.started -= AlterHeight;
        }

        public void Jump(InputAction.CallbackContext context)
        {
            OnJump?.Invoke();
        }

        public void AlterHeight(InputAction.CallbackContext context)
        {
            OnAlterJumpHeight?.Invoke(context.action.ReadValue<float>());
        }
    }
}
