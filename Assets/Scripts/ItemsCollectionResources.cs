using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BGNS_Studios
{
    public class ItemsCollectionResources
    {
        private Dictionary<string, ItemData> _itemsDictionary;

        public ItemsCollectionResources()
        {
            _itemsDictionary = new Dictionary<string, ItemData>();
            List<ItemData> loadedItems = Resources.LoadAll<ItemData>("Items").ToList();
            loadedItems.ForEach(item =>
            {
                _itemsDictionary.Add(item.ItemID, item);
                Debug.Log($"Loaded Item: {item.ItemID}");
            });
        }

        public ItemData GetItemByID(string itemID)
        {
            if (_itemsDictionary.ContainsKey(itemID))
                return _itemsDictionary[itemID];
            return null;
        }
    }
}