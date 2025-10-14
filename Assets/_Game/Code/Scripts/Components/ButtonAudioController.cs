using UnityEngine;
using UnityEngine.EventSystems;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Odpowiada za odtwarzanie dźwięków przy najechaniu i kliknięciu przycisku UI.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class ButtonAudioController : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        [SerializeField] private AudioClip _onHoverSound;
        [SerializeField] private AudioClip _onClickSound;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Odtwarza dźwięk po najechaniu kursorem na przycisk.
        /// </summary>
        /// <param name="eventData">Dane zdarzenia kursora.</param>
        public void OnPointerEnter(PointerEventData eventData) => _audioSource.PlayOneShot(_onHoverSound);

        /// <summary>
        /// Odtwarza dźwięk po kliknięciu przycisku.
        /// </summary>
        /// <param name="eventData">Dane zdarzenia kliknięcia.</param>
        public void OnPointerDown(PointerEventData eventData) => _audioSource.PlayOneShot(_onClickSound);
    }
}