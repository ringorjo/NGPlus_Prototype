using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGNS_Studios
{
    public class InventoryDescriptionView : MonoBehaviour
    {
        [Header("UI view References")]
        [SerializeField]
        private TextMeshProUGUI _tittle;
        [SerializeField]
        private TextMeshProUGUI _description;
        [SerializeField]
        private Image _icon;
        [Header("UI Interaction References")]
        [SerializeField]
        private Button _useButton;
        [SerializeField]
        private TextMeshProUGUI _useButtonText;
        [SerializeField]
        private Button _dropButton;
        private InventoryManagerService _inventoryManagerService;
        private InvetorySlot _currentItemData;

        private void Awake()
        {
            _useButton.onClick.AddListener(OnUse);
            _dropButton.onClick.AddListener(OnDrop);
            ChangeButtonState(null);
        }
        private void Start()
        {
            if (ServiceLocator.Instance.Exists<InventoryManagerService>())
            {
                _inventoryManagerService = ServiceLocator.Instance.Get<InventoryManagerService>();
                _inventoryManagerService.OnSlotDataSelected += UpdateDescription;
            }
        }

        private void OnDestroy()
        {
            if (_inventoryManagerService != null)
            {
                _inventoryManagerService.OnSlotDataSelected -= UpdateDescription;
            }
        }

        private void OnDrop()
        {
            _currentItemData?.DropItem();
        }

        private void OnUse()
        {
            _currentItemData.UseItem();
        }

        private void UpdateDescription(InvetorySlot slot)
        {
            _currentItemData = slot;
            ChangeButtonState(_currentItemData);
        }

        private void ChangeButtonState(InvetorySlot slot)
        {
            _dropButton.interactable = slot == null ? false : true;
            _useButton.interactable = slot == null ? false : true;
            _icon.enabled = slot == null ? false : true;
            _icon.sprite = slot == null ? null : slot.ItemData.Icon;
            _tittle.text = slot == null ? string.Empty : slot.ItemData.ItemName;
            _description.text = slot == null ? string.Empty : slot.ItemData.Description;
            if (slot != null)
                _useButtonText.text = slot.ItemData.IsConsumable ? "Use" : "Equip";
        }
    }
}
