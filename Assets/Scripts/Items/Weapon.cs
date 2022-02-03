using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "Item/Weapon")]
public class Weapon : Items
{
    public GameObject prefab;
    public int damage;
    public int magazineSize;
    public int storeAmmo;
    public float fireRate;
    public float range;
    public WeaponType weaponType;
    public WeaponStyle weaponStyle;
}

public enum WeaponType{Melee, Pistol, AR, Shotgun, Sniper}
public enum WeaponStyle{Primary, Secondary, Tertiary};
