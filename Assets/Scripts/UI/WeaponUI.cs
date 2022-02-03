using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text magazineSizeText;
    [SerializeField] private Text storeAmmoText;

    public void UpdateInfo(Sprite weaponIcon, int magazineSize, int storeAmmo)
    {
        icon.sprite = weaponIcon;
        magazineSizeText.text = magazineSize.ToString();
        storeAmmoText.text = storeAmmo.ToString();
    }
    
    public void UpdateAmmoUI(int magazineSize, int storeAmmo)
    {
        magazineSizeText.text = magazineSize.ToString();
        storeAmmoText.text = storeAmmo.ToString();
    }

}
