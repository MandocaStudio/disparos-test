using UnityEngine;

[CreateAssetMenu(fileName = "NewArmor", menuName = "Inventory/Armor")]
public class Armor : ItemClass
{
    public int fisicDefense;
    public int magicDefense;
    public void Initialize(string name, string description, Sprite icon, int defensa, int magic)
    {
        base.Initialize(name, description, icon, ItemType.armor);
        itemtypeEnum = ItemType.armor;  // Asignación del tipo correctamente

        fisicDefense = defensa;
        magicDefense = magic;
    }
}

[CreateAssetMenu(fileName = "NewKey", menuName = "Inventory/Key")]
public class Key : ItemClass
{
    public string Name;
    public bool oneUse;
    public void Initialize(string name, string description, Sprite icon, bool Use, string keyName)
    {
        base.Initialize(name, description, icon, ItemType.key);
        itemtypeEnum = ItemType.key;  // Asignación del tipo correctamente

        oneUse = Use;

        Name = keyName;
    }

}

[CreateAssetMenu(fileName = "New Accessories", menuName = "Inventory/Accessory")]
public class accesories : ItemClass
{
    public void Initialize(string name, string description, Sprite icon)
    {
        base.Initialize(name, description, icon, ItemType.key);
        itemtypeEnum = ItemType.key;  // Asignación del tipo correctamente

    }

}

[CreateAssetMenu(fileName = "NewPotion", menuName = "Inventory/Potion")]
public class Potion : ItemClass
{

    public int potionAmount;

    public enum potionEffect
    {
        heal,
        mana,
        poison
        //no que mas
    }

    public potionEffect potionType;

    public void Initialize(string name, string description, Sprite icon, int amount, potionEffect Type)
    {
        base.Initialize(name, description, icon, ItemType.potion);
        itemtypeEnum = ItemType.potion;  // Asignación del tipo correctamente

        potionAmount = amount;
        potionType = Type;

    }

}

[CreateAssetMenu(fileName = "questItems", menuName = "Inventory/questItems")]
public class QuestItems : ItemClass
{

    public string questItemName;

    public void Initialize(string name, string description, Sprite icon, string itemName)
    {
        base.Initialize(name, description, icon, ItemType.questitems);
        itemtypeEnum = ItemType.questitems;  // Asignación del tipo correctamente

        questItemName = itemName;

    }

}


[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class Weapon : ItemClass
{

    public int physicDamage;

    public int magicDamage;

    public void Initialize(string name, string description, Sprite icon, int mDamage, int pDamage)
    {
        base.Initialize(name, description, icon, ItemType.weapon);
        itemtypeEnum = ItemType.weapon;  // Asignación del tipo correctamente

        physicDamage = mDamage;
        magicDamage = pDamage;
    }
}
