using UnityEngine;

public class PlayerGearHandler : MonoBehaviour
{
    public WeaponObject weaponEquipped;
    [SerializeField] private Transform weaponSocket;

    [SerializeField] private GameObject currentWeapon;


    private void Start()
    {
        EquipWeapon(weaponEquipped);
        Instantiate(weaponEquipped.mesh, weaponSocket);
    }

    public void EquipWeapon(WeaponObject weapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        if (weapon.mesh != null)
        {
            Instantiate(weapon.mesh, weaponSocket);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
        }
    }
}
