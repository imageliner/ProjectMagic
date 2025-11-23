using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public UIInventorySlot slotPrefab;
    public Transform inventoryParent;
    public List<UIInventorySlot> allClonedSlots = new List<UIInventorySlot>();

    public int maxSlots = 30;

    [SerializeField] TextMeshProUGUI goldText;

    private void Start()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            UIInventorySlot slotClone = Instantiate(slotPrefab, inventoryParent);
            slotClone.ClearSlot();
            allClonedSlots.Add(slotClone);
        }
    }

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
        foreach (var slot in allClonedSlots)
            slot.ClearSlot();
    }

    public void RefreshInventory(Inventory inventoryRef)
    {
        // simply clear slot contents, not the slots themselves
        foreach (var slot in allClonedSlots)
            slot.ClearSlot();

        foreach (InventoryItem item in inventoryRef.allItems)
            AddItemIconToInventory(item);
    }

    public void AddItemIconToInventory(InventoryItem item)
    {
        var emptySlot = allClonedSlots.FirstOrDefault(s => !s.inUse);

        if (emptySlot == null)
        {
            Debug.Log("inventory full");
            return;
        }

        emptySlot.InitializeItemDisplay(item);
    }

    public void UpdateCurrencyText(int currencyAmount)
    {
        goldText.text = "Gold: " + currencyAmount.ToString();
    }

    public void RemoveSpecificItem(InventoryItem item)
    {
        foreach (var slot in allClonedSlots)
        {
            if (slot.itemData == item)
            {
                slot.ClearSlot();
                return;
            }
        }
    }
}
