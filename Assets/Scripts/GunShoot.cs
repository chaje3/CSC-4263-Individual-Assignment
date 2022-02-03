using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    private float lastShootTime = 0;

    [SerializeField] private bool canShoot = true;

    public bool canReload = true;

    [SerializeField] private int primaryCurrentAmmo;
    [SerializeField] private int primaryCurrentAmmoStorage;
    [SerializeField] private int secondaryCurrentAmmo;
    [SerializeField] private int secondaryCurrentAmmoStorage;
    [SerializeField] private int tertiaryCurrentAmmo;
    [SerializeField] private int tertiaryCurrentAmmoStorage;

    [SerializeField] private bool primaryMagazineIsEmpty = false;
    [SerializeField] private bool secondaryMagazineIsEmpty = false;
    [SerializeField] private bool tertiaryMagazineIsEmpty = false;


    private Camera cam;
    private Inventory inventory;
    private EquipmentManager manager;
    private PlayerHUD hud;
    private PlayerStats stats;

    private void Start()
    {
        GetReferences();
        canShoot = true;
        canReload = true;
    }

    private void Update()
    {
        if(!stats.IsDead())
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                Shoot();
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                Reload(manager.currentlyEquipped);
            }
        }
        
    }

    private void RayCastShoot(Weapon currentWeapon)
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        RaycastHit hit;
        
        float currentWeaponRange = currentWeapon.range;

        if(Physics.Raycast(ray, out hit, currentWeaponRange))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.tag == "Enemy")
            {
                CharacterStats enemyStats = hit.transform.GetComponent<CharacterStats>();
                enemyStats.TakeDamage(currentWeapon.damage);
            }
        }

    }

    private void Shoot()
    {
        CheckCanShoot(manager.currentlyEquipped);

        if(canShoot && canReload)
        {
            Weapon currentWeapon = inventory.GetItem(manager.currentlyEquipped);
        
            if(Time.time > lastShootTime + currentWeapon.fireRate)
            {
                Debug.Log("Shoot");
                lastShootTime = Time.time;

                RayCastShoot(currentWeapon);
                UseAmmo((int)currentWeapon.weaponStyle, 1, 0);
            }
        }
        else
            Debug.Log("Empty");
        
    }

    private void UseAmmo(int slot, int currentAmmoUsed, int currentStoredAmmoUsed)
    {
        //Primary
        if(slot == 0)
        {
            if(primaryCurrentAmmo <= 0)
            {
                primaryMagazineIsEmpty = true;
                CheckCanShoot(manager.currentlyEquipped);
            }
            else
                primaryCurrentAmmo -= currentAmmoUsed;
                primaryCurrentAmmoStorage -= currentStoredAmmoUsed;
                hud.UpdateWeaponAmmoUI(primaryCurrentAmmo, primaryCurrentAmmoStorage);
        }
        //Secondary
        if(slot == 1)
        {
            if(secondaryCurrentAmmo <= 0)
            {
                secondaryMagazineIsEmpty = true;
                CheckCanShoot(manager.currentlyEquipped);
            }
            else
                secondaryCurrentAmmo -= currentAmmoUsed;
                secondaryCurrentAmmoStorage -= currentStoredAmmoUsed;
                hud.UpdateWeaponAmmoUI(secondaryCurrentAmmo, secondaryCurrentAmmoStorage);
        }
        //Tertiary
        if(slot == 2)
        {
            if(tertiaryCurrentAmmo <= 0)
            {
                tertiaryMagazineIsEmpty = true;
                CheckCanShoot(manager.currentlyEquipped);
            }
            else
                tertiaryCurrentAmmo -= currentAmmoUsed;
                tertiaryCurrentAmmoStorage -= currentStoredAmmoUsed;
                hud.UpdateWeaponAmmoUI(tertiaryCurrentAmmo, tertiaryCurrentAmmoStorage);
        }
    }

    private void AddAmmo(int slot, int currentAmmoAdded, int currentStoredAmmoAdded)
    {
        //Primary
        if(slot == 0)
        {
            primaryCurrentAmmo += currentAmmoAdded;
            primaryCurrentAmmoStorage += currentStoredAmmoAdded;
            hud.UpdateWeaponAmmoUI(primaryCurrentAmmo, primaryCurrentAmmoStorage);
        }
        //Secondary
        if(slot == 1)
        {
            secondaryCurrentAmmo += currentAmmoAdded;
            secondaryCurrentAmmoStorage += currentStoredAmmoAdded;
            hud.UpdateWeaponAmmoUI(secondaryCurrentAmmo, secondaryCurrentAmmoStorage);
        }
        //Tertiary
        if(slot == 2)
        {
            tertiaryCurrentAmmo += currentAmmoAdded;
            tertiaryCurrentAmmoStorage += currentStoredAmmoAdded;
            hud.UpdateWeaponAmmoUI(tertiaryCurrentAmmo, tertiaryCurrentAmmoStorage);
        }
    }

    private void Reload(int slot)
    {
        if(canReload)
        {
            if(slot == 0)
            {
                int ammoToReload = inventory.GetItem(0).magazineSize - primaryCurrentAmmo;

                if(primaryCurrentAmmoStorage >= ammoToReload)
                {
                    if(primaryCurrentAmmo == inventory.GetItem(0).magazineSize)
                    {
                        Debug.Log("Magazine full");
                        return;
                    }

                    AddAmmo(slot, ammoToReload, 0);
                    UseAmmo(slot, 0, ammoToReload);

                    primaryMagazineIsEmpty = false;
                    CheckCanShoot(slot);
                }
                else
                    Debug.Log("Not enough ammo");
                
            }
            if(slot == 1)
            {
                int ammoToReload = inventory.GetItem(1).magazineSize - secondaryCurrentAmmo;

                if(secondaryCurrentAmmoStorage >= ammoToReload)
                {
                    if(secondaryCurrentAmmo == inventory.GetItem(1).magazineSize)
                    {
                        Debug.Log("Magazine full");
                        return;
                    }

                    AddAmmo(slot, ammoToReload, 0);
                    UseAmmo(slot, 0, ammoToReload);

                    secondaryMagazineIsEmpty = false;
                    CheckCanShoot(slot);
                }
                else
                    Debug.Log("Not enough ammo");
            }
            if(slot == 2)
            {
                int ammoToReload = inventory.GetItem(2).magazineSize - tertiaryCurrentAmmo;

                if(tertiaryCurrentAmmoStorage >= ammoToReload)
                {
                    if(tertiaryCurrentAmmo == inventory.GetItem(2).magazineSize)
                    {
                        Debug.Log("Magazine full");
                        return;
                    }

                    AddAmmo(slot, ammoToReload, 0);
                    UseAmmo(slot, 0, ammoToReload);

                    tertiaryMagazineIsEmpty = false;
                    CheckCanShoot(slot);
                }
                else
                    Debug.Log("Not enough ammo");
            }
        }
        else
            Debug.Log("Can't reload at the moment");
    }

    private void CheckCanShoot(int slot)
    {
        if(slot == 0)
        {
            if(primaryMagazineIsEmpty)
                canShoot = false;
            else
                canShoot = true;
        }
        if(slot == 1)
        {
            if(secondaryMagazineIsEmpty)
                canShoot = false;
            else
                canShoot = true;
        }
        if(slot == 2)
        {
            if(tertiaryMagazineIsEmpty)
                canShoot = false;
            else
                canShoot = true;
        }
    }

    public void InitAmmo(int slot, Weapon weapon)
    {
        //Primary
        if(slot == 0)
        {
            primaryCurrentAmmo = weapon.magazineSize;
            primaryCurrentAmmoStorage = weapon.storeAmmo;
        }
        //Secondary
        if(slot == 1)
        {
            secondaryCurrentAmmo = weapon.magazineSize;
            secondaryCurrentAmmoStorage = weapon.storeAmmo;
        }
        //Tertiary
        if(slot == 2)
        {
            tertiaryCurrentAmmo = weapon.magazineSize;
            tertiaryCurrentAmmoStorage = weapon.storeAmmo;
        }
    }

    private void GetReferences()
    {
        cam = GetComponentInChildren<Camera>();
        inventory = GetComponent<Inventory>();
        manager = GetComponent<EquipmentManager>();
        hud = GetComponent<PlayerHUD>();
        stats = GetComponent<PlayerStats>();
    }

}
