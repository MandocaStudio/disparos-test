using UnityEngine;

// Instancia runtime de armas para manejar stats modificables
[System.Serializable]
public class WeaponInstance
{
    public Weapon weaponData;

    public float currentDamage;
    public float currentFireRate;
    public float currentRange;

    public WeaponInstance(Weapon baseWeapon)
    {
        weaponData = baseWeapon;
        currentDamage = baseWeapon.baseDamage;
        currentFireRate = baseWeapon.baseFireRate;
        currentRange = baseWeapon.baseRange;
    }
}
