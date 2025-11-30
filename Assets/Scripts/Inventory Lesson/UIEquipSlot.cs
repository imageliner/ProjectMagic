using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipSlot : UIInventorySlot
{
    [SerializeField] private SlotType slotType;

    public enum SlotType
    {
        Weapon,
        Helmet,
        Armor
    }

    public void OnEquippedSlotClicked()
    {
        Inventory inv = FindAnyObjectByType<Inventory>();

        switch (slotType)
        {
            case SlotType.Weapon:
                inv.equippedWeapon = null;
                break;
            case SlotType.Helmet:
                inv.equippedHelmet = null;
                break;
            case SlotType.Armor:
                inv.equippedArmor = null;
                break;
        }

        inv.AddItem(itemData);
        ClearSlot();
        FindAnyObjectByType<PlayerGearHandler>().EquipGearType(null, slotType.ToString());

    }
}
