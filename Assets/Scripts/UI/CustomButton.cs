using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace BGNS_Studios
{

    public class CustomButton : Button
    {
        [SerializeField]
        private TextMeshProUGUI _buttonText;
        [SerializeField]
        private List<ButtonTextStyle> _states;

        public ButtonTextStyle GetButtonTextStyle(TextSelectionState state)
        {
            ButtonTextStyle result = _states.Find(s => s.State == state);
            return result;
        }

        protected override void Awake()
        {
            base.Awake();
        }
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            if(_states.Count==0)
                return;

            if (!IsInteractable())
            {
                _buttonText.color = GetButtonTextStyle(TextSelectionState.Disabled).TextColor;
                return;
            }
            _buttonText.color = GetButtonTextStyle((TextSelectionState)state).TextColor;
        }
    }
}

