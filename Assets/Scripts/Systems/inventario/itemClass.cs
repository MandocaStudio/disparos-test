using UnityEngine;

[System.Serializable]
public class ItemClass : ScriptableObject
{
    public string itemID;
    public string itemName;
    public string itemDescription;

    public Vector2Int itemSize = Vector2Int.one;

    public bool isStackable;
    public int maxStack = 1;

    public bool inventorySlotsCount;

    public Sprite itemIcon;
    //public int itemQuantity;
    public ItemType itemtypeEnum;

    public void Initialize(string name, string description, Sprite icon, ItemType type, Vector2Int Size, bool stackable, int maxStackSize)
    {
        itemName = name;
        itemDescription = description;
        itemIcon = icon;
        itemtypeEnum = type;
        itemSize = Size;
        isStackable = stackable;
        maxStack = maxStackSize;
    }
}

public enum ItemType
{
    weapon,
    consumable,
    key,
    questitem,
    accesory,
    Map,
    ammo
}


