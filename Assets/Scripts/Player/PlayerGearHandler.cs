using Unity.VisualScripting;
using UnityEngine;

public class PlayerGearHandler : MonoBehaviour
{
    public WeaponObject weaponEquipped;
    [SerializeField] private Transform weaponSocket;

    [SerializeField] private WeaponItem currentWeapon;
    private GameObject weaponInstance;

    [Header("Debug")]
    [SerializeField] private WeaponItem debug_Wep_WAR;
    [SerializeField] private WeaponItem debug_Wep_MAGE;
    [SerializeField] private WeaponItem debug_Wep_ARCH;


    private void Start()
    {
    }

    #region DEBUG

    public void Debug_EquipWeapon_WAR() => EquipWeapon(debug_Wep_WAR);
    public void Debug_EquipWeapon_MAGE() => EquipWeapon(debug_Wep_MAGE);
    public void Debug_EquipWeapon_ARCH() => EquipWeapon(debug_Wep_ARCH);


    #endregion

    public void EquipWeapon(WeaponItem newWeapon)
    {
        if (weaponInstance != null)
        {
            Destroy(weaponInstance);
            
        }

        currentWeapon = newWeapon;
        weaponEquipped = currentWeapon.GetWeaponObject();

        if (newWeapon != null)
        {
            weaponInstance = Instantiate(newWeapon.gameObject, weaponSocket);
            //currentWeapon.transform.localPosition = Vector3.zero;
            //currentWeapon.transform.localRotation = Quaternion.identity;
        }

    }
}
