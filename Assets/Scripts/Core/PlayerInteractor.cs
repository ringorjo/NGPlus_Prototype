using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGNS_Studios
{
    public class PlayerInteractor : MonoBehaviour, IService
    {
        [SerializeField, Range(-1f, 1f)]
        private float _dotTolerance;
        [SerializeField]
        private float _maxDistance;
        [SerializeField]
        private float _tickRate = 0.2f;

        private Camera _camera;
        private WaitForSeconds _seconds;
        private IInteractable _currentSelected;

        private List<IInteractable> _interactables = new List<IInteractable>();

        public void AddInteractable(IInteractable interactable)
        {
            if (!_interactables.Contains(interactable))
                _interactables.Add(interactable);
        }

        public void RemoveInteractable(IInteractable interactable)
        {
            if (_interactables.Contains(interactable))
                _interactables.Remove(interactable);
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
                        }  
                    }
                    else 
                    {
                        if(_currentSelected == interactable)
                        {
                            _currentSelected.OnLoseFocus();
                            _currentSelected = null;
                        }
                    }
                }

                yield return _seconds;
            }
        }

    }
}
