using UnityEngine;

[CreateAssetMenu(fileName = "New Usable", menuName = "Inventory/Usable")]
public class UsableItem : InventoryItem
{
    public int value;

    public UsableType usableType;
}
public enum UsableType
{
    Health,
    Mana
}