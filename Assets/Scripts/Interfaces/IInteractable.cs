using System.Transactions;
using UnityEngine;

namespace BGNS_Studios
{
    public interface IInteractable
    {
        public string IterationName { get; }
        Transform GetTransform();
        void Interact();    

        void OnFocus();
        void OnLoseFocus();
    }
}
