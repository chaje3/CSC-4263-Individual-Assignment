using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public int currentlyEquipped = 2;
    public GameObject currentWeaponObject = null;
    public Transform WeaponHolder = null;
    private Inventory inventory;
    private PlayerHUD hud;

    private void Start()
    {
        GetReferences();
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && currentlyEquipped != 0)
        {
            UnequipWeapon();
            EquipWeapon(inventory.GetItem(0));
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && currentlyEquipped != 1)
        {
            UnequipWeapon();
            EquipWeapon(inventory.GetItem(1));
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && currentlyEquipped != 2)
        {
            UnequipWeapon();
            EquipWeapon(inventory.GetItem(2));
        }
    }

    private void EquipWeapon(Weapon weapon)
    {
        currentlyEquipped = (int)weapon.weaponStyle;
        currentWeaponObject = Instantiate(weapon.prefab, WeaponHolder);
        hud.UpdateWeaponUI(weapon);
    }

    private void UnequipWeapon()
    {
        Destroy(currentWeaponObject);
    }

    private void GetReferences()
    {
        inventory = GetComponent<Inventory>();
        hud = GetComponent<PlayerHUD>();
    }
}
