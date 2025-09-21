using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public ItemClass itemData;
    public int quantity;

    public ItemInstance(ItemClass data, int qty)
    {
        itemData = data;
        quantity = qty;
    }

    // Devuelve true si este item es un arma
    public bool IsWeapon()
    {
        return itemData.itemtypeEnum == ItemType.weapon;
    }
}