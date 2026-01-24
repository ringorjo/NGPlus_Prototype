using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BGNS_Studios
{
    public class Player : MonoBehaviour, IService
    {

        public event Action<float> OnHealthChanged;
        public event Action<float> OnManaLevelChanged;
        public event Action<bool> OnWeaponEquiped;

        [SerializeField, Range(0, 100)]
        private float _health;
        [SerializeField, Range(0, 100)]
        private float _mana;

        [SerializeField]
        private Transform _itemDropSpawn;

        [SerializeField]
        private List<PlayerArmor> _armorElements;

        public Transform ItemDropSpawn 
        { 
            get => _itemDropSpawn; 
        }

        public void ChangeArmorVisibility(string name, bool isvisible)
        {
            PlayerArmor armor = _armorElements.Find(a => a.Name == name);
            if (armor != null)
            {
                armor.UpdateArmorVisibility(isvisible);
                WeaponRequest(armor);
            }
        }

        private void WeaponRequest(PlayerArmor weapon)
        {
            if (!weapon.IsWeapon)
                return;

            OnWeaponEquiped?.Invoke(weapon.IsEquiped);
        }


        private void Awake()
        {
            Register();
        }
        private async void Start()
        {
            await Task.Delay(200);
            UpdateHealthLevel(.3f);
            UpdateManaLevel(.25f);
        }
        private void OnDestroy()
        {
            Unregister();
        }

        public void UpdateHealthLevel(float percentage)
        {
            if (_health > 100)
                return;

            _health += 100 * percentage;
            OnHealthChanged?.Invoke(_health);
        }

        public void UpdateManaLevel(float percentage)
        {
            if (_mana > 100)
                return;

            _mana += 100 * percentage;
            OnManaLevelChanged?.Invoke(_mana);
        }

        public void Register()
        {
            ServiceLocator.Instance.Register(this);
        }

        public void Unregister()
        {
            ServiceLocator.Instance.Unregister(this);
        }
    }
}
