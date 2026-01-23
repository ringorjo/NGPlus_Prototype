using UnityEngine;
namespace BGNS_Studios
{
    public class InventoryUIView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _inventory;
        private InputManagerService _inputManagerService;

        private void Start()
        {
            _inputManagerService = ServiceLocator.Instance.Get<InputManagerService>();
            if (_inputManagerService != null)
                _inputManagerService.OnInvetoryKeyPressed += OnInventory;
        }

        private void OnDestroy()
        {
            if (_inputManagerService != null)
                _inputManagerService.OnInvetoryKeyPressed -= OnInventory;
        }

        private void OnInventory()
        {
            _inventory.alpha = _inventory?.alpha == 0 ? 1 : 0;

        }
    }
}
