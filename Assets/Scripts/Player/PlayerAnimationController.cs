using UnityEngine;

namespace BGNS_Studios
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        private Player _player;
        private const string INTERACTION_ANIMATION_ID = "Pickable";
        private const string EQUIPED_ANIMATION_ID = "IsEquiped";


        private InputManagerService _inputManagerService;

        private void Start()
        {
            _player = GetComponent<Player>();
            _player.OnWeaponEquiped += OnWeaponEquiped;
            _inputManagerService = ServiceLocator.Instance.Get<InputManagerService>();
            if (_inputManagerService != null)
            {
                _inputManagerService.OnInteractableKeyPressed += OnInteractAnimation;
            }
        }

        private void OnDestroy()
        {
            if (_inputManagerService != null)
            {
                _inputManagerService.OnInteractableKeyPressed -= OnInteractAnimation;
            }
        }

        private void OnWeaponEquiped(bool isequiped)=> _animator?.SetBool(EQUIPED_ANIMATION_ID, isequiped);

        private void OnInteractAnimation()=> _animator?.SetTrigger(INTERACTION_ANIMATION_ID);
      
    }
}
