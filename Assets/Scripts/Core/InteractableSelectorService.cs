using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGNS_Studios
{
    public class InteractableSelectorService : MonoBehaviour, IService
    {
        public event Action<bool, IInteractable> OnInteractableSelectorChanged;
        [SerializeField, Range(-1f, 1f)]
        private float _dotTolerance;
        [SerializeField]
        private float _maxDistance;
        [SerializeField]
        private float _tickRate = 0.2f;

        private Camera _camera;
        private WaitForSeconds _seconds;
        private IInteractable _currentSelected;
        private InputManagerService _inputManagerService;

        private List<IInteractable> _interactables = new List<IInteractable>();
        private bool _isfocusing = true;

        public void AddInteractable(IInteractable interactable)
        {
            if (!_interactables.Contains(interactable))
                _interactables.Add(interactable);
        }

        public void RemoveInteractable(IInteractable interactable)
        {
            if (_interactables.Contains(interactable))
            {
                CleanInteractable();
                _interactables.Remove(interactable);
            }
        }

        private void Awake()
        {
            Register();
            _camera = Camera.main;
            _seconds = new WaitForSeconds(_tickRate);
        }

        private void Start()
        {
            StartCoroutine(UpdateAsync());
            _inputManagerService = ServiceLocator.Instance.Get<InputManagerService>();
            if (_inputManagerService != null)
            {
                _inputManagerService.OnInteractableKeyPressed += OnInteract;
                _inputManagerService.OnFocusing += OnGameFocus;
            }
        }

        private void OnGameFocus(bool isfocus) => _isfocusing = isfocus;


        private void OnDestroy()
        {
            Unregister();
            if (_inputManagerService != null)
                _inputManagerService.OnInteractableKeyPressed -= OnInteract;
        }

        public void Register()
        {
            ServiceLocator.Instance.Register(this);
        }

        public void Unregister()
        {
            ServiceLocator.Instance.Unregister(this);
        }

        private IEnumerator UpdateAsync()
        {
            while (true)
            {
                foreach (var interactable in _interactables)
                {
                    Vector3 directionToInteractable = interactable.GetTransform().position - _camera.transform.position;
                    float distanceToInteractable = Vector3.Distance(interactable.GetTransform().position, _camera.transform.position);
                    directionToInteractable.Normalize();
                    float dotProduct = Vector3.Dot(_camera.transform.forward, directionToInteractable);
                    if (dotProduct >= _dotTolerance && distanceToInteractable <= _maxDistance)
                    {
                        if (_currentSelected == null)
                        {
                            _currentSelected = interactable;
                            _currentSelected.OnFocus();
                            OnInteractableSelectorChanged?.Invoke(true, _currentSelected);
                        }
                    }
                    else
                    {
                        if (_currentSelected == interactable)
                        {
                            CleanInteractable();
                        }
                    }
                }

                yield return _seconds;
            }
        }

        private void OnInteract()
        {
            if (_isfocusing)
                _currentSelected?.Interact();
        }

        private void CleanInteractable()
        {
            _currentSelected?.OnLoseFocus();
            _currentSelected = null;
            OnInteractableSelectorChanged?.Invoke(false, null);

        }
    }
}
