using TMPro;
using UnityEngine;

namespace BGNS_Studios
{
    public class InteractableUIView : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private TextMeshProUGUI _label;
        [SerializeField]
        private Vector3 _offsetLabel;
        private RectTransform _recTransform;
        private Transform _interactableTransform;

        private InteractableSelectorService _interactableSelector;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (ServiceLocator.Instance.Exists<InteractableSelectorService>())
            {
                _interactableSelector = ServiceLocator.Instance.Get<InteractableSelectorService>();
                _interactableSelector.OnInteractableSelectorChanged += OnSelectInteractable;
            }
            _recTransform = GetComponent<RectTransform>();
            _canvasGroup.alpha = 0;
            enabled = false;

        }

        private void OnDestroy()
        {
            if (_interactableSelector != null)
                _interactableSelector.OnInteractableSelectorChanged -= OnSelectInteractable;

        }

        private void OnSelectInteractable(bool isvisible, IInteractable interactable)
        {
            _canvasGroup.alpha = isvisible ? 1 : 0;
            if (isvisible)
                _label.text = $"To {interactable.IterationName}";
            enabled = isvisible;
            _interactableTransform = interactable == null ? null: interactable.GetTransform();
        }

        private void Update()
        {
            if (_interactableTransform == null)
                return;

            Vector3 screenPos = Camera.main.WorldToScreenPoint(_interactableTransform.position);
            _recTransform.position = screenPos + _offsetLabel;

        }
    }
}
