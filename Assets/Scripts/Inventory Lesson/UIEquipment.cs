using NUnit.Framework.Interfaces;
using UnityEngine;

public class UIEquipment : MonoBehaviour
{
    public UIEquipSlot helmetSlot;
    public UIEquipSlot armorSlot;
    public UIEquipSlot weaponSlot;

    public void EquipToSlot(EquipInventoryItem equip)
    {
        if (equip.equipType == EquipType.Weapon)
        {
            FindAnyObjectByType<Inventory>().RemoveItemForEquip(equip);
            weaponSlot.itemData = equip;
            weaponSlot.InitializeItemDisplay(equip);
        }
        if (equip.equipType == EquipType.Armor)
        {
            FindAnyObjectByType<Inventory>().RemoveItemForEquip(equip);
            armorSlot.itemData = equip;
            armorSlot.InitializeItemDisplay(equip);
        }
        if (equip.equipType == EquipType.Helmet)
        {
            FindAnyObjectByType<Inventory>().RemoveItemForEquip(equip);
            helmetSlot.itemData = equip;
            helmetSlot.InitializeItemDisplay(equip);
        }
    }
}
