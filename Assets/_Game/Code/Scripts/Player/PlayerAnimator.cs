using UnityEngine;

namespace YNQ.JumpyJoe
{
    public class PlayerAnimator : MonoBehaviour, IPlayerComponent
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _deathAnim;
        
        public void Initialize(PlayerController playerController)
        {
            playerController.OnDeath += _ =>
            {
                _animator.Play(_deathAnim.name);
            };
        }
    }
}
