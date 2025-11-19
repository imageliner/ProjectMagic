using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public UIInventorySlot slotPrefab;
    public Transform inventoryParent;
    public List<UIInventorySlot> allClonedSlots = new List<UIInventorySlot>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RefreshInventory(FindAnyObjectByType<Inventory>());
            // or close window (disabled)
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ClearUpUI();
        }
    }

    public void ClearUpUI()
    {
        for (int i = 0; i < allClonedSlots.Count; i++)
        {
            Destroy(allClonedSlots[i].gameObject);
        }
        allClonedSlots.Clear();
    }

    public void RefreshInventory(Inventory inventoryRef)
    {
        ClearUpUI();

        foreach(InventoryItem item in inventoryRef.allItems)
        {
            AddItemIconToInventory(item);
        }
    }

    public void AddItemIconToInventory(InventoryItem item)
    {
        UIInventorySlot slotClone = Instantiate(slotPrefab, inventoryParent);
        slotClone.InitializeItemDisplay(item);
        allClonedSlots.Add(slotClone);
    }

    public void RemoveSpecificItem(InventoryItem item)
    {
        UIInventorySlot slotFound = allClonedSlots.Where(search => search.itemData == item).First();
        allClonedSlots.Remove(slotFound);
        Destroy(slotFound.gameObject);
    }
}
