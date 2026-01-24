using UnityEngine;

namespace BGNS_Studios
{
    public abstract class ItemData : ScriptableObject
    {
        public Sprite Icon;
        public GameObject prefab;
        public string ItemID;
        public string ItemName;
        public int Quantity;
        public bool IsConsumable;
        [TextArea]
        public string Description;
        public abstract void UseItem();
        public virtual void DropItem()
        {
            Transform spawn = ServiceLocator.Instance.Get<Player>().ItemDropSpawn;
            Instantiate(prefab, spawn.position, Quaternion.identity);// Refactor Use Pool
        }
    }

}
