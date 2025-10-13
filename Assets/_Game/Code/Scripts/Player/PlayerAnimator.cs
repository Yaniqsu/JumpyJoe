using UnityEngine;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Odpowiada za animacje gracza, w tym odtwarzanie animacji śmierci.
    /// </summary>
    public class PlayerAnimator : MonoBehaviour, IPlayerComponent
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _deathAnim;
        
        /// <summary>
        /// Inicjalizuje animatora i przypina reakcję na zdarzenie śmierci gracza.
        /// </summary>
        /// <param name="playerController">Kontroler gracza, którego zdarzenia są nasłuchiwane.</param>
        public void Initialize(PlayerController playerController)
        {
            playerController.OnDeath += _ =>
            {
                _animator.Play(_deathAnim.name);
            };
        }
    }
}