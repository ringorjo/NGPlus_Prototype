using System;
using System.Collections.Generic;
using UnityEngine;

namespace BGNS_Studios
{
    [Serializable]
    public class PlayerArmor
    {
        public string Name;
        public bool IsEquiped;
        public bool IsWeapon;
        public List<GameObject> ArmorElements;

        public void UpdateArmorVisibility(bool isvisible)
        {
            ArmorElements.ForEach(a => a.SetActive(isvisible));
            IsEquiped = isvisible;
        }
    }
}
