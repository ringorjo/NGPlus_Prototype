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
        private PlayerInteractor _playerInteractor;

        private void Reset()
        {
            _outline = GetComponent<OutlineInteractable>();
        }

        private void Start()
        {
            if (ServiceLocator.Instance.Exists<InventoryManagerService>())
                _inventoryManagerService = ServiceLocator.Instance.Get<InventoryManagerService>();
            if (ServiceLocator.Instance.Exists<PlayerInteractor>())
                _playerInteractor = ServiceLocator.Instance.Get<PlayerInteractor>();

            _playerInteractor?.AddInteractable(this);
            _interactableParticle?.Play();
        }

        private void OnDestroy()
        {
            _playerInteractor?.RemoveInteractable(this);
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
