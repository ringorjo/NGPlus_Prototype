using UnityEngine;

namespace BGNS_Studios
{
    [RequireComponent(typeof(OutlineInteractable))]
    public abstract class BaseInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        protected ItemData _itemData;
        protected InventoryManagerService _inventoryManagerService;
        [Header("Items VFX")]
        [SerializeField]
        protected OutlineInteractable _outline;
        [SerializeField]
        protected ParticleSystem _interactableParticle;
        protected InteractableSelectorService _interactableSelector;

        public virtual string IterationName => "Interact";

        private void Reset()
        {
            _outline = GetComponent<OutlineInteractable>();
        }

        private void Start()
        {
            if (ServiceLocator.Instance.Exists<InventoryManagerService>())
                _inventoryManagerService = ServiceLocator.Instance.Get<InventoryManagerService>();
            if (ServiceLocator.Instance.Exists<InteractableSelectorService>())
                _interactableSelector = ServiceLocator.Instance.Get<InteractableSelectorService>();

            _interactableSelector?.AddInteractable(this);
            _interactableParticle?.Play();
        }

        private void OnDestroy()
        {
            _interactableSelector?.RemoveInteractable(this);
        }

        [ContextMenu(nameof(Interact))]
        public virtual void Interact()
        {
            _inventoryManagerService?.AddToInventory(_itemData);
        }
        public virtual void OnFocus()
        {
            _outline?.OnSelect();
        }
        public virtual void OnLoseFocus()
        {
            _outline?.OnDeselect();
        }

        public Transform GetTransform()
        {
           return transform;
        }
    }
}
