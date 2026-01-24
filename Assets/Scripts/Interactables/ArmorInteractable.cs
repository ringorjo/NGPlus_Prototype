using UnityEngine;
namespace BGNS_Studios
{
    public class ArmorInteractable : BaseInteractable
    {
        public override void Interact()
        {
            base.Interact();
            Destroy(gameObject);
        }
    }
}
