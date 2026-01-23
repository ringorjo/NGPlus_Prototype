using BGNS_Studios;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Input;

namespace BGNS_Studios
{
    public class InputManagerService : MonoBehaviour, IService, IHudActions, IInteractionActions
    {
        public event Action OnInvetoryKeyPressed;
        public event Action OnInteractableKeyPressed;
        public event Action<bool> OnFocusing;
        private Input _input;
        private bool _isfocusing = true;
        private void Awake()
        {
            Register();
        }
        private void OnEnable()
        {
            if (_input == null)
            {
                _input = new Input();
                _input.Interaction.SetCallbacks(this);
                _input.Hud.SetCallbacks(this);
            }
           
            _input.Enable();
        }
        private void OnDestroy()
        {
            Unregister();
        }
        public void Register()
        {
            ServiceLocator.Instance.Register(this);
        }

        public void Unregister()
        {
            ServiceLocator.Instance?.Unregister(this);
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            bool isPressed = context.performed;
            if (isPressed)
            {
                OnInvetoryKeyPressed?.Invoke();
                _isfocusing = !_isfocusing;
                OnFocusing?.Invoke(_isfocusing);
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            bool isPressed = context.performed;
            if (isPressed)
                OnInteractableKeyPressed?.Invoke();
        }
    }
}