using UnityEngine;
namespace BGNS_Studios
{
    public class ChestInteractable : BaseInteractable
    {
        [SerializeField]
        private Animator _animator;
        private const string OPEN_TRIGGER = "Open";
        public override string IterationName => "Open";


        public override void Interact()
        {
            _animator.SetTrigger(OPEN_TRIGGER);
            _interactableParticle?.Stop();
        }
        
    }
}
