using UnityEngine;

public class itemPickup : MonoBehaviour
{
    public ItemClass itemData;
    [Tooltip("Cantidad que dará este pickup al recogerlo")]
    public int quantity = 1;

    // private void OnTriggerEnter(Collider other)
    // {
    //     Inventory inventory = other.GetComponent<Inventory>();
    //     if (inventory != null)
    //     {
    //         bool added = inventory.AddItem(itemData, quantity);
    //         if (added)
    //         {
    //             Destroy(gameObject);
    //         }
    //     }
    // }
}
