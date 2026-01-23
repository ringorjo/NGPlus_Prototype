using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BGNS_Studios
{
    public class InvetorySlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Image _selector;
        [SerializeField]
        private TextMeshProUGUI _quantity;
        [SerializeField]
        private int _slotSize = 10;
        private bool _isOcuped;
        private int _quantityCount;
        private ItemData _itemData;
        private InventoryManagerService _inventoryManagerService;

        public ItemData ItemData
        {
            get => _itemData;
        }
        public bool IsOcuped 
        { 
            get => _isOcuped;
        }


        private void Awake()
        {
            ClearData();
        }

        private void Start()
        {
            if (ServiceLocator.Instance.Exists<InventoryManagerService>())
                _inventoryManagerService = ServiceLocator.Instance.Get<InventoryManagerService>();
        }

        public void InjectData(ItemData data)
        {
            if(data==null)
            {
                ClearData();
                return;    
            }
            UpdateSlot(data);
        }

        private void UpdateSlot(ItemData data)
        {
            if (_itemData==null)
            {
                _itemData = data;
                _icon.sprite = data.Icon;
                _icon.enabled = true;
                _isOcuped = true;
            }

            AddQuantity(data.Quantity);
        }

        public void AddQuantity(int amount)
        {
            _quantityCount += amount;
            _quantity.text = $"X{_quantityCount}";
        }

        private void UpdateSlot()
        {
            if (!_isOcuped)
                return;
            _itemData?.UseItem();

            if (_quantityCount < 0 && _itemData.IsConsumable)
            {
                _quantityCount--;
                _quantity.text = $"X{_quantityCount}";
                return;
            }
            _isOcuped = false;
            ClearData();
        }

        public void UseItem()
        {
            UpdateSlot();
        }

        public void DropItem()
        {
            _itemData?.DropItem();
            _inventoryManagerService?.RemoveFromInventory(this);
            ClearData();
        }

        private void ClearData()
        {
            _itemData = null;
            _icon.enabled = false;
            _selector.enabled = false;
            _isOcuped = false;
            _quantity.text = string.Empty;
            _quantityCount = 0;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isOcuped)
                return;
            _selector.enabled = true;
            _inventoryManagerService?.SelectSlot(this);
        }
        public void OnUnSelect()
        {
            _selector.enabled = false;
        }
    }
}
