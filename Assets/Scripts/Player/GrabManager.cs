using UnityEngine;
using UnityEngine.UI;

public class GrabManager : MonoBehaviour
{
    [Header("Settings")]
    public float grabRadius = 2.5f; 

    [Header("References")]
    public PlayerInventory playerInventory;
    public Transform playerTransform;
    public Button grabButton;

    void Start()
    {
        grabButton.onClick.AddListener(GrabNearbyItems);
    }

    void GrabNearbyItems()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerTransform.position, grabRadius);

        foreach (Collider2D hit in hits)
        {
            DroppedItem dropped = hit.GetComponent<DroppedItem>();
            if (dropped != null)
            {
                playerInventory.AddItem(dropped.itemName, dropped.amount);
                Destroy(dropped.gameObject);
            }
        }
    }

   
    void OnDrawGizmosSelected()
    {
        if (playerTransform == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerTransform.position, grabRadius);
    }
}
