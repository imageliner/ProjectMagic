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
        weaponHandSocket = GameObject.Find("DEF-hand.socket.R").transform;
        weaponBackSocket = GameObject.Find("DEF-back.socket").transform;
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
