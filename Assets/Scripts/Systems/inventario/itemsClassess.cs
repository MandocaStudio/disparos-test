using System;
using UnityEngine;


[CreateAssetMenu(fileName = "NewKey", menuName = "Inventory/Key")]
public class Key : ItemClass
{
    public string Name;
    public bool oneUse;
    public void Initialize(string name, string description, Sprite icon, bool Use, string keyName)
    {
        base.Initialize(name, description, icon, ItemType.key);

        oneUse = Use;

        Name = keyName;
    }

}

// [CreateAssetMenu(fileName = "New Accessories", menuName = "Inventory/Accessory")]
// public class accesories : ItemClass
// {
//     public void Initialize(string name, string description, Sprite icon)
//     {
//         base.Initialize(name, description, icon, ItemType.key);
//         itemtypeEnum = ItemType.key;  // Asignación del tipo correctamente

//     }

// }

[CreateAssetMenu(fileName = "NewConsumable", menuName = "Inventory/Potion")]
public class consumable : ItemClass
{

    public int potionAmount;

    public enum consumableType
    {
        heal,
        cordura,
        other,
        FlashLightBatery
        //no que mas
    }

    public consumableType potionType;

    public void Initialize(string name, string description, Sprite icon, int amount, consumableType Type)
    {
        base.Initialize(name, description, icon, ItemType.consumable);

        potionAmount = amount;
        potionType = Type;

    }

}

[CreateAssetMenu(fileName = "questItems", menuName = "Inventory/questItems")]
public class questitem : ItemClass
{

    public string questItemName;

    public void Initialize(string name, string description, Sprite icon, string itemName)
    {
        base.Initialize(name, description, icon, ItemType.questitem);

        questItemName = itemName;

    }

}


[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class Weapon : ItemClass
{
    public float baseDamage;
    public float baseFireRate;
    public float baseRange;

    public GameObject crosshairPrefab; // referencia a la mira
    public GameObject weaponPrefab; // Aquí enlazas el prefab del modelo



    public void Initialize(string name, string description, Sprite icon, float Damage, float fireRate, GameObject crosshair, GameObject prefab, float Range)
    {
        base.Initialize(name, description, icon, ItemType.weapon);

        baseDamage = Damage;
        baseRange = Range;
        baseFireRate = fireRate;
        crosshairPrefab = crosshair;
        weaponPrefab = prefab;

    }
}


[CreateAssetMenu(fileName = "NewWeaponAmount", menuName = "Inventory/Amount")]
public class ammo : ItemClass
{
    public enum AmountType
    {
        shootgun,
        pistol,
        rifle,
        smp
        //no que mas
    }

    public AmountType amountTypeVar; // referencia a la mira


    public void Initialize(string name, string description, Sprite icon, AmountType amountType)
    {
        base.Initialize(name, description, icon, ItemType.weapon);

        amountTypeVar = amountType;

    }
}