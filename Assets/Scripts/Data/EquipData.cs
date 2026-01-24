using UnityEngine;

namespace BGNS_Studios
{
    [CreateAssetMenu(fileName = "EquipData", menuName = "Inventory/EquipData")]
    public class EquipData : ItemData
    {

        public override void UseItem()
        {
            ServiceLocator.Instance.Get<Player>().ChangeArmorVisibility(ItemID, true);
        }

        public override void DropItem()
        {
            base.DropItem();
            ServiceLocator.Instance.Get<Player>().ChangeArmorVisibility(ItemID, false);

        }
    }
}

