using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public ItemClass itemData;
    public int quantity;

    // Si es un arma, se asigna automáticamente
    public WeaponInstance weaponInstance;

    public ItemInstance(ItemClass data, int amount = 1)
    {
        itemData = data;
        quantity = amount;

        if (data.itemtypeEnum == ItemType.weapon)
        {
            Weapon weaponData = data as Weapon;
            weaponInstance = new WeaponInstance(weaponData);
        }
    }

    public bool IsStackableWith(ItemInstance other)
    {
        if (itemData.itemtypeEnum == ItemType.weapon) return false;
        if (!itemData.isStackable || !other.itemData.isStackable) return false;
        return itemData.itemID == other.itemData.itemID;
    }
}