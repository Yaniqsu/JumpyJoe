using System;
using NaughtyAttributes;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Główny kontroler gracza — obsługuje ruch, wejście, skoki, kolizje i śmierć.
    /// </summary>
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

        /// <summary>
        /// Inicjalizuje komponent gracza, przypina zdarzenia i konfiguruje zależności.
        /// </summary>
        /// <param name="tileManager">Referencja do menedżera kafelków.</param>
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
            Movement.OnJumpStart += () =>
            {
                OnJump?.Invoke(Movement.CurrentHeight);
                PlayerInput.EnableMicInput = false;
            };
            Movement.OnJumpEnd += () => PlayerInput.EnableMicInput = true;
        }

        /// <summary>
        /// Wykonuje skok gracza, używając aktualnych pozycji kafelków.
        /// </summary>
        private void Jump()
        {
            Movement.Jump(_tileManager.CurrentPos, _tileManager.NextPos);
        }

        /// <summary>
        /// Aktualizuje logikę gracza w każdej klatce (wejście, testowe skoki).
        /// </summary>
        private void Update()
        {
            if (_dead)
                return;
            
            PlayerInput.Update();
            
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1)) Jump(.1f);
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha2)) Jump(.2f);
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha3)) Jump(.3f);
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha4)) Jump(.4f);
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha5)) Jump(.5f);
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha6)) Jump(.6f);
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha7)) Jump(.7f);
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha8)) Jump(.8f);
        }

        /// <summary>
        /// Wykonuje skok z określoną wysokością (używane w testach i debugowaniu).
        /// </summary>
        /// <param name="height">Wysokość skoku (0–1).</param>
        private void Jump(float height)
        {
            Movement.SetHeight(height);
            Movement.Jump(_tileManager.CurrentPos, _tileManager.NextPos);
        }

        /// <summary>
        /// Reaguje na kolizje — sprawdza, czy gracz uderzył w przeszkodę i jeśli tak, zabija gracza.
        /// </summary>
        /// <param name="collision">Kolizja wykryta przez trigger.</param>
        private void OnTriggerEnter(Collider collision)
        {
            var rb = collision.attachedRigidbody;
            if (rb == null)
                return;
            
            if (rb.CompareTag(_obstacleTag))
                Kill(rb.gameObject);
        }

        /// <summary>
        /// Zabija gracza po zderzeniu z przeszkodą i uruchamia efekt cząsteczkowy.
        /// </summary>
        /// <param name="obstacle">Obiekt przeszkody, który spowodował śmierć.</param>
        private void Kill(GameObject obstacle)
        {
            if (_dead)
                return;
            
            _destroyParticle.Play();
            Die();
            OnDeath?.Invoke(obstacle);
        }

        /// <summary>
        /// Zabija gracza bez powiązania z konkretną przeszkodą.
        /// </summary>
        public void Kill()
        {
            if (_dead)
                return;
            
            Die();
            OnDeath?.Invoke(null);
        }

        /// <summary>
        /// Oznacza gracza jako martwego i przełącza kamerę na ekran końcowy.
        /// </summary>
        private void Die()
        {
            _dead = true;
            CameraManager.SwitchCamera(CameraType.GameOver);
        }
    }
}
