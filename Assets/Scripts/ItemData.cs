using UnityEngine;

namespace BGNS_Studios
{



    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public abstract class ItemData : ScriptableObject
    {
        public Sprite Icon;
        public int ItemID;
        public string ItemName;
        public int Quantity;
        public bool IsConsumable;
        [TextArea]
        public string Description;


        public abstract void UseItem();


       public abstract void DropItem();
    }

}
