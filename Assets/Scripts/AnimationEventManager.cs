using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    private EquipmentManager manager;
    private Inventory inventory;
    private GunShoot shooting;

    private void Start()
    {
        GetReferences();
    }

    public void DestroyWeapon()
    {
        Destroy(manager.currentWeaponObject);
    }

    public void InstantiateWeapon()
    {
        manager.currentWeaponObject = Instantiate(inventory.GetItem(manager.currentlyEquipped).prefab, manager.WeaponHolder);
    }

    public void StartReloading()
    {
        shooting.canReload = false;
    }

    public void EndReload()
    {
        shooting.canReload = true;
    }

    private void GetReferences()
    {
        inventory = GetComponentInParent<Inventory>();
        manager = GetComponentInParent<EquipmentManager>();
        shooting = GetComponentInParent<GunShoot>();
    }
}
