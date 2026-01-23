using UnityEngine;
namespace BGNS_Studios
{
    public class ChestInteractable : BaseInteractable
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private AudioSource _audioSource;
        private const string ANIMATION_STATE = "Open";
        public override string IterationName => "Open";


        public override void Interact()
        {
            _animator.Play(ANIMATION_STATE);
            _interactableParticle?.Stop();
            _audioSource?.Play();
            _interactableSelector.RemoveInteractable(this);
        }

    }
}
