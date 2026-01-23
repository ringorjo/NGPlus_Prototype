using System.Transactions;
using UnityEngine;

namespace BGNS_Studios
{
    public interface IInteractable
    {
        Transform GetTransform();
        void Interact();    

        void OnFocus();
        void OnLoseFocus();
    }
}
