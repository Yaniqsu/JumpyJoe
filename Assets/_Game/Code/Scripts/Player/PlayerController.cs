using System;
using NaughtyAttributes;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputReference _inputReference;
        [SerializeField] private PlayerMovementValues _playerMovementValues;
        [SerializeField] private ParticleSystem _destroyParticle;
        [SerializeField, Tag] private string _obstacleTag;
        
        private TileManager _tileManager;
        public PlayerMovement Movement { get; private set; }
        public PlayerInput PlayerInput { get; private set; }
        public PlayerCameraManager CameraManager { get; private set; }

        public event Action<GameObject> OnDeath = null;

        public void Initialize(TileManager tileManager)
        {
            _tileManager = tileManager;
            Movement = new PlayerMovement(this, _playerMovementValues);
            PlayerInput = new PlayerInput(_inputReference);
            CameraManager = GetComponentInChildren<PlayerCameraManager>();

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

        private void OnTriggerEnter(Collider collision)
        {
            var rb = collision.attachedRigidbody;
            if (rb == null)
                return;
            
            Debug.Log(rb.tag);
            
            if (rb.CompareTag(_obstacleTag))
                Kill(rb.gameObject);
        }

        private void Kill(GameObject obstacle)
        {
            _destroyParticle.Play();
            CameraManager.SwitchCamera(CameraType.GameOver);
            OnDeath?.Invoke(obstacle);
        }
    }
}