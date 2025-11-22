using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipSlot : UIInventorySlot
{
    public void OnEquippedSlotClicked()
    {
        if (itemData != null)
        {
            Inventory inv = FindAnyObjectByType<Inventory>();
            
            inv.equippedWeapon = null;
            inv.AddItem(itemData);
            ClearSlot();
            FindAnyObjectByType<PlayerGearHandler>().EquipWeapon(null);
        }
    }
}
