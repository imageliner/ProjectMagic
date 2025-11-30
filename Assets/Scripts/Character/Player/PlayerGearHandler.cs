using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGearHandler : MonoBehaviour
{
    [Header("Weapon")]
    public GearObject weaponEquipped;
    [SerializeField] private Transform weaponHandSocket;
    [SerializeField] private Transform weaponBackSocket;

    [SerializeField] private GearItem currentWeapon;
    private GameObject weaponInstanceCombat;
    private GameObject weaponInstanceBack;

    [Header("Helmet")]
    public GearObject helmetEquipped;
    [SerializeField] private Transform helmetSocket;

    [SerializeField] private GearItem currentHelmet;
    private GameObject helmetInstance;



    private void Awake()
    {
        foreach (var t in GetComponentsInChildren<Transform>(true))
        {
            if (t.name == "DEF-hand.socket.R")
                weaponHandSocket = t;

            if (t.name == "DEF-back.socket")
                weaponBackSocket = t;

            if (t.name == "DEF-head")
                helmetSocket = t;
        }
    }


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

    public void EquipGearType(GearItem newGear, string newType)
    {
        if (newGear == null)
        {
            if (newType == "Weapon")
            {
                EquipWeapon(null);
            }
            if (newType == "Helmet")
            {
                EquipHelmet(null);
            }
        }
        if (newGear != null)
        {
            //if (newGear.GetGearObject().GetGearType() == "Weapon")
            if (newType == "Weapon")
            {
                EquipWeapon(newGear);
            }
            if (newType == "Helmet")
            {
                EquipHelmet(newGear);
            }
        }
    }

    public void EquipWeapon(GearItem newWeapon)
    {
        if (currentWeapon != null)
        {
            currentWeapon.RemoveStatsFromPlayer();
            Destroy(weaponInstanceBack);
            Destroy(weaponInstanceCombat);
        }

        if (newWeapon == null)
        {
            currentWeapon = Resources.Load<GearItem>("Default_WeaponItem");
            weaponEquipped = currentWeapon.GetGearObject();

            weaponInstanceBack = Instantiate(currentWeapon.gameObject, weaponBackSocket);
            weaponInstanceCombat = Instantiate(currentWeapon.gameObject, weaponHandSocket);
            weaponInstanceCombat.SetActive(false);
            return;
        }

        currentWeapon = newWeapon;
        weaponEquipped = currentWeapon.GetGearObject();
        currentWeapon.AddStatsToPlayer();

        weaponInstanceBack = Instantiate(currentWeapon.gameObject, weaponBackSocket);
        weaponInstanceCombat = Instantiate(currentWeapon.gameObject, weaponHandSocket);
        weaponInstanceCombat.SetActive(false);
    }

    public void EquipHelmet(GearItem newHelmet)
    {
        if (currentHelmet != null)
        {
            currentHelmet.RemoveStatsFromPlayer();
            Destroy(helmetInstance);
        }

        if (newHelmet == null)
        {
            //currentHelmet.RemoveStatsFromPlayer();
            currentHelmet = null;
            helmetEquipped = null;
            Destroy(helmetInstance);

            //currentHelmet = Resources.Load<GearItem>("Default_WeaponItem");
            //helmetEquipped = currentHelmet.GetGearObject();

            //helmetInstance = Instantiate(currentHelmet.gameObject, helmetSocket);
            return;
        }

        currentHelmet = newHelmet;
        helmetEquipped = currentHelmet.GetGearObject();
        currentHelmet.AddStatsToPlayer();

        helmetInstance = Instantiate(currentHelmet.gameObject, helmetSocket);
    }

    //not going to use yet
    public void GetCurrentWeaponClass()
    {
        weaponEquipped.GetClass();
    }
}
