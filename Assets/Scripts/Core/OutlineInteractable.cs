using UnityEngine;

namespace BGNS_Studios
{
    public class OutlineInteractable : MonoBehaviour
    {
        [SerializeField]
        private Outline _outline;
        [SerializeField]
        private float _outlineWidth = 5f;
        [SerializeField]
        private Color _outlineColor = Color.yellow;

        private void Awake()
        {
            OnDeselect();
            _outline.OutlineColor = _outlineColor;
        }
       

        public void OnSelect() => _outline.OutlineWidth = _outlineWidth;
        public void OnDeselect() => _outline.OutlineWidth = 0f;

    }
}

