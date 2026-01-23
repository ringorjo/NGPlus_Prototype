using UnityEngine;
namespace BGNS_Studios
{
    public class NpcInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private AudioSource _audioSource;
        [Header("Items VFX")]
        [SerializeField]
        private OutlineInteractable _outline;
        private const string TRIGGER_NAME = "Interact";
        private InteractableSelectorService _interactableSelector;


        private void Start()
        {
            if (ServiceLocator.Instance.Exists<InteractableSelectorService>())
                _interactableSelector = ServiceLocator.Instance.Get<InteractableSelectorService>();

            _interactableSelector?.AddInteractable(this);
        }
        private void Reset()
        {
            _outline = GetComponent<OutlineInteractable>();
        }
        private void OnDestroy()
        {
            _interactableSelector?.RemoveInteractable(this);
        }
        public string IterationName => "Talk";

        public Transform GetTransform()
        {
            return transform;
        }

        public void Interact()
        {
            if (_audioSource.isPlaying)
                return;
            _animator?.SetTrigger(TRIGGER_NAME);
            _audioSource.PlayDelayed(.3f);
        }

        public virtual void OnFocus()
        {
            _outline?.OnSelect();
        }
        public virtual void OnLoseFocus()
        {
            _outline?.OnDeselect();
        }
    }
}
