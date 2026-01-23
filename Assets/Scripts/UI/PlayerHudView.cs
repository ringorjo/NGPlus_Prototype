using UnityEngine;
using UnityEngine.UI;

namespace BGNS_Studios
{
    public class PlayerHudView : MonoBehaviour
    {
        [SerializeField]
        private Image _healthBar;
        [SerializeField]
        private Image _manaBar;
        private PlayerStats _playerStats;

        private void Start()
        {
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
            if (_playerStats != null)
            {
                _playerStats.OnHealthChanged += OnHealthChanged;
                _playerStats.OnManaLevelChanged += OnManaChanaged;
            }
        }

        private void OnDestroy()
        {
            if (_playerStats != null)
            {
                _playerStats.OnHealthChanged -= OnHealthChanged;
                _playerStats.OnManaLevelChanged -= OnManaChanaged;
            }
        }

        private void OnManaChanaged(float mana) => _manaBar.fillAmount = mana / 100;

        private void OnHealthChanged(float health) => _healthBar.fillAmount = health / 100;

    }
}
