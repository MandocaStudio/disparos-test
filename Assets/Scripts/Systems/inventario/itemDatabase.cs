using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemClass> allItems;

    public ItemClass GetItemByID(string id)
    {
        return allItems.Find(item => item.itemID == id);
    }
}
