using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemClass itemData;
    public int quantity = 1;

    private void OnTriggerEnter(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            ItemInstance newItem = new ItemInstance(itemData, quantity);
            if (inventory.AddItem(newItem))
            {
                Destroy(gameObject);
            }
        }
    }
}

