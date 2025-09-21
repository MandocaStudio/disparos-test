using UnityEngine;

[System.Serializable]
public class ItemClass : ScriptableObject
{
    public string itemID;
    public string itemName;
    public string itemDescription;

    public int inventorySlots;

    public bool inventorySlotsCount;


    public Sprite itemIcon;
    //public int itemQuantity;
    public ItemType itemtypeEnum;


    public void Initialize(string name, string description, Sprite icon, ItemType type)
    {
        itemName = name;
        itemDescription = description;
        itemIcon = icon;
        itemtypeEnum = type;
    }
}

public enum ItemType
{
    weapon,
    consumable,
    key,
    questitem,
    accesory,
    Map
}


