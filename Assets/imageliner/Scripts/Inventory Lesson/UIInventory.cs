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


    private Inventory inventoryRef;

    private void Start()
    {
        inventoryRef = FindAnyObjectByType<Inventory>();
        inventoryRef.onCurrencyChanged += UpdateCurrencyText;
        inventoryRef.onInventoryChanged += ()=> RefreshInventory(inventoryRef);

        UpdateCurrencyText(inventoryRef.currency);

        for (int i = 0; i < maxSlots; i++)
        {
            UIInventorySlot slotClone = Instantiate(slotPrefab, inventoryParent);
            slotClone.ClearSlot();
            allClonedSlots.Add(slotClone);
        }
    }

    private void OnEnable()
    {
        if (inventoryRef != null)
            RefreshInventory(inventoryRef);
    }

    private void Update()
    {
    }

    public void ClearUpUI()
    {
        foreach (var slot in allClonedSlots)
            slot.ClearSlot();
    }

    public void RefreshInventory(Inventory inventoryRef)
    {
        //clear slot contents, not the slots themselves
        foreach (var slot in allClonedSlots)
            slot.ClearSlot();

        foreach (InventoryItem item in inventoryRef.allItems)
            AddItemIconToInventory(item);

        UpdateCurrencyText(inventoryRef.currency);
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
