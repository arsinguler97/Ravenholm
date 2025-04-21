using UnityEngine;

public class BasketTrigger : MonoBehaviour
{
    public enum TriggerType { Top, Bottom }
    public TriggerType triggerType;
    public BasketManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("canPickUp"))
        {
            manager.RegisterTrigger(triggerType, other);
        }
    }
}