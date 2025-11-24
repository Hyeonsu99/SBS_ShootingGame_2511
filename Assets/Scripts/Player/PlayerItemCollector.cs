using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerItemCollector : MonoBehaviour
{
    private void Awake()
    {
        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.radius = 2.5f;
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.TryGetComponent<IPickup>(out IPickup pickItem))
        {
            pickItem.OnPickup(transform.root.gameObject);
        }
        
    }
}
