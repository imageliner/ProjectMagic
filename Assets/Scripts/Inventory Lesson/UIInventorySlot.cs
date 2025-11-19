using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI nameDisplay;

    public InventoryItem itemData;

    public void InitializeItemDisplay(InventoryItem item)
    {
        itemData = item;
        icon.color = item.itemColor;
        nameDisplay.text = item.name;
    }

    public void ClickToDelete()
    {// if itemdata is EquitptmentInventoryItem equip
        //could also open another window with equip or remove
        FindAnyObjectByType<Inventory>().RemoveItem(itemData);
    }
}
