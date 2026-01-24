using BGNS_Studios;
using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		private bool _isFocusing = true;
        private InputManagerService _inputManagerService;


        private void Start()
        {
            _inputManagerService=ServiceLocator.Instance.Get<InputManagerService>();
			if (_inputManagerService != null)
                _inputManagerService.OnFocusing += OnFocusGame;
        }

        private void OnFocusGame(bool isfocus)
        {
            _isFocusing=isfocus;
			Debug.Log("Focus: " + _isFocusing);
            cursorLocked =isfocus;
			cursorInputForLook=isfocus;
            SetCursorState(cursorLocked);
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			Vector2 newValue= _isFocusing ? value.Get<Vector2>() :Vector2.zero;
			MoveInput(newValue);
		}

		public void OnLook(InputValue value)
		{

            if (cursorInputForLook)
			{
                Vector2 newValue = _isFocusing ? value.Get<Vector2>() : Vector2.zero;
                LookInput(newValue);
			}
		}

		public void OnJump(InputValue value)
		{
            if (!_isFocusing)
                return;
            JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
            if (!_isFocusing)
                return;
            SprintInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
        }

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}