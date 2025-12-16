using UnityEngine;


[CreateAssetMenu(fileName = "New Equip", menuName = "Inventory/Equipment")]
public class EquipInventoryItem : InventoryItem
{
    public EquipType equipType;

    public GearItem gearItem;


    public string GetStats()
    {
        return gearItem.GetGearObject().GetStats();
    }

    public int GetDamage()
    {
        return gearItem.GetGearObject().GetDamage();
    }
}
public enum EquipType
{
    Helmet,
    Armor,
    Weapon
}
