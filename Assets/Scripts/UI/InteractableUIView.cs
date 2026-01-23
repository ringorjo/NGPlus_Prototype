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
        private IInteractable _currentInteractable;
        private RectTransform _recTransform;

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

            enabled = isvisible;
            _canvasGroup.alpha = isvisible ? 1 : 0;
            _currentInteractable= interactable;
            if (isvisible)
                _label.text = $"To {interactable.IterationName}";
        }

        private void Update()
        {
            if (_currentInteractable == null)
                return;

            Vector3 screenPos = Camera.main.WorldToScreenPoint(_currentInteractable.GetTransform().position);
            _recTransform.position = screenPos + _offsetLabel;

        }
    }
}
