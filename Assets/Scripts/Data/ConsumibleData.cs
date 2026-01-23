using UnityEngine;

namespace BGNS_Studios
{

    [CreateAssetMenu(fileName = "ConsumibleData", menuName = "Inventory/ConsumibleData")]
    public class ConsumibleData : ItemData
    {
        [SerializeField]
        private bool _isForHealth;
        [SerializeField,Range(0,1)]
        private float _percentageEffectivity;
        public override void DropItem()
        {

        }

        public override void UseItem()
        {
            if (_isForHealth)
            {
                ServiceLocator.Instance.Get<PlayerStats>().UpdateHealthLevel(_percentageEffectivity);
                return;
            }
            ServiceLocator.Instance.Get<PlayerStats>().UpdateManaLevel(_percentageEffectivity);

        }
    }
}

