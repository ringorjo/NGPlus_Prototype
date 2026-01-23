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

        public void UpdateSlot(InvetorySlot from, InvetorySlot to)
        {
            if (CanStack(from, to))
            {
                StackItems(from, to);
                return;
            }

            if (!to.IsOcuped)
            {
                MoveItem(from, to);
            }
        }

        private bool SlotsExist(InvetorySlot from, InvetorySlot to)
        {
            return from.ItemData != null && to.ItemData != null;
        }

        private bool CanStack(InvetorySlot from, InvetorySlot to)
        {
            if (!SlotsExist(from, to))
                return false;

            return from.ItemData.ItemID == to.ItemData.ItemID && from.ItemData.IsConsumable;
        }
        private void StackItems(InvetorySlot from, InvetorySlot to)
        {
            to.AddQuantity(from.ItemData.Quantity);
            from.InjectData(null);
        }
        private void MoveItem(InvetorySlot from, InvetorySlot to)
        {
            to.InjectData(from.ItemData);
            from.InjectData(null);
        }


        public void Register() => ServiceLocator.Instance.Register(this);

        public void Unregister() => ServiceLocator.Instance.Unregister<IService>(this);

    }
}
