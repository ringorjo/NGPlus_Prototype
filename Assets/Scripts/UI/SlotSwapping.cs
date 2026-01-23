using UnityEngine;
using UnityEngine.EventSystems;

namespace BGNS_Studios
{
    public class SlotSwapping : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
    {
        [SerializeField, Range(0, 1)]
        private float _alphaValue = .6f;
        private Vector3 _defaultPosition;
        private Transform _defaultParent;
        private CanvasGroup _canvasGroup;
        private InvetorySlot _slot;
        private InventoryManagerService _inventoryManagerService;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _slot = GetComponent<InvetorySlot>();
        }

        private void Start()
        {
            if (ServiceLocator.Instance.Exists<InventoryManagerService>())
                _inventoryManagerService = ServiceLocator.Instance.Get<InventoryManagerService>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = _alphaValue;
            transform.SetParent(transform.root);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1;
            transform.SetParent(_defaultParent);
            transform.localPosition = _defaultPosition;
            InvetorySlot _targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<InvetorySlot>();
            if (_targetSlot)
            {
                _inventoryManagerService?.UpdateSlot(_slot, _targetSlot);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _defaultParent = transform.parent;
            _defaultPosition = transform.localPosition;
        }
    }
}
