
namespace BGNS_Studios
{
    public class PotionInteractable : BaseInteractable
    {
        public override void Interact()
        {
            base.Interact();
            Destroy(gameObject);

        }
    }
}
