using UnityEngine;


[CreateAssetMenu(fileName = "New Equip", menuName = "Inventory/Equipment")]
public class EquipInventoryItem : InventoryItem
{
    public EquipType equipType;

    public GearItem gearItem;

}
public enum EquipType
{
    Helmet,
    Armor,
    Weapon
}
