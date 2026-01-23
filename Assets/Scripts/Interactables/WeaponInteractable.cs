namespace BGNS_Studios
{
    public class WeaponInteractable : BaseInteractable
    {
        public override void Interact()
        {
            base.Interact();
            Destroy(gameObject);
        }
    }
}
