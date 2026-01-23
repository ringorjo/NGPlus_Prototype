using System.Collections.Generic;
using UnityEngine;

namespace BGNS_Studios
{
    [RequireComponent(typeof(InventoryManagerService))]
    public class InventoryDataPersistance : MonoBehaviour
    {
        [SerializeField]
        private InventoryManagerService _inventoryManager;

        private const string INVENTORY_DATA_FILENAME = "inventory_data";
        private JsonPersistence<InventoryDataCollection> _inventoryData;
        private InventoryDataCollection _data;
        private ItemsCollectionResources _itemsCollection;

        private void Awake()
        {
            _data = new InventoryDataCollection();
            _inventoryData = new JsonPersistence<InventoryDataCollection>(INVENTORY_DATA_FILENAME);
            _itemsCollection = new ItemsCollectionResources();
        }

        private void Start()
        {
            // LoadData();
        }

        private void Reset()
        {
            _inventoryManager = GetComponent<InventoryManagerService>();
        }

        private void OnApplicationQuit()
        {
            // SaveData();
        }


        [ContextMenu(nameof(LoadData))]
        private void LoadData()
        {
            _data = _inventoryData.Load();
            if (_data == null)
                throw new System.Exception("Loading Data error");

            _inventoryManager.CleanSlots();
            foreach (InventoryData inventoryData in _data.inventoryDatas)
            {
                InvetorySlot slot = _inventoryManager.Slots.Find(s => s.SlotId == inventoryData.slotID);
                ItemData itemData = _itemsCollection.GetItemByID(inventoryData.itemID);
                if (slot != null && itemData != null)
                {
                    slot.InjectData(itemData);
                }
            }
        }

        [ContextMenu(nameof(SaveData))]
        private void SaveData()
        {
            foreach (InvetorySlot slot in _inventoryManager.Slots)
            {
                if (!slot.IsOcuped)
                    continue;

                _data.inventoryDatas.Add(new InventoryData
                {
                    slotID = slot.SlotId,
                    itemID = slot.ItemData.ItemID,
                    quantity = slot.QuantityCount
                });
            }
            _inventoryData?.Save(_data);
        }

    }

    [System.Serializable]
    public class InventoryData
    {
        public string slotID;
        public string itemID;
        public int quantity;
    }
    [System.Serializable]
    public class InventoryDataCollection
    {
        public List<InventoryData> inventoryDatas=new List<InventoryData>();
    }

}
