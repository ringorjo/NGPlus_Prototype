using System;
using System.Collections.Generic;
using UnityEngine;

namespace BGNS_Studios
{
    public class InventoryManagerService : MonoBehaviour, IService
    {
        private List<InvetorySlot> _slots = new List<InvetorySlot>();
        public event Action<InvetorySlot> OnSlotDataSelected;
        private InvetorySlot _currentSlotSelected;
        private void Awake()
        {
            Register();
        }

        public void AddSlot(InvetorySlot slot)
        {
            if (!_slots.Contains(slot))
            {
                _slots.Add(slot);
            }
        }

        public void RemoveSlot(InvetorySlot slot)
        {
            if (_slots.Contains(slot))
            {
                _slots.Remove(slot);
            }
        }



        private void OnDestroy()
        {
            Unregister();
        }

        public void SelectSlot(InvetorySlot slot)
        {
            _currentSlotSelected?.OnUnSelect();
            _currentSlotSelected = slot;
            OnSlotDataSelected?.Invoke(_currentSlotSelected);
        }

        public void SwapSlot(InvetorySlot from, InvetorySlot to)
        {
            from?.InjectData(null);
            to?.InjectData(from.ItemData);
            // limpiar el slot from y pasar los datos al slot to

        }


        public void Register() => ServiceLocator.Instance.Register(this);

        public void Unregister() => ServiceLocator.Instance.Unregister<IService>(this);

    }
}
