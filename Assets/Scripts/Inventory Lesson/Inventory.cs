using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject pickupPrefab;

    public List<InventoryItem> allItems = new List<InventoryItem>();

    private Dictionary<string, int> itemCounts = new Dictionary<string, int>();

    private Transform playerPos;

    [SerializeField] private int currency;

    public EquipInventoryItem equippedWeapon;
    public EquipInventoryItem equippedHelmet;
    public EquipInventoryItem equippedArmor;

    public Action<int> onCurrencyChanged;
    public Action onInventoryChanged;

    private void Awake()
    {
        playerPos = FindAnyObjectByType<PlayerCharacter>().transform;
    }


    private void IncrementCount(InventoryItem item)
    {
        string key = item.itemName;

        if (!itemCounts.ContainsKey(key))
            itemCounts[key] = 0;

        itemCounts[key]++;
    }

    private void DecrementCount(InventoryItem item)
    {
        string key = item.itemName;

        if (!itemCounts.ContainsKey(key))
            return;

        itemCounts[key]--;

        if (itemCounts[key] <= 0)
            itemCounts.Remove(key);
    }

    private void UpdatePotionUI(UsableItem usable)
    {
        int count = GetAmountOf(usable);

        var hud = FindAnyObjectByType<UIPlayerHUD>();
        if (usable.usableType == UsableType.Health)
            hud.UpdatePotionCount(count, "health");
        else if (usable.usableType == UsableType.Mana)
            hud.UpdatePotionCount(count, "mana");
    }

    public void AddItem(InventoryItem toAdd)
    {
        allItems.Add(toAdd);
        IncrementCount(toAdd);

        onInventoryChanged?.Invoke();

        if (toAdd is UsableItem usable)
            UpdatePotionUI(usable);
    }

    public void RemoveItem(InventoryItem toRemove)
    {
        var drop = Instantiate(pickupPrefab, playerPos.position, playerPos.rotation);
        drop.GetComponent<ItemPickup>().itemData = toRemove;

        allItems.Remove(toRemove);
        DecrementCount(toRemove);

        if (toRemove is UsableItem usable)
            UpdatePotionUI(usable);

        onInventoryChanged?.Invoke();
    }

    public void DeleteItem(InventoryItem toDelete)
    {
        allItems.Remove(toDelete);
        DecrementCount(toDelete);

        onInventoryChanged?.Invoke();
    }

    public void RemoveItemForEquip(InventoryItem toRemove)
    {
        allItems.Remove(toRemove);
        DecrementCount(toRemove);

        onInventoryChanged?.Invoke();
    }

    public bool HasItem(InventoryItem toCheck)
    {
        return allItems.Contains(toCheck);
    }

    public void AddCurrency(int currencyAmount)
    {
        currency += currencyAmount;
        onCurrencyChanged?.Invoke(currency);
    }

    public void RemoveCurrency(int currencyAmount)
    {
        currency -= currencyAmount;
        onCurrencyChanged?.Invoke(currency);
    }

    public void EquipNewHelmet(EquipInventoryItem newHelmet)
    {
        if (equippedHelmet != null)
        {
            AddItem(equippedHelmet);
            equippedHelmet = null;
        }

        allItems.Remove(newHelmet);
        DecrementCount(newHelmet);

        equippedHelmet = newHelmet;

        FindAnyObjectByType<UIEquipment>().EquipToSlot(newHelmet);
        FindAnyObjectByType<PlayerGearHandler>()
            .EquipGearType(newHelmet.gearItem, newHelmet.gearItem.GetGearObject().GetGearType());
    }

    public void EquipNewWeapon(EquipInventoryItem newWeapon)
    {
        if (equippedWeapon != null)
        {
            AddItem(equippedWeapon);
            equippedWeapon = null;
        }

        allItems.Remove(newWeapon);
        DecrementCount(newWeapon);

        equippedWeapon = newWeapon;

        FindAnyObjectByType<UIEquipment>().EquipToSlot(newWeapon);
        FindAnyObjectByType<PlayerGearHandler>()
            .EquipGearType(newWeapon.gearItem, newWeapon.gearItem.GetGearObject().GetGearType());
    }

    public void UseItem(UsableItem newUsable)
    {
        if (newUsable.usableType == UsableType.Health)
        {
            GameManager.singleton.playerStats.health.AddResource(newUsable.value);
        }
        else if (newUsable.usableType == UsableType.Mana)
        {
            GameManager.singleton.playerStats.mana.AddResource(newUsable.value);
        }

        DeleteItem(newUsable);
        UpdatePotionUI(newUsable);

        onInventoryChanged?.Invoke();
    }

    public int GetAmountOf(InventoryItem itemRef)
    {
        return itemCounts.TryGetValue(itemRef.itemName, out int count) ? count : 0;
    }

    private void UseFirstPotionOfType(UsableType type)
    {
        foreach (var item in allItems)
        {
            if (item is UsableItem usable && usable.usableType == type)
            {
                UseItem(usable);
                return;
            }
        }
    }

    public void GetFirstHealthPotion() => UseFirstPotionOfType(UsableType.Health);
    public void GetFirstManaPotion() => UseFirstPotionOfType(UsableType.Mana);
}
