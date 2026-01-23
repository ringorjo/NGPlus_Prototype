using System;
using System.Threading.Tasks;
using UnityEngine;

namespace BGNS_Studios
{
    public class PlayerStats : MonoBehaviour, IService
    {
        public event Action<float> OnHealthChanged;
        public event Action<float> OnManaLevelChanged;

        [SerializeField, Range(0, 100)]
        private float _health;
        [SerializeField, Range(0, 100)]
        private float _mana;

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
