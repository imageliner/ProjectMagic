using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGearHandler : MonoBehaviour
{
    public WeaponObject weaponEquipped;
    [SerializeField] private Transform weaponHandSocket;
    [SerializeField] private Transform weaponBackSocket;

    [SerializeField] private WeaponItem currentWeapon;
    private GameObject weaponInstanceCombat;
    private GameObject weaponInstanceBack;

    [Header("Debug")]
    [SerializeField] private WeaponItem debug_Wep_WAR;
    [SerializeField] private WeaponItem debug_Wep_MAGE;
    [SerializeField] private WeaponItem debug_Wep_ARCH;


    private void Awake()
    {
        foreach (var t in GetComponentsInChildren<Transform>(true))
        {
            if (t.name == "DEF-hand.socket.R")
                weaponHandSocket = t;

            if (t.name == "DEF-back.socket")
                weaponBackSocket = t;
        }
    }

    

    #region DEBUG
    public void Debug_EquipWeapon_WAR() => EquipWeapon(debug_Wep_WAR);
    public void Debug_EquipWeapon_MAGE() => EquipWeapon(debug_Wep_MAGE);
    public void Debug_EquipWeapon_ARCH() => EquipWeapon(debug_Wep_ARCH);
    #endregion

    public void SheatheWeapon()
    {
        if (weaponInstanceBack != null || weaponInstanceCombat != null)
        {
            weaponInstanceCombat.SetActive(false);
            weaponInstanceBack.SetActive(true);
        }
    }

    public void UnsheatheWeapon()
    {
        if (weaponInstanceBack != null || weaponInstanceCombat != null)
        {
            weaponInstanceCombat.SetActive(true);
            weaponInstanceBack.SetActive(false);
        }
    }

    public void EquipWeapon(WeaponItem newWeapon)
    {
        if (currentWeapon != null)
        {
            currentWeapon.RemoveStatsFromPlayer();
            Destroy(weaponInstanceBack);
            Destroy(weaponInstanceCombat);
        }

        // If nothing replace with default
        if (newWeapon == null)
        {
            currentWeapon = Resources.Load<WeaponItem>("Default_WeaponItem");
            weaponEquipped = currentWeapon.GetWeaponObject();

            weaponInstanceBack = Instantiate(currentWeapon.gameObject, weaponBackSocket);
            weaponInstanceCombat = Instantiate(currentWeapon.gameObject, weaponHandSocket);
            weaponInstanceCombat.SetActive(false);
            return;
        }

        // Equip the new weapon
        currentWeapon = newWeapon;
        weaponEquipped = currentWeapon.GetWeaponObject();
        currentWeapon.AddStatsToPlayer();

        weaponInstanceBack = Instantiate(currentWeapon.gameObject, weaponBackSocket);
        weaponInstanceCombat = Instantiate(currentWeapon.gameObject, weaponHandSocket);
        weaponInstanceCombat.SetActive(false);
    }


    public void EquipWeapon_Old(WeaponItem newWeapon)
    {
        if (newWeapon == null)
        {
            currentWeapon.RemoveStatsFromPlayer();
            Destroy(weaponInstanceBack);
            Destroy(weaponInstanceCombat);
            currentWeapon = Resources.Load<WeaponItem>("Default_WeaponItem");
            weaponEquipped = currentWeapon.GetWeaponObject();
        }

        if (weaponInstanceBack != null || weaponInstanceCombat != null)
        {
            currentWeapon.RemoveStatsFromPlayer();
            Destroy(weaponInstanceBack);
            Destroy(weaponInstanceCombat);
            
        }

        currentWeapon = newWeapon;
        weaponEquipped = currentWeapon.GetWeaponObject();
        currentWeapon.AddStatsToPlayer();

        if (newWeapon != null)
        {
            weaponInstanceBack = Instantiate(newWeapon.gameObject, weaponBackSocket);
            weaponInstanceCombat = Instantiate(newWeapon.gameObject, weaponHandSocket);
            weaponInstanceCombat.SetActive(false);
            //currentWeapon.transform.localPosition = Vector3.zero;
            //currentWeapon.transform.localRotation = Quaternion.identity;
        }
    }

    public void GetCurrentWeaponClass()
    {
        weaponEquipped.GetClass();
    }
}
