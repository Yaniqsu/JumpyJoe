using System;
using NaughtyAttributes;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputReference _inputReference;
        [SerializeField] private PlayerMovementValues _playerMovementValues;
        [SerializeField] private ParticleSystem _destroyParticle;
        [SerializeField] private MicrophoneInputController _microphoneInputController;
        [SerializeField, Tag] private string _obstacleTag;

        private bool _dead;
        
        private TileManager _tileManager;
        public PlayerMovement Movement { get; private set; }
        public PlayerInput PlayerInput { get; private set; }
        public PlayerCameraManager CameraManager { get; private set; }
        public MicrophoneInputController MicrophoneInputController => _microphoneInputController;

        public event Action<GameObject> OnDeath = null;
        public event Action<float> OnJump = null;

        public void Initialize(TileManager tileManager)
        {
            _tileManager = tileManager;
            Movement = new PlayerMovement(this, _playerMovementValues);
            PlayerInput = new PlayerInput(_microphoneInputController, _inputReference);
            CameraManager = GetComponentInChildren<PlayerCameraManager>();
            foreach (var component in GetComponentsInChildren<IPlayerComponent>())
            {
                component.Initialize(this);
            }

            PlayerInput.OnSetJumpHeight += ratio =>
            {
                Movement.SetHeight(ratio);
                Jump();
            };
            Movement.OnJumpStart += () => OnJump?.Invoke(Movement.CurrentHeight);
        }

        private void Jump()
        {
            Movement.Jump(_tileManager.CurrentPos, _tileManager.NextPos);
        }

        private void Update()
        {
            if (_dead)
                return;
            
            PlayerInput.Update();
        }

        private void OnTriggerEnter(Collider collision)
        {
            var rb = collision.attachedRigidbody;
            if (rb == null)
                return;
            
            if (rb.CompareTag(_obstacleTag))
                Kill(rb.gameObject);
        }

        private void Kill(GameObject obstacle)
        {
            if (_dead)
                return;
            
            _destroyParticle.Play();
            Die();
            OnDeath?.Invoke(obstacle);
        }

        public void Kill()
        {
            if (_dead)
                return;
            
            Die();
            OnDeath?.Invoke(null);
        }

        private void Die()
        {
            _dead = true;
            CameraManager.SwitchCamera(CameraType.GameOver);
        }
    }
}